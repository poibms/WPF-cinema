using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.ViewModels.Base;
using WPF_cinema.Assistants.Commands;
using WPF_cinema.Views;
using System.Text.RegularExpressions;

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


        private void OnRegisterCommandExecuted(object p)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            
            string nm = "^[a-zA-Z][a-zA-Z._\\d]*$";
            string login = "^[a-zA-Z][a-zA-Z._\\d]*$";
            if (Regex.IsMatch(Name, nm))
            {
                if (Regex.IsMatch(registerLogin, login))
                {
                    if (context.Users.FirstOrDefault(u => u.Login == registerLogin) == null)
                    {

                        if (Regex.IsMatch(Email, pattern, RegexOptions.IgnoreCase))
                        {
                            if (context.Users.FirstOrDefault(u => u.Email == Email) == null)
                            {
                                if (context.Users.Count() == 0)
                                {
                                    var window = Application.Current.Windows[0];
                                    User user1 = new User(Name, Email, registerLogin, registerPassword);
                                    user1.Role = 1;
                                    context.Users.Add(user1);
                                    context.SaveChanges();
                                    var MainWindowViewModel = new MainWindowViewModel(user1, "AdminPage");
                                    var MainWindow = new MainWindow
                                    {
                                        DataContext = MainWindowViewModel
                                    };
                                    MainWindow.Show();
                                    window.Close();
                                }
                                else
                                {
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
                            }
                            else
                            {
                                dialogText = "Пользователь с таким email уже зарегистрирован";
                                dialog = true;
                            }
                        }
                        else
                        {
                            dialogText = "Email введен некоректно";
                            dialog = true;
                        }

                    }
                    else
                    {
                        dialogText = "Пользователь с данным псевдонимом уже зарегистрирован.";
                        dialog = true;
                    }
                }
                else
                {
                    dialogText = "Не верный формат логина";
                    dialog = true;
                }
            }
            else
            {
                dialogText = "Не верный формат имени";
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
