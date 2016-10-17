using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppProjectsQuiz.DataModel
{
    public static class QuizDataSource
    {
        public static ObservableCollection<string> GetSubjects()
        {
            return new ObservableCollection<string> {"Movies", "Music", "Books", "Tech", "Comics"};
        }
    }
}
