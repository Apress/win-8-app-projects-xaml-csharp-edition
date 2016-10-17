using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NotificationsDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //await BackgroundExecutionManager.RequestAccessAsync();
        }

        private void ButtonSetLockScreenClick(object sender, RoutedEventArgs e)
        {
            var lockscreen = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            var template = lockscreen.GetElementsByTagName("badge");
            ((XmlElement)template[0]).SetAttribute("value", TextboxLockScreen.Text);

            BadgeNotification badge = new BadgeNotification(lockscreen);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
        }

        private void ButtonSendNotificationClick(object sender, RoutedEventArgs e)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            var element = template.GetElementsByTagName("text")[0];
            element.AppendChild(template.CreateTextNode(TextboxToastText.Text.Trim()));

            var toast = new ToastNotification(template);
            notifier.Show(toast);

        }

        private void ButtonSendNotificationWithSoundClick(object sender, RoutedEventArgs e)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            var element = template.GetElementsByTagName("text")[0];
            element.AppendChild(template.CreateTextNode(TextboxToastText.Text.Trim()));

            XmlElement audioElement = template.CreateElement("audio");
            audioElement.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            var toast = new ToastNotification(template);
            notifier.Show(toast);

        }

        private void ButtonSendNotificationWithImageClick(object sender, RoutedEventArgs e)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);

            var element = template.GetElementsByTagName("text")[0];
            ((XmlElement)template.GetElementsByTagName("image")[0]).SetAttribute("src", "ms-appx:///Assets/mslogo.png");

            element.AppendChild(template.CreateTextNode(TextboxToastTextWithImage.Text.Trim()));

            var toast = new ToastNotification(template);
            notifier.Show(toast);
        }

        private void ButtonSendScheduledNotificationClick(object sender, RoutedEventArgs e)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);

            var element = template.GetElementsByTagName("text")[0];
            ((XmlElement)template.GetElementsByTagName("image")[0]).SetAttribute("src", "ms-appx:///Assets/mslogo.png");

            element.AppendChild(template.CreateTextNode(string.Format("This toast appeared after {0} seconds", TextboxTime.Text)));

            var date = DateTime.Now.AddSeconds(Convert.ToInt32(TextboxTime.Text));
            var scheduledToast = new ScheduledToastNotification(template, date, new TimeSpan(0, 0, 0, 30), 2);
            notifier.AddToSchedule(scheduledToast);
        }
    }
}
