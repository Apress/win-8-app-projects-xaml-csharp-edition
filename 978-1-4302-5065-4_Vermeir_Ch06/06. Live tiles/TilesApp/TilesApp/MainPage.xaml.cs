using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TilesApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : TilesApp.Common.LayoutAwarePage, INotifyPropertyChanged
    {
        public string TileMessage { get; set; }
        public string SmallerMessage { get; set; }
        public string BadgeNumber { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            InitializeControls();
        }

        private void InitializeControls()
        {
            TileMessage = "Enter tile message";
            SmallerMessage = "Enter smaller message";
            BadgeNumber = "Enter badge number";
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

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UpdateTile(TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareBlock));
        }

        private void UpdateTile(XmlDocument tileXml)
        {
            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");

            if (tileTextAttributes.Count == 1)
            {
                tileTextAttributes[0].InnerText = TileMessage;
            }
            else if (tileTextAttributes.Count == 2)
            {
                tileTextAttributes[0].InnerText = TileMessage;
                tileTextAttributes[1].InnerText = SmallerMessage;
            }
            else //more then 2 text elements means we can set an image in this example
            {
                tileXml.GetElementsByTagName("image")[0].Attributes[1].InnerText = "ms-appx:///Assets/icon.png";
                tileTextAttributes[0].InnerText = TileMessage;
                tileTextAttributes[1].InnerText = SmallerMessage;
            }

            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UpdateTile(TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText01));
        }

        private void Button_Click_3(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SecondaryTilePage));
        }

        private void Button_Click_4(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
           SetBadge(BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber));
        }

        private void SetBadge(XmlDocument template)
        {
            XmlElement textnode = (XmlElement)template.SelectSingleNode("/badge");

            textnode.SetAttribute("value", BadgeNumber);

            BadgeNotification badgeNotification = new BadgeNotification(template);

            BadgeUpdateManager.CreateBadgeUpdaterForSecondaryTile("tileId").Update(badgeNotification);
        }

        private void Button_Click_5(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BadgeNumber = "newMessage";
            SetBadge(BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph));
        }
    }
}
