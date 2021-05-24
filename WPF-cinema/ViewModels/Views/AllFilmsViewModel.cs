using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using WPF_cinema.Views;
using WPF_cinema.ViewModels.Views;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WPF_cinema.ViewModels.Views
{
    class AllFilmsViewModel : BaseViewModel
    {
        private User user;
        private readonly CinemaDBContext context = new CinemaDBContext();
        private readonly List<string> _sorting = new List<string> { "по новизне", "по алфавиту"};
        private MainWindowViewModel MainwindowVM;
        private ObservableCollection<Film> _filmList;

        private string _search;
        private string _sortingSelected;

        #region public

        public ObservableCollection<Film> filmList
        {
            get => _filmList;
            set => Set(ref _filmList, value);
        }

        public List<string> sorting
        {
            get => _sorting;
        }

        public string search
        {
            get => _search;
            set => Set(ref _search, value);
        }
        public string sortingSelected
        {
            get => _sortingSelected;
            set
            {
                Set(ref _sortingSelected, value);
                switch (sortingSelected)
                {
                    case "по новизне":
                        filmList = new ObservableCollection<Film>(context.Films.OrderByDescending(f => f.FilmsId));
                        break;
                    case "по алфавиту":
                        filmList = new ObservableCollection<Film>(context.Films.OrderBy(f => f.FilmsName));
                        break;
                }
            }
        }
        #endregion

        public ICommand SearchFilms { get; }
        public bool CamSearchFilmsCommandExecute(object p) => true;
        public void OnSearchFilmsCommandExecute(object p)
        {
            if(!string.IsNullOrWhiteSpace(search))
            {
                filmList = new ObservableCollection<Film>(context.Films.Where(f => f.FilmsName.Contains(search)));
            }
            else if (string.IsNullOrWhiteSpace(search))
            {
                filmList = new ObservableCollection<Film>(context.Films.AsNoTracking().ToList());
            }
            
        }

        public ICommand SwitchViewCommand { get; }
        private void OnSwitchViewCommandExecuted(object p)
        {
            MainwindowVM.selectedVM = new FilmPageViewModel(user, (int)p, MainwindowVM);
        }
      

        public AllFilmsViewModel(User user, MainWindowViewModel vm)
        {
            this.user = user;
            MainwindowVM = vm;
            sortingSelected = sorting[0];

            SwitchViewCommand = new LambdaCommand(OnSwitchViewCommandExecuted);
            SearchFilms = new LambdaCommand(OnSearchFilmsCommandExecute, CamSearchFilmsCommandExecute);
            
        }
    }
}
