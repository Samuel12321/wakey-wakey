using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wakey_Wakey.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        List<Classes.NetworkDevice> SavedDevicesList = new List<Classes.NetworkDevice>() { };
        public Home()
        {
            this.InitializeComponent();
            UpdateList();

            DevicesListView.SelectionChanged += yourListView_ItemSelectionChanged;
        }
        private async void WakeUp(string mac)
        {
            await Classes.WakeOnLan.Wake(mac);
        }
        private void yourListView_ItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void UpdateList()
        {
            for (int i = 1; i < 51; i++)
            {
                if (localSettings.Values.ContainsKey("computer_" + i))
                {
                    string[] raw_device = localSettings.Values["computer_" + i].ToString().Split(';');
                    int.TryParse(raw_device[4], out int port);

                    SavedDevicesList.Add(new Classes.NetworkDevice
                    {
                        id = i,
                        port = port,
                        name = raw_device[0],
                        ip = raw_device[1],
                        mac = raw_device[2],
                        subnet = raw_device[3]
                    });
                }
            }

            if (SavedDevicesList.Count < 1)
                textblock_nodevices.Visibility = Visibility.Visible;

            DevicesListView.ItemsSource = SavedDevicesList;
        }
        private void Button_PowerOn_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse((sender as Button).Tag.ToString(), out int id);

            foreach (Classes.NetworkDevice device in SavedDevicesList)
            {
                if (device.id == id)
                {
                    WakeUp(device.mac);
                }
            }
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse((sender as Button).Tag.ToString(), out int id);

            if (!localSettings.Values.ContainsKey("computer_" + id))
                return;

            localSettings.Values.Remove("computer_" + id);

            Frame.BackStack.Clear();
            Frame.Navigate(typeof(Views.Home));
        }
        private void Button_AddManually_Click(object sender, RoutedEventArgs e)
        {
            Frame.BackStack.Clear();
            Frame.Navigate(typeof(Views.AddManually));
        }
        private void DevicesListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            DevicesListView.ItemsSource = SavedDevicesList;
            DevicesListView.UpdateLayout();
        }
    }
}
