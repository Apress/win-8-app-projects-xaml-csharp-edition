﻿using ChildrenObservations.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace ChildrenObservations
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ObservationPage : ChildrenObservations.Common.LayoutAwarePage, INotifyPropertyChanged
    {
        private Observation _newObservation;
        public Observation NewObservation
        {
            get { return _newObservation; }
            set
            {
                if (_newObservation == value) return;

                _newObservation = value;

                OnPropertyChanged("NewObservation");
            }
        }

        private string _childName;
        public string ChildName
        {
            get { return _childName; }
            set
            {
                if (_childName == value) return;

                _childName = value;

                OnPropertyChanged("ChildName");
            }
        }

        public ObservationPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null) Frame.GoBack();

            NewObservation = new Observation();
            NewObservation.ChildId = (e.Parameter as Child).Id;
            ChildName = "Observation for " + (e.Parameter as Child).FullName;

            base.OnNavigatedTo(e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                NewObservation.Date = DateTime.Today;
                App.Connection.InsertAsync(NewObservation);
                Frame.GoBack();
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("Something went wrong, please try again");

                dialog.ShowAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
