using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Text.RegularExpressions;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wakey_Wakey.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddManually : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public AddManually()
        {
            this.InitializeComponent();

            //TextBoxMask.SetMask(textfield_name, "00-00");

            //extensions:TextBoxMask.Mask="9a9a-a9a*"
            //textfield_name.mask = "";

            
        }
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (textfield_name.Text.Length < 2)
            {
                FormErrorTip.Subtitle = "You must enter a computer name";
                FormErrorTip.IsOpen = true;
                return;
            }
            if (textfield_address.Text.Length > 0 && textfield_address6.Text.Length > 0)
            {
                FormErrorTip.Subtitle = "You must specify only one IP address";
                FormErrorTip.IsOpen = true;
                return;
            }
            if (textfield_address.Text.Length < 11 && textfield_address6.Text.Length == 0 || textfield_address6.Text.Length < 21 && textfield_address.Text.Length == 0)
            {
                FormErrorTip.Subtitle = "The IP address provided is too short";
                FormErrorTip.IsOpen = true;
                return;
            }
            if (textfield_mac.Text.Length < 12)
            {
                FormErrorTip.Subtitle = "The MAC address provided is too short";
                FormErrorTip.IsOpen = true;
                return;
            }
            if (textfield_subnet.Text.Length < 11)
            {
                FormErrorTip.Subtitle = "The subnet mask provided is too short";
                FormErrorTip.IsOpen = true;
                return;
            }
            if (textfield_port.Text.Length < 1)
            {
                FormErrorTip.Subtitle = "You must provide a port number";
                FormErrorTip.IsOpen = true;
                return;
            }

            for (int i = 1; i < 51; i++)
            {
                if (!localSettings.Values.ContainsKey("computer_" + i))
                {
                    string ip = textfield_address.Text;
                    if (ip.Length < 1)
                        ip = textfield_address6.Text;

                    localSettings.Values["computer_" + i] = textfield_name.Text + ";" + ip + ";" + textfield_mac.Text + ";" + textfield_subnet.Text + ";" + textfield_port.Text;
                    break;
                }
            }

            Frame.Navigate(typeof(Views.Home));
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.Home));
        }
        private void TextName_BeforeTextChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //string text = textfield_address.Text;
            //int length = text.Length;

            Regex rgx = new Regex(@"^[-|a-zA-Z0-9\s]");
            args.Cancel = args.NewText.Any(c => !(rgx.IsMatch(c.ToString())));
        }
        private void TextAddress_BeforeTextChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !(char.IsDigit(c) || c == '.'));
        }
        private void TextAddress6_BeforeTextChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
        {
            Regex rgx = new Regex(@"^[:a-fA-F0-9]");
            args.Cancel = args.NewText.Any(c => !(rgx.IsMatch(c.ToString())));
        }
        private void TextMac_BeforeTextChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //(sender as TextBox).Text = (sender as TextBox).Text.Replace('.', '-');
            Regex rgx = new Regex(@"^[-a-zA-Z0-9]");
            args.Cancel = args.NewText.Any(c => !(rgx.IsMatch(c.ToString())));
        }
        private void TextSubnet_BeforeTextChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !(char.IsDigit(c) || c == '.'));
        }
        
        private void TextPort_BeforeTextChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !(char.IsDigit(c)));
        }
    }
}
