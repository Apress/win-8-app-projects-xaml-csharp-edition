using System;
using Windows.UI.Xaml;

namespace AppProjectsQuiz.Navigation
{
    public class NavigationService : INavigationService
    {
        public void Initialize(Window window, bool activate = true)
        {
            if (window.Content == null)
                window.Content = App.RootFrame;

            if (activate)
                window.Activate();
        }

        public virtual void Navigate(Type destination, object parameter = null)
        {
            // avoid navigation if current equals to destination

            if (App.RootFrame.CurrentSourcePageType != destination)
                App.RootFrame.Navigate(destination, parameter);
        }

        public virtual void GoBack()
        {
            if (App.RootFrame.CanGoBack)
                App.RootFrame.GoBack();
        }

        public virtual void GoForward()
        {
            if (App.RootFrame.CanGoForward)
                App.RootFrame.GoForward();
        }

        public virtual bool CanGoBack
        {
            get { return App.RootFrame.CanGoBack; }
        }

        public virtual bool CanGoForward
        {
            get { return App.RootFrame.CanGoForward; }
        }
    }
}