using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using WPF_cinema.ViewModels.Base;
using System.Collections.ObjectModel;
using WPF_cinema.Assistants.Commands;
using Microsoft.EntityFrameworkCore;

namespace WPF_cinema.ViewModels.Views
{
    class AccountPageViewModel : BaseViewModel
    {
        #region private
        private User user;
        private readonly CinemaDBContext context = new CinemaDBContext();
        private ObservableCollection<Ticket> _orderTicket = new ObservableCollection<Ticket>();
        private Ticket _selectedticket;
        private User _user;
        private MainWindowViewModel mainWindowViewModel;

        private bool _dialog = false;
        private string _dialogText;

        private string _login;
        #endregion

        #region public
        public User User
        {
            get => _user;

        }
        public string login
        {
            get => _login;
            set => Set(ref _login, value);
        }
        public ObservableCollection<Ticket> orderTicket
        {
            get => _orderTicket;
            set => Set(ref _orderTicket, value);
        }
        public Ticket selectedticket
        {
            get => _selectedticket;
            set => Set(ref _selectedticket, value);
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



        public ICommand DeleteAccountCommand { get; }
        private bool CanDeleteCommandExecute(object p) => true;
        private void OnDeleteCommandExecuted(object p)
        {
            if (user.Role != 1)
            {

                User us = context.Users.Find(user.UserId);

                context.Users.Remove(us);
                context.SaveChanges();
                App.Current.Shutdown();
            }
            else
            {
                dialogText = "Администратор не может удалить аккаунт";
                dialog = true;
            }
        }
        public ICommand CancelOrderTicketCommand { get; }
        private bool CanCancelOrderTicketCommandExecute(object p) => true;
        private void OnCancelOrderTicketCommandExecuted(object p)
        {
            if (selectedticket != null)
            {
                //foreach (OrderTicket ord in context.OrderTickets.Where(o => o.TicketsId == selectedticket.TicketsId))
                //{
                //    context.OrderTickets.Remove(ord);
                //}
                //context.OrderTickets.Remove(selectedticket);
                var newu = context.Users.Include(u => u.OrderTickets).FirstOrDefault(u => u.UserId == user.UserId).OrderTickets.Remove(context.OrderTickets.FirstOrDefault(t => t.TicketsId == selectedticket.TicketsId));
                
                orderTicket.Remove(selectedticket);
                context.SaveChanges();
                
            }
            else
            {
                dialogText = "Выберите билет";
                dialog = true;
            }
        }
        public ICommand CloseDialogCommand { get; }
        private bool CanCloseDialogCommandExecute(object p) => true;
        private void OnCloseDialogCommandExecuted(object p) => dialog = false;

        public AccountPageViewModel(User user, MainWindowViewModel mainWindowViewModel)
        {
            this.user = user;
            this.mainWindowViewModel = mainWindowViewModel;

            _user = context.Users.Find(user.UserId);

            //orderTicket = context.OrderTickets.ToList();
            _orderTicket = new ObservableCollection<Ticket>(context.Tickets.Where(t => context.OrderTickets.Where(o => o.UserId == user.UserId).Select(s => s.TicketsId).Contains(t.TicketsId)));
            foreach (var ticket in _orderTicket)
            {
                ticket.Session = context.Sessions.FirstOrDefault(s => s.SessionId == ticket.SessionId);
                ticket.Session.Films = context.Films.FirstOrDefault(f => f.FilmsId == ticket.Session.FilmsId);
                ticket.Session.Halls = context.Halls.FirstOrDefault(f => f.HallsId == ticket.Session.HallsId);
                //Output h = new Output(ticket.Session.Halls.HallsName, ticket.Session.Films.FilmsName, ticket.Session.Date, ticket.Session.Time, ticket.Place, ticket.Row);
                //hyulist.Add(h);
            }
            //foreach (var orderticket in orderTicket)
            //{
            //    orderticket.Tickets = context.Tickets.FirstOrDefault(t => t.TicketsId == orderticket.TicketsId);
            //    //orderticket.Tickets = context.Sessions.FirstOrDefault(s => s.SessionId == orderticket.)
            //}
            //foreach (OrderTicket ordtcts in context.Users.Find(user.UserId).OrderTickets)
            //{
            //    _orderTicket.Add(ordtcts);
            //}

            CancelOrderTicketCommand = new LambdaCommand(OnCancelOrderTicketCommandExecuted, CanCancelOrderTicketCommandExecute);
            DeleteAccountCommand = new LambdaCommand(OnDeleteCommandExecuted, CanDeleteCommandExecute);
            CloseDialogCommand = new LambdaCommand(OnCloseDialogCommandExecuted, CanCloseDialogCommandExecute);
        }
    }
}
