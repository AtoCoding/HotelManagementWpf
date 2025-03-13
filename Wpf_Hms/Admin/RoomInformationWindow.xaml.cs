using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using Wpf_Hms.Admin;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RoomInformationWindow : Window
    {
        private readonly IService<RoomInformation> _RoomInformationService;
       
        public RoomInformationWindow()
        {
            InitializeComponent();
            _RoomInformationService = new RoomInformationService();
            LoadRoomInformation();
        }

        private void LoadRoomInformation()
        {
            var roomsInfor = _RoomInformationService.GetAll();

            dgHotel.ItemsSource = roomsInfor;
            dgHotel.Items.Refresh();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            RoomDetailWindow roomDetailWindow = new(true, null!, dgHotel);
            roomDetailWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = (RoomInformation)dgHotel.SelectedItem;
            if (roomInformation == null)
            {
                MessageBox.Show("Please select a room to update");
                return;
            }
            RoomDetailWindow roomDetailWindow = new(false, roomInformation, dgHotel);
            roomDetailWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you really want to delete this room?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                RoomInformation roomInformation = (RoomInformation)dgHotel.SelectedItem;

                if (roomInformation == null)
                {
                    MessageBox.Show("Please select a room to delete");
                    return;
                }

                if (_RoomInformationService.Delete(roomInformation.RoomID))
                {
                    MessageBox.Show("Delete successfully");
                }
                else
                {
                    MessageBox.Show("Delete failed");
                }
                LoadRoomInformation();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OptionWindow optionWindow = new(true);
            optionWindow.Show();
            this.Close();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}