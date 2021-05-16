using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.Views;
using WPF_cinema.ViewModels.Views;

namespace WPF_cinema.ViewModels.Views
{
    class AdminPageViewModel : BaseViewModel
    {
        private readonly User user;
        private readonly MainWindowViewModel MainWindowVM;

       

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

        public AdminPageViewModel(User user, MainWindowViewModel vm)
        {
            this.user = user;
            MainWindowVM = vm;

            HallsViewCommand = new LambdaCommand(OnSwitchHallsCommandExecuted);
            FilmsViewCommand = new LambdaCommand(OnSwitchFilmsCommandExecuted);
            SessionViewCommand = new LambdaCommand(OnSwitchSessionCommandExecuted);
            TicketsViewCommand = new LambdaCommand(OnSwitchTicketsCommandExecuted);
        }
    }
}
