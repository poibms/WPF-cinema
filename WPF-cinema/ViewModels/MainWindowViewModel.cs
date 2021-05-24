using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.Views;
using WPF_cinema.ViewModels.Views;
using System;

namespace WPF_cinema.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly User user;
        private BaseViewModel _selectedVM;
        private bool _ItemVisability;

        public BaseViewModel selectedVM
        {
            get => _selectedVM;
            set => Set(ref _selectedVM, value);
        }

        public bool ItemVisability
        {
            get => _ItemVisability;
        }

        public ICommand SwitchUserCommand { get; }
        private bool CanSwitchUserCommandExecute(object p) => true;
        
        private void OnSwitchUserCommandExecuted(object p)
        {
            var window = Application.Current.Windows[0];
            var AuthWindow = new AuthWindow();
            AuthWindow.Show();
            window.Close();
        }

        public ICommand SwitchViewCommand { get; }
        
        private void OnSwitchViewCommandExecuted(object p)
        {
            switch (p.ToString())
            {
                case "AdminPage":
                    selectedVM = new AdminPageViewModel(user,this);
                    break;
                case "Catalog":
                    selectedVM = new AllFilmsViewModel(user, this);
                    break;
                case "Ticket":
                    selectedVM = new TicketsWindowViewModel(user, this);
                    break;
                case "Account":
                    selectedVM = new AccountPageViewModel(user, this);
                    break;
            }
        }

        public MainWindowViewModel(User user, string v)
        {
            this.user = user;
            _ItemVisability = user.Role != 0;

            if (v == "Catalog")
                selectedVM = new AllFilmsViewModel(user, this);
            else if(v == "AdminPage")
                selectedVM = new AdminPageViewModel(user,this);
            SwitchUserCommand = new LambdaCommand(OnSwitchUserCommandExecuted, CanSwitchUserCommandExecute);
            SwitchViewCommand = new LambdaCommand(OnSwitchViewCommandExecuted);
        }

       
    }
}
