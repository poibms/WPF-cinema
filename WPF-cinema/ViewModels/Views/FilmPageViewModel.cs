using System.Linq;
using System.Windows.Input;
using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;

namespace WPF_cinema.ViewModels.Views
{
    class FilmPageViewModel : BaseViewModel
    {
        #region private
        private User user;
        private readonly CinemaDBContext context = new CinemaDBContext();
        private MainWindowViewModel MainwindowVM;
        private Film _film;
        


        #endregion

        #region public
        public Film film
        {
            get => _film;
        }


        //public Session session
        //{
        //    get => _session;
        //}
        //public Hall hall
        //{
        //    get => _hall;
        //}
        #endregion

        public ICommand SwitchViewCommand { get; }
        private bool CanSwitchViewCommandExecute(object p) => true;
        private void OnSwitchViewCommandExecuted(object p)
        {
            MainwindowVM.selectedVM = new AllFilmsViewModel(user, MainwindowVM);
        }
        //public ICommand TicketsPageCommand { get; }
        //private void OnSwitchTicketCommandExecuted(object p)
        //{
        //    MainwindowVM.selectedVM = new TicketsWindowViewModel(user,MainwindowVM);
        //}
        //public ICommand SessionPageCommand { get; }
        //private bool CanSwitchSassionCommandExecute(object p) => true;
        //private void OnSwitchSassionCommandExecuted(object p)
        //{
        //    MainwindowVM.selectedVM = new SessionPageViewModel(user, (int)p, MainwindowVM);
        //}


        public FilmPageViewModel(User user, int FilmId, MainWindowViewModel mainwindowVM)
        {
            this.user = user;
            MainwindowVM = mainwindowVM;

            _film = context.Films.Find(FilmId);

            //var ssion = context.Sessions.FirstOrDefault(s => s.FilmsId == FilmId);
            //var film = context.Films.FirstOrDefault(f => f.FilmsId == ssion.FilmsId);
            //var hall = context.Halls.FirstOrDefault(f => f.HallsId == ssion.HallsId);
            //var ticket = context.Tickets.FirstOrDefault(t => t.SessionId == ssion.SessionId);
            //output = film.FilmsName + " " + hall.HallsName + " " + ssion.Date.ToString() + " " + ssion.Time.ToString() + " " + ticket.Place.ToString() + " " + ticket.Row.ToString();

            SwitchViewCommand = new LambdaCommand(OnSwitchViewCommandExecuted, CanSwitchViewCommandExecute);
            //TicketsPageCommand = new LambdaCommand(OnSwitchTicketCommandExecuted);
            //SessionPageCommand = new LambdaCommand(OnSwitchSassionCommandExecuted, CanSwitchSassionCommandExecute);
        }
    }
}
