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

        #endregion

        public ICommand SwitchViewCommand { get; }
        private bool CanSwitchViewCommandExecute(object p) => true;
        private void OnSwitchViewCommandExecuted(object p)
        {
            MainwindowVM.selectedVM = new AllFilmsViewModel(user, MainwindowVM);
        }
       


        public FilmPageViewModel(User user, int FilmId, MainWindowViewModel mainwindowVM)
        {
            this.user = user;
            MainwindowVM = mainwindowVM;

            _film = context.Films.Find(FilmId);

            SwitchViewCommand = new LambdaCommand(OnSwitchViewCommandExecuted, CanSwitchViewCommandExecute);
            
        }
    }
}
