using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Settings : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public Settings()
        {
            this.InitializeComponent();
            FillSettings();
            //ApplicationLanguages.PrimaryLanguageOverride = "en";
        }

        private void FillSettings()
        {
            if(localSettings.Values.ContainsKey("language"))
            {
                if(localSettings.Values["language"].ToString() == "en")
                {
                    ComboBoxLanguage.SelectedIndex = 0;
                }
                else
                {
                    ComboBoxLanguage.SelectedIndex = 1;
                }
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBoxLanguage.SelectedIndex == 0)
            {
                localSettings.Values["language"] = "en";
            }
            else
            {
                localSettings.Values["language"] = "pl";
            }

            title_settingsinfo.Visibility = Visibility.Visible;

            //CoreApplication.RequestRestartAsync(string.Empty);
        }
    }
}
