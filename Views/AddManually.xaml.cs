using System.Linq;
using System.Text.RegularExpressions;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
                textfield_error.Text = "You must enter a computer name";
                return;
            }
            if (textfield_address.Text.Length > 0 && textfield_address6.Text.Length > 0)
            {
                textfield_error.Text = "You must specify only one IP address";
                return;
            }
            if (textfield_address.Text.Length < 11 && textfield_address6.Text.Length == 0 || textfield_address6.Text.Length < 21 && textfield_address.Text.Length == 0)
            {
                textfield_error.Text = "The IP address provided is too short";
                return;
            }
            if (textfield_mac.Text.Length < 12)
            {
                textfield_error.Text = "The MAC address provided is too short";
                return;
            }
            if (textfield_subnet.Text.Length < 11)
            {
                textfield_error.Text = "The subnet mask provided is too short";
                return;
            }
            if (textfield_port.Text.Length < 1)
            {
                textfield_error.Text = "You must provide a port number";
                return;
            }

            textfield_error.Text = "";

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

            Frame.BackStack.Clear();
            Frame.Navigate(typeof(Views.Home));
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.BackStack.Clear();
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
            Regex rgx = new Regex(@"^[:a-zA-Z0-9]");
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
