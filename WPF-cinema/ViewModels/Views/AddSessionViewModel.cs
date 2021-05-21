using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.ViewModels.Base;
using System.Collections.ObjectModel;
using WPF_cinema.Assistants.Commands;
using WPF_cinema.Views;
using System;
using System.Collections.Generic;

namespace WPF_cinema.ViewModels.Views
{
    class AddSessionViewModel : BaseViewModel
    {
        #region private
        private User user;

        private CinemaDBContext context = new CinemaDBContext();
        private List<Film> _films;
        private List<Hall> _halls;
        private Film _selectedFilm;
        private Hall _selectedHall;
        private string _date;
        private string _time;
        int place = 50;
        private MainWindowViewModel mainWindowVM;

        #endregion

        #region public
        //public Film films
        //{
        //    get => _selectedFilm;
        //} 
        //public Hall halls
        //{
        //    get => _selectedHall;
        //}

        public List<Film> films
        {
            get => _films;
            set => Set(ref _films, value);
        }
        public Film selectedFilm
        {
            get => _selectedFilm;
            set => Set(ref _selectedFilm, value);
        }
        public List<Hall> halls
        {
            get => _halls;
            set => Set(ref _halls, value);
        }
        public Hall selectedHall
        {
            get => _selectedHall;
            set => Set(ref _selectedHall, value);
        }
        public string date
        {
            get => _date;
            set => Set(ref _date, value);
        }
        public string time
        {
            get => _time;
            set => Set(ref _time, value);
        }
        #endregion

        public ICommand AddSessionommand { get; }
        private bool CanToSessionCommandExecute(object p) => true;
        private void OnToSessionCommandExecuted(object p)
        {

            var ses1 = new Session(selectedFilm.FilmsId, selectedHall.HallsId, date, time);
            ses1.Films = selectedFilm;
            ses1.Halls = selectedHall;
            if (context.Sessions.FirstOrDefault(s => s.FilmsId == _selectedFilm.FilmsId && s.HallsId == selectedHall.HallsId && s.Date == date && s.Time == time) == null)
            {
                context.Sessions.Add(ses1);
                context.SaveChanges();
            }
            if (context.Tickets.FirstOrDefault(t => t.SessionId == ses1.SessionId) == null)
            {
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 1; j <= 5; j++)
                    {
                        //context.Tickets.Add(new Ticket { SessionId = ses1.SessionId, Row = j, Place = i });
                        Ticket tic = new Ticket(j,i);
                        tic.Session = context.Sessions.FirstOrDefault(s => s.SessionId == ses1.SessionId);
                        context.Tickets.Add(tic);
                    }
                }
                context.SaveChanges();
            }


        }

        public AddSessionViewModel(User user, MainWindowViewModel vm)
        {
            this.user = user;
            mainWindowVM = vm;

            films = context.Films.ToList();
            halls = context.Halls.ToList();

            AddSessionommand = new LambdaCommand(OnToSessionCommandExecuted, CanToSessionCommandExecute);
        }
    }
}
