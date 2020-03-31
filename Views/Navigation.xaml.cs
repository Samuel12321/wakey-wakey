using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wakey_Wakey.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Navigation : Page
    {
        public static Navigation Current;
        public static Frame RootFrame = null;
        public Navigation()
        {
            this.InitializeComponent();

            Current = this;
            RootFrame = rootFrame;
        }

        public void Navigate()
        {
            rootFrame.Navigate(typeof(Views.AddManually));
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            // Close any open teaching tips before navigation
            //CloseTeachingTips();

            /*
            if (e.SourcePageType == typeof(AllControlsPage) ||
                e.SourcePageType == typeof(NewControlsPage))
            {
                NavigationViewControl.AlwaysShowHeader = false;
            }
            else
            {
                NavigationViewControl.AlwaysShowHeader = true;

                bool isFilteredPage = e.SourcePageType == typeof(SectionPage) || e.SourcePageType == typeof(SearchResultsPage);
                PageHeader?.UpdateBackground(isFilteredPage);
            }
            */
        }

        private void NavigationViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in mainNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "devicesPage")
                {
                    mainNav.SelectedItem = item;
                    break;
                }
            }
            rootFrame.BackStack.Clear();
            rootFrame.Navigate(typeof(Views.Home));
        }

        private void NavigationViewControl_Invoke(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                rootFrame.BackStack.Clear();
                rootFrame.Navigate(typeof(Views.Settings));
            }
            else
            {
                rootFrame.BackStack.Clear();

                switch (args.InvokedItemContainer.Tag)
                {
                    case "devicesPage":
                        rootFrame.Navigate(typeof(Views.Home));
                        break;
                    case "searchPage":
                        //rootFrame.Navigate(typeof(Views.Search));
                        break;
                }
            }
        }
        private void NavigationViewControl_Selection(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
        }

    }
}
