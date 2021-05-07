using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_cinema.Assistants.Commands
{
    class LambdaCommand : Command
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(Execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _execute(parameter);
    }
}
