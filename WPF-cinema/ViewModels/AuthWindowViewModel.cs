using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.ViewModels.Base;
using WPF_cinema.Assistants.Commands;
using WPF_cinema.Views;

namespace WPF_cinema.ViewModels
{
    class AuthWindowViewModel : BaseViewModel
    {
        private readonly CinemaDBContext context = new CinemaDBContext();
        private string _login;
        private string _loginPassword;
        private bool _dialog = false;
        private string _dialogText;

        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        public string loginPassword
        {
            get => _loginPassword;
            set => Set(ref _loginPassword, value);
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

        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => Login?.Length > 0 && loginPassword?.Length > 0;
        AuthWindow window { get => Application.Current.MainWindow as AuthWindow; }
        private void OnLoginCommandExecuted(object p)
        {
            
            if (context.Users.FirstOrDefault(u => u.Login == Login && u.Password == User.getHash(loginPassword)) != null)
            {
                User user = context.Users.FirstOrDefault(u => u.Login == Login);
                var MainWindowViewModel = new MainWindowViewModel(user);
                var MainWindow = new MainWindow
                {
                    DataContext = MainWindowViewModel
                };
                MainWindow.Show();
                window.Close();
            }
            else
            {
                
                dialogText = "Неверный логин или пароль.";
                dialog = true;
            }
        }

        public ICommand CloseDialogCommand { get; }
        private bool CanCloseDialogCommandExecute(object p) => true;
        private void OnCloseDialogCommandExecuted(object p) => dialog = false;

        public AuthWindowViewModel()
        {
            #region Commands

            LoginCommand = new LambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
            CloseDialogCommand = new LambdaCommand(OnCloseDialogCommandExecuted, CanCloseDialogCommandExecute);

            #endregion
        }
    }
}
