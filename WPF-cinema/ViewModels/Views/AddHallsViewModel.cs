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
        private readonly CinemaDBContext context = new CinemaDBContext();
        private readonly Hall addhall;
        private User user;
        private string _hallsName;
        private int _capacity;
        private readonly MainWindowViewModel MainWindowVM;

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

        private void Reset()
        {
            HallsName = "";
            Capacity = 0;
        }

        public ICommand AddHallsCommand { get; }
        private bool CanAddHallsCommandExecute(object p) => true;
        private void OnAddHallsCommandExecute(object p)
        {
            var halls = new Hall(HallsName, Capacity);
            if (addhall == null)
            {
                context.Halls.Add(halls);
            }
            else
            {
                halls.HallsId = addhall.HallsId;
                addhall.HallsName = HallsName;
                addhall.Capacity = Capacity;
            }
            context.SaveChanges();
            Reset();
        }


        public AddHallsViewModel(User user, MainWindowViewModel vm)
        {
            this.user = user;
            MainWindowVM = vm;

            AddHallsCommand = new LambdaCommand(OnAddHallsCommandExecute, CanAddHallsCommandExecute);
        }
    }
}
