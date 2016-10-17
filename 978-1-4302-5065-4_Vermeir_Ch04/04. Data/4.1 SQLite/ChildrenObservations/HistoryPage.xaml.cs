using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChildrenObservations.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace ChildrenObservations
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class HistoryPage : ChildrenObservations.Common.LayoutAwarePage, INotifyPropertyChanged
    {
        private List<Child> _children;
 
        private List<Observation> _history;
        public List<Observation> History
        {
            get { return _history; }
            set
            {
                if (_history == value) return;

                _history = value;

                OnPropertyChanged("History");
            }
        }

        public string Description { get; set; }
        public HistoryPage()
        {
            try
            {
                this.InitializeComponent();

                Description = "Use the share charm to send this list to your email";
                GetData();

                DataTransferManager.GetForCurrentView().DataRequested += OnDataRequested;
            }
            catch (Exception e)
            {
                Frame.GoBack();
            }
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            args.Request.Data.Properties.Title = "Your baby's history";
            args.Request.Data.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(GetHtmlTable()));
        }

        private string GetHtmlTable()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(@"<table border=""1"">");
            builder.AppendLine("<tr>");
            builder.AppendLine("<th>Date</th>");
            builder.AppendLine("<th>Child</th>");
            builder.AppendLine("<th>Comment</th>");
            builder.AppendLine("</tr>");

            foreach (var action in History)
            {
                builder.AppendLine("<tr>");
                builder.AppendLine("<td>");
                builder.AppendLine(action.Date.ToString("MM/dd/yyyy"));
                builder.AppendLine("</td>");
                builder.AppendLine("<td>");
                builder.AppendLine(action.ChildName);
                builder.AppendLine("</td>");
                builder.AppendLine("<td>");
                builder.AppendLine(action.Text);
                builder.AppendLine("</td>");
                builder.AppendLine("</tr>");
            }

            builder.AppendLine("</table>");

            return builder.ToString();
        }

        private async void GetData()
        {
            try
            {
                History = await App.Connection.Table<Observation>().ToListAsync();
                _children = await App.Connection.Table<Child>().ToListAsync();

                foreach (var observation in History)
                {
                    Observation observation1 = observation;
                    observation.ChildName = _children.FirstOrDefault(c => c.Id == observation1.ChildId).FullName;
                }
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog("Something went wrong while fetching your data, try again");
                dialog.ShowAsync();
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
