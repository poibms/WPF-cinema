using System;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.Views;
using WPF_cinema.ViewModels.Views;
using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;

namespace WPF_cinema.ViewModels.Views
{
    class AddHallsViewModel : BaseViewModel
    {
        #region private
        private readonly CinemaDBContext context = new CinemaDBContext();
        private readonly Hall addhall;
        private User user;
        private string _hallsName;
        private int _capacity;
        private readonly MainWindowViewModel MainWindowVM;

        private bool _dialog = false;
        private string _dialogText;
        #endregion

        #region publc
        public string HallsName
        {
            get => _hallsName;
            set => Set(ref _hallsName, value);
        }
        public int Capacity
        {
            get => _capacity;
            set => Set(ref _capacity, value);
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

        #endregion 
        private void Reset()
        {
            HallsName = "";
            Capacity = 0;
        }
        public ICommand CloseDialogCommand { get; }
        private bool CanCloseDialogCommandExecute(object p) => true;
        private void OnCloseDialogCommandExecuted(object p) => dialog = false;

        public ICommand AddHallsCommand { get; }
        private bool CanAddHallsCommandExecute(object p) => true;
        private void OnAddHallsCommandExecute(object p)
        {
            if (HallsName != string.Empty && Capacity != 0)
            {
                var halls = new Hall(HallsName, Capacity);
                context.Halls.Add(halls);
                //if (addhall == null)
                //{
                //    context.Halls.Add(halls);
                //}
                //else
                //{
                //    halls.HallsId = addhall.HallsId;
                //    addhall.HallsName = HallsName;
                //    addhall.Capacity = Capacity;
                //}
                context.SaveChanges();
                Reset();
            }
            else
            {
                dialogText = "Заполните все поля";
                dialog = true;
            }
        }


        public AddHallsViewModel(User user, MainWindowViewModel vm)
        {
            this.user = user;
            MainWindowVM = vm;

            AddHallsCommand = new LambdaCommand(OnAddHallsCommandExecute, CanAddHallsCommandExecute);
            CloseDialogCommand = new LambdaCommand(OnCloseDialogCommandExecuted, CanCloseDialogCommandExecute);
        }
    }
}
