using System;

namespace AppProjectsQuiz.Navigation
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        void GoBack();
        void GoForward();
        void Initialize(global::Windows.UI.Xaml.Window window, bool activate = true);
        void Navigate(Type destination, object parameter = null);
    }
}
