
using System.Windows;

namespace WPF_cinema.Assistants.Commands
{
    class AppClose : Command
    {
       
            public override bool CanExecute(object parameter) => true;

            public override void Execute(object parameter) => Application.Current.Shutdown();
        
    }
}
