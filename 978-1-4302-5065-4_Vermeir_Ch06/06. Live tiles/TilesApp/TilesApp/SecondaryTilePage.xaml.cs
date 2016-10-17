using System;
using System.Collections.Generic;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TilesApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SecondaryTilePage : TilesApp.Common.LayoutAwarePage
    {
        public string TileMessage { get; set; }
        public string SmallerMessage { get; set; }

        public SecondaryTilePage()
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

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SecondaryTile secondaryTile = new SecondaryTile(
                                                            "tileId", 
                                                            "SecondTile", 
                                                            "Secondary Tile", 
                                                            "target=SecondaryTilePage", 
                                                            TileOptions.ShowNameOnLogo, 
                                                            new Uri("ms-appx:///Assets/secondaryIcon.png"));

            secondaryTile.RequestCreateAsync();
        }

        private void UpdateTile(XmlDocument tileXml, string tileId)
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
                tileXml.GetElementsByTagName("image")[0].Attributes[1].InnerText = "ms-appx:///Assets/secondaryIcon.png";
                tileTextAttributes[0].InnerText = TileMessage;
                tileTextAttributes[1].InnerText = SmallerMessage;
            }

            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForSecondaryTile(tileId).Update(tileNotification);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SecondaryTile.Exists("tileId"))
            {
                UpdateTile(TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText01), "tileId");                
            }

            ////comment the above code and uncomment the following code to update all live tiles
            //UpdateAllSecondaryTiles();
        }

        private async void UpdateAllSecondaryTiles()
        {
            IReadOnlyList<SecondaryTile> tilelist = await SecondaryTile.FindAllAsync();

            foreach (var tile in tilelist)
            {
                UpdateTile(TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText01), tile.TileId);                                
            }
        }
    }
}
