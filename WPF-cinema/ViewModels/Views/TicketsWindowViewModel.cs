using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using WPF_cinema.ViewModels.Base;
using System.Collections.ObjectModel;
using WPF_cinema.Assistants.Commands;
using System.Net.NetworkInformation;
using System;
using System.Net.Mail;
using System.Net;

namespace WPF_cinema.ViewModels.Views
{

    class TicketsWindowViewModel : BaseViewModel
    {
        #region private
        private CinemaDBContext context = new CinemaDBContext();
        private ObservableCollection<Session> _session = new ObservableCollection<Session>(new CinemaDBContext().Sessions);
        private ObservableCollection<Film> _film = new ObservableCollection<Film>(new CinemaDBContext().Films);
        private List<Ticket> _tickets;
        private Ticket _selectedOutput;
        private User user;
        private Session _date;
        private Film _filmname;
        private MainWindowViewModel mainwindowVM;

        private Visibility _visibleState = Visibility.Visible;
        private bool _dialog = false;
        private string _dialogText;


        #endregion

        #region public 

        public ObservableCollection<Session> session
        {
            get => _session;
            set => Set(ref _session, value);
        }
        public Session Date
        {
            get => _date;
            set => Set(ref _date, value);
        }
        public ObservableCollection<Film> film
        {
            get => _film;
            set => Set(ref _film, value);
        }
        public Film filmname
        {
            get => _filmname;
            set => Set(ref _filmname, value);
        }
        public List<Ticket> tickets
        {
            get => _tickets;
            set => Set(ref _tickets, value);
        }

        public Ticket selectedOutput
        {
            get => _selectedOutput;
            set => Set(ref _selectedOutput, value);
        }

        public Visibility VisibleState
        {
            get => _visibleState;
            set => Set(ref _visibleState, value);
        }
        public bool dialog
        {
            get => _dialog;
            set => Set(ref _dialog, value);
        }

        public string dialogText
        {
            get => _dialogText;
            set => Set(ref _dialogText, value);
        }

        #endregion

        #region command
        public ICommand CloseDialogCommand { get; }
        private bool CanCloseDialogCommandExecute(object p) => true;
        private void OnCloseDialogCommandExecuted(object p) => dialog = false;

        public ICommand SortTickets { get; }
        private bool CanSortTicketsComandExecute(object p) => true;
        private void OnSortTicketsCommandExecute(object p)
        {
            
            if (filmname == null || Date == null)
            {
                dialogText = "Заполните все поля";
                dialog = true;
            }
            else
            {
                tickets = (context.Tickets.Where(t => t.Session.Films.FilmsName == filmname.FilmsName && t.Session.Date == Date.Date).ToList());
                if (tickets.Count == 0)
                {
                    dialogText ="Билетов нет";
                    dialog = true;
                }
                //tickets = context.Tickets.Where(t => t.Session.Films.FilmsName.Contains(filmname)).ToList();
            }
        }
        private static bool _CanPingGoogle()
        {
            const int timeout = 1000;
            const string host = "google.com";

            var ping = new Ping();
            var buffer = new byte[32];
            var pingOptions = new PingOptions();

            try
            {
                var reply = ping.Send(host, timeout, buffer, pingOptions);
                return (reply != null && reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICommand AddOrderTicketCommand { get; }
        private bool CanAddOrderTicketCommandExecute(object p) => true;
        private void OnAddOrderTicketCommandExecuted(object p)
        {
            if (context.OrderTickets.FirstOrDefault(o => o.TicketsId == selectedOutput.TicketsId) == null)
            {
                context.OrderTickets.Add(new OrderTicket { TicketsId = selectedOutput.TicketsId, UserId = user.UserId });
                context.SaveChanges();
                tickets.Remove(selectedOutput);
                //if (_CanPingGoogle())
                //{
                //    try
                //    {
                //        MailAddress from = new MailAddress("unc7447@gmail.com", "Company Rentlock");
                //        MailAddress to = new MailAddress($"{}");
                //        MailMessage m = new MailMessage(from, to);
                //        m.Subject = "Rentlock";
                //        m.Body = $"<h2>Dear {name}, thank you for oredring the {car} on the {date}.\nWe will contact you at the number {phone} as soon as possible.</h2>";
                //        m.IsBodyHtml = true;
                //        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //        smtp.Credentials = new NetworkCredential("unc7447@gmail.com", "Macbook2019");
                //        smtp.EnableSsl = true;
                //        smtp.Send(m);
                //        Console.Read();
                //        MessageBox.Show($"Thank you for order, our manager will contact you.\nCheck your e-mail {nowemailrent}.");

                //    }
                //    catch
                //    {
                //        MessageBox.Show("Thank you for order, our manager will contact you\nYour email doesn't exist");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Thank you for order, our manager will contact you");

                //}

            }
            else
            {
                dialogText = "Этот билет уже заказан";
                dialog = true;
            }
        }
        #endregion

        public TicketsWindowViewModel(User user, MainWindowViewModel mainwindowVM)
        {
            this.user = user;
            this.mainwindowVM = mainwindowVM;

            tickets = context.Tickets.ToList();
            //List<Output> hyulist = new List<Output>();
            foreach (var ticket in tickets)
            {
                ticket.Session = context.Sessions.FirstOrDefault(s => s.SessionId == ticket.SessionId);
                ticket.Session.Films = context.Films.FirstOrDefault(f => f.FilmsId == ticket.Session.FilmsId);
                ticket.Session.Halls = context.Halls.FirstOrDefault(f => f.HallsId == ticket.Session.HallsId);
                //Output h = new Output(ticket.Session.Halls.HallsName, ticket.Session.Films.FilmsName, ticket.Session.Date, ticket.Session.Time, ticket.Place, ticket.Row);
                //hyulist.Add(h);
            }


            AddOrderTicketCommand = new LambdaCommand(OnAddOrderTicketCommandExecuted, CanAddOrderTicketCommandExecute);
            SortTickets = new LambdaCommand(OnSortTicketsCommandExecute, CanSortTicketsComandExecute);
            CloseDialogCommand = new LambdaCommand(OnCloseDialogCommandExecuted, CanCloseDialogCommandExecute);
        }
    }
}
