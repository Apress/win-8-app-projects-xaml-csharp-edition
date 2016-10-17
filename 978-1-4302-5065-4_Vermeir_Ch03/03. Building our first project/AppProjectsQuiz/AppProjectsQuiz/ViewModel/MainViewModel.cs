using System.Collections.Generic;
using System.Collections.ObjectModel;
using AppProjectsQuiz.DataModel;
using AppProjectsQuiz.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AppProjectsQuiz.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        private ObservableCollection<string> _subjects;
        public ObservableCollection<string> Subjects
        {
            get { return _subjects; }
            set
            {
                if (_subjects == value) return;

                _subjects = value;
                RaisePropertyChanged(() => Subjects);
            }
        }

        private string _selectedSubject;

        public string SelectedSubject
        {
            get { return _selectedSubject; }
            set 
            {
                _selectedSubject = value;

                _navigationService.Navigate(typeof(QuizPage), _selectedSubject);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Subjects = QuizDataSource.GetSubjects();
        }
    }
}