using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.ViewModels.Base;
using System.Collections.ObjectModel;
using WPF_cinema.Assistants.Commands;
using WPF_cinema.Views;
using System;

namespace WPF_cinema.ViewModels.Views
{
    class SessionPageViewModel :BaseViewModel
    {
        #region private
        private User user;
        private MainWindowViewModel MainwindowVM;
        private readonly CinemaDBContext context = new CinemaDBContext();
        private ObservableCollection<Session> _sessionlist = new ObservableCollection<Session>();
        #endregion

        #region public
        public ObservableCollection<Session> sessionlist
        {
            get => _sessionlist;
            set => Set(ref _sessionlist, value);
        }

       
        #endregion

        public SessionPageViewModel(User user,  MainWindowViewModel vm)
        {
            this.user = user;
            MainwindowVM = vm;
            

            //_sessionlist = new ObservableCollection<Session>(context.Sessions.Where(s => s.FilmsId == film.FilmsId).Select(s => s.FilmsId).Contains(film.FilmsId));
            //_sessionlist = new ObservableCollection<Session>(context.Sessions.Where(s => context.Films.Where(s => s.FilmsId == film.FilmsId).Select(s => s.FilmsId).Contains(film.FilmsId)));
        }
    }
}
