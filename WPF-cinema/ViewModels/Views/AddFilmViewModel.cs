using WPF_cinema.Assistants.Commands;
using WPF_cinema.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using System.Drawing;
using WPF_cinema.Views;
using WPF_cinema.ViewModels.Views;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System;
using Microsoft.Win32;
using System.IO;
using System.Linq;

namespace WPF_cinema.ViewModels.Views
{
    class AddFilmViewModel : BaseViewModel
    {
        #region private
        private User user;
        private CinemaDBContext context = new CinemaDBContext();
        private MainWindowViewModel MainwindowVM;
        private readonly string _myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        

        private BitmapImage _filmPicture = new BitmapImage();
        private Film editfilm;

        private string _filmName;
        private string _genre;
        private string _country;
        private string _director;
        private string _time;
        private string _description;
        private string _imgPath;
        private bool _dialog = false;
        private string _dialogText;
        #endregion

        #region public

        public string filmName
        {
            get => _filmName;
            set => Set(ref _filmName, value);
        }
        public string genre
        {
            get => _genre;
            set => Set(ref _genre, value);
        }
        public string country
        {
            get => _country;
            set => Set(ref _country, value);
        }
        public string director
        {
            get => _director;
            set => Set(ref _director, value);
        }
        public string time
        {
            get => _time;
            set => Set(ref _time, value);
        }
        public string description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        public BitmapImage filmPicture
        {
            get => _filmPicture;
            set => Set(ref _filmPicture, value);
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

        public void Reset()
        {
            filmName = "";
            genre = "";
            country = "";
            director = "";
            time = "";
            description = "";
            filmPicture = null;


        }

        public ICommand CloseDialogCommand { get; }
        private bool CanCloseDialogCommandExecute(object p) => true;
        private void OnCloseDialogCommandExecuted(object p) => dialog = false;

        public ICommand SelectImagePathCommand { get; }
        private bool CanSelectImagePathCommandExecute(object p) => true;
        private void OnSelectImagePathCommandExecuted(object p)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Фотографии|*.jpg;*.png;*.jpeg;";
            if (openFileDialog.ShowDialog() == true)
            {
                filmPicture = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                _imgPath = openFileDialog.FileName;
            }
        }

        public ICommand AddFillmCommand { get; }
        private bool CanAddFilmCommandExecute(object p) => true;
        private void OnAddFilmCommandExecuted(object p)
        {
           
            if(filmName?.Length > 0 && genre?.Length > 0 && country?.Length > 0 && director?.Length > 0 && time?.Length > 0 && description?.Length > 0)
            {
                if (context.Films.FirstOrDefault(f => f.FilmsName == filmName) == null)
                {


                    var film = new Film(filmName, genre, country, director, time, description);
                    if (editfilm == null)
                    {
                        context.Films.Add(film);

                    }
                    else
                    {
                        film.FilmsId = editfilm.FilmsId;
                        editfilm.FilmsName = filmName;
                        editfilm.Genre = genre;
                        editfilm.Country = country;
                        editfilm.Director = director;
                        editfilm.Description = description;
                        editfilm.Time = time;

                    }
                    context.SaveChanges();
                    Directory.CreateDirectory(_myDocumentsPath + $@"\PalaceOfArts\films\{film.FilmsId}");
                    try
                    {
                        File.Copy(_imgPath, _myDocumentsPath + $@"\PalaceOfArts\films\{film.FilmsId}\cover.jpg", true);
                    }
                    catch { }
                    Reset();
                }
                else
                {
                    dialogText = "Такой фильм уже есть";
                    dialog = true;
                }
            }
            else
            {
                dialogText = "Заполните все поля";
                dialog = true;
            }
        }

        public AddFilmViewModel(User user, MainWindowViewModel mainWindowVM)
        {
            this.user = user;
            MainwindowVM = mainWindowVM;
            
            SelectImagePathCommand = new LambdaCommand(OnSelectImagePathCommandExecuted, CanSelectImagePathCommandExecute);
            AddFillmCommand = new LambdaCommand(OnAddFilmCommandExecuted, CanAddFilmCommandExecute);
            CloseDialogCommand = new LambdaCommand(OnCloseDialogCommandExecuted, CanCloseDialogCommandExecute);
        }

       
    }
}
