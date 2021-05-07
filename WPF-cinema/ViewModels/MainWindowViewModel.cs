using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.Views;
using WPF_cinema.ViewModels.Views;

namespace WPF_cinema.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly User user;
        private BaseViewModel _selectedVM;

        public BaseViewModel selectedVM
        {
            get => _selectedVM;
            set => Set(ref _selectedVM, value);
        }

        public ICommand SwitchUserCommand { get; }
        private bool CanSwitchUserCommandExecute(object p) => true;
        MainWindow window { get => Application.Current.MainWindow as MainWindow; }
        private void OnSwitchUserCommandExecuted(object p)
        {
            var Regwindow = new RegWindow();
            Regwindow.Show();
            window.Close();
        }

        public ICommand AdminCommand { get; }
        private bool CanSwitchAdminCommandExecute(object p) => true;
        private void OnSwitchAdminCommandExecuted(object p)
        {
            switch (p.ToString())
            {
                case "AdminPage":
                    selectedVM = new AdminPageViewModel(user);
                    break;
            }
        }

        public MainWindowViewModel(User user)
        {
            this.user = user;

            SwitchUserCommand = new LambdaCommand(OnSwitchUserCommandExecuted, CanSwitchUserCommandExecute);
            AdminCommand = new LambdaCommand(OnSwitchAdminCommandExecuted, CanSwitchAdminCommandExecute);
        }
    }
}
