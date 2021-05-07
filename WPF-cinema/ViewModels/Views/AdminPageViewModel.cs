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
        private User user;



        public AdminPageViewModel(User user)
        {
            this.user = user;
        }
    }
}
