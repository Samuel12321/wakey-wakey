using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;

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

            // Bug 23809028: Window.Current.SetTitleBar is not available in MUX [XamlControlsGallery]
            // Window.Current.SetTitleBar(AppTitleBar);
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += (s, e) => UpdateAppTitle(s);
        }

        public void Navigate()
        {
            rootFrame.Navigate(typeof(Views.AddManually));
        }

        void UpdateAppTitle(CoreApplicationViewTitleBar coreTitleBar)
        {
            var full = (ApplicationView.GetForCurrentView().IsFullScreenMode);
            var left = 12 + (full ? 0 : coreTitleBar.SystemOverlayLeftInset);
            AppTitle.Margin = new Thickness() { Left = left, Top = 8, Right = 0, Bottom = 0 };
            AppTitleBar.Height = coreTitleBar.Height;
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
            rootFrame.Navigate(typeof(Views.Home));
        }

        private void NavigationViewControl_Invoke(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                rootFrame.Navigate(typeof(Views.Settings));
            }
            else
            {
                switch (args.InvokedItemContainer.Tag)
                {
                    case "devicesPage":
                        rootFrame.Navigate(typeof(Views.Home));
                        break;
                    case "searchPage":
                        rootFrame.Navigate(typeof(Views.Search));
                        break;
                }
            }
        }

        private void NavigationViewControl_Selection(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
        }

        private void NavigationViewControl_PaneClosing(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewPaneClosingEventArgs args)
        {
            UpdateAppTitleMargin(sender);
        }

        private void NavigationViewControl_PaneOpened(Microsoft.UI.Xaml.Controls.NavigationView sender, object args)
        {
            UpdateAppTitleMargin(sender);
        }

        private void UpdateAppTitleMargin(Microsoft.UI.Xaml.Controls.NavigationView sender)
        {
            const int smallLeftIndent = 0, largeLeftIndent = 42;

            AppTitle.TranslationTransition = new Vector3Transition();

            if (sender.IsPaneOpen == false && (sender.DisplayMode == Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Expanded ||
                sender.DisplayMode == Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Compact))
            {
                AppTitle.Translation = new System.Numerics.Vector3(largeLeftIndent, 0, 0);
            }
            else
            {
                AppTitle.Translation = new System.Numerics.Vector3(smallLeftIndent, 0, 0);
            }
        }
    }
}
