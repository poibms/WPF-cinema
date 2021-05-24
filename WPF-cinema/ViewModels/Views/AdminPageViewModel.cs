using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.Views;
using WPF_cinema.ViewModels.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPF_cinema.ViewModels.Views
{
    class AdminPageViewModel : BaseViewModel
    {
        #region private
        private readonly User user;
        private CinemaDBContext context = new CinemaDBContext();
        private readonly MainWindowViewModel MainWindowVM;
        private ObservableCollection<Film> _film = new ObservableCollection<Film>(new CinemaDBContext().Films);
        private Film _selectedfilm;
        private Film _flmname;

        private bool _dialog = false;
        private string _dialogText;
        #endregion

        #region public
        public ObservableCollection<Film> Film
        {
            get => _film;
            set => Set(ref _film, value);
        }
        public Film selectedFilm
        {
            get => _selectedfilm;
            set => Set(ref _selectedfilm, value);
        }
        public Film FilmName
        {
            get => _flmname;
            set => Set(ref _flmname, value);
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
        public ICommand HallsViewCommand { get; }
        private void OnSwitchHallsCommandExecuted(object p)
        {
            MainWindowVM.selectedVM = new AddHallsViewModel(user, MainWindowVM);
        }
        public ICommand FilmsViewCommand { get; }
        private void OnSwitchFilmsCommandExecuted(object p)
        {
            MainWindowVM.selectedVM = new AddFilmViewModel(user, MainWindowVM);
        }
        public ICommand SessionViewCommand { get; }
        private void OnSwitchSessionCommandExecuted(object p)
        {
            MainWindowVM.selectedVM = new AddSessionViewModel(user, MainWindowVM);
        }
        public ICommand TicketsViewCommand { get; }
        private void OnSwitchTicketsCommandExecuted(object p)
        {
            MainWindowVM.selectedVM = new AddSessionViewModel(user, MainWindowVM);
        }
        public ICommand CloseDialogCommand { get; }
        private bool CanCloseDialogCommandExecute(object p) => true;
        private void OnCloseDialogCommandExecuted(object p) => dialog = false;

        //public ICommand ChangeFilmCommand { get; }
        //private bool CanChangeFilmCommandExecute(object p) => true;
        //private void OnChangeFilmCommandExecute(object p)
        //{
        //    if (Film != null)
        //    {
        //        MainWindowVM.selectedVM = new AddFilmViewModel(user, (int)p, MainWindowVM);
        //    }
        //    else
        //    {
        //        dialogText = "Выберите фильм";
        //        dialog = true;
        //    }
        //}
        public ICommand DeleteFilmCommand { get; }
        private bool CanDeleteFilmCommandExecute(object p) => true;
        private void OnDeleteFilmCommandExecute(object p)
        {
            if (Film != null)
            {
                foreach (Film flm in context.Films.Where(f => f.FilmsId == FilmName.FilmsId))
                {
                    context.Films.Remove(flm);
                }
                foreach (Session ssion in context.Sessions.Where(s => s.FilmsId == FilmName.FilmsId))
                {
                    context.Sessions.Remove(ssion);
                }
                foreach (Ticket tckt in context.Tickets.Where(t => t.Session.Films.FilmsId == FilmName.FilmsId))
                {
                    context.Tickets.Remove(tckt);
                }
                foreach (OrderTicket ordtckt in context.OrderTickets.Where(o => o.Tickets.Session.Films.FilmsId == FilmName.FilmsId))
                {
                    context.OrderTickets.Remove(ordtckt);
                }
                context.SaveChanges();
                Film = null;
            }
            else
            {
                dialogText = "Выберите фильм";
                dialog = true;
            }
        }

        #endregion

        public AdminPageViewModel(User user, MainWindowViewModel vm)
        {
            this.user = user;
            MainWindowVM = vm;

            HallsViewCommand = new LambdaCommand(OnSwitchHallsCommandExecuted);
            FilmsViewCommand = new LambdaCommand(OnSwitchFilmsCommandExecuted);
            SessionViewCommand = new LambdaCommand(OnSwitchSessionCommandExecuted);
            TicketsViewCommand = new LambdaCommand(OnSwitchTicketsCommandExecuted);

            DeleteFilmCommand = new LambdaCommand(OnDeleteFilmCommandExecute, CanDeleteFilmCommandExecute);
            //ChangeFilmCommand = new LambdaCommand(OnChangeFilmCommandExecute, CanChangeFilmCommandExecute);
            CloseDialogCommand = new LambdaCommand(OnCloseDialogCommandExecuted, CanCloseDialogCommandExecute);
        }
    }
}
