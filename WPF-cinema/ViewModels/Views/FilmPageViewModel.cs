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
        private Session _session;
        private Hall _hall;


        #endregion

        #region public
        public Film film
        {
            get => _film;
        }

        private string _output;

        public string Output
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged();
            }
        }


        public Session session
        {
            get => _session;
        }
        public Hall hall
        {
            get => _hall;
        }
        #endregion

        public ICommand SwitchViewCommand { get; }
        private bool CanSwitchViewCommandExecute(object p) => true;
        private void OnSwitchViewCommandExecuted(object p)
        {
            MainwindowVM.selectedVM = new AllFilmsViewModel(user, MainwindowVM);
        }
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
            //Output = film.FilmsName + " " + hall.HallsName + " " + ssion.Date.ToString() + "" + ssion.Time.ToString();

            SwitchViewCommand = new LambdaCommand(OnSwitchViewCommandExecuted, CanSwitchViewCommandExecute);
            //SessionPageCommand = new LambdaCommand(OnSwitchSassionCommandExecuted, CanSwitchSassionCommandExecute);
        }
    }
}
