using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.ViewModels.Base;
using WPF_cinema.Assistants.Commands;
using WPF_cinema.Views;


namespace WPF_cinema.ViewModels
{
    class RegWindowViewModel : BaseViewModel
    {
        private readonly CinemaDBContext context = new CinemaDBContext();
        private string _name;
        private string _email;
        private string _reglogin;
        private string _registerPassword;
        private bool _dialog = false;
        private string _dialogText;


        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        public string registerLogin
        {
            get => _reglogin;
            set => Set(ref _reglogin, value);
        }

        public string registerPassword
        {
            get => _registerPassword;
            set => Set(ref _registerPassword, value);
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


        public ICommand RegisterCommand { get; }
        private bool CanRegisterCommandExecute(object p) => Name?.Length > 0 && registerLogin?.Length > 0
            && registerPassword?.Length > 0;

        //RegWindow window { get => Application.Current.MainWindow as RegWindow; }
        private void OnRegisterCommandExecuted(object p)
        {
            if (context.Users.FirstOrDefault(u => u.Login == registerLogin) == null)
            {
                //if (context.Users.FirstOrDefault(u => u.UserId == 0))
                //{

                //    conte
                    var window = Application.Current.Windows[0];
                    User user = new User(Name, Email, registerLogin, registerPassword);
                    context.Users.Add(user);
                    context.SaveChanges();
                    var MainWindowViewModel = new MainWindowViewModel(user, "Catalog");
                    var MainWindow = new MainWindow
                    {
                        DataContext = MainWindowViewModel
                    };
                    MainWindow.Show();
                    window.Close();
                
            }
            else
            {
                dialogText = "Пользователь с данным псевдонимом уже зарегистрирован.";
                dialog = true;
            }
        }

        public ICommand AuthWindowCommand { get; }
        public bool CanAuthWindowCommandExecute(object p) => true;
        public void OnAuthWindowCommandExecuted(object p)
        {
            var window = Application.Current.Windows[0];
            var AuthWindow = new AuthWindow();
            AuthWindow.Show();
            window.Close();
        }

        public ICommand CloseDialogCommand { get; }
        private bool CanCloseDialogCommandExecute(object p) => true;
        private void OnCloseDialogCommandExecuted(object p) => dialog = false;

        public RegWindowViewModel()
        {
            #region Commands

            RegisterCommand = new LambdaCommand(OnRegisterCommandExecuted, CanRegisterCommandExecute);
            AuthWindowCommand = new LambdaCommand(OnAuthWindowCommandExecuted, CanAuthWindowCommandExecute);
            CloseDialogCommand = new LambdaCommand(OnCloseDialogCommandExecuted, CanCloseDialogCommandExecute);

            #endregion
        }
    }
}
