using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RoomInformationWindow : Window
    {
        private readonly IService<RoomInformation> _Service;
       
        public RoomInformationWindow()
        {
            InitializeComponent();
            _Service = new RoomInformationService();
            LoadRoomInformation();
        }

        private void LoadRoomInformation()
        {
            var roomsInfor = _Service.GetAll();

            dgHotel.ItemsSource = roomsInfor;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            RoomDetailWindow roomDetailWindow = new(true, null!, dgHotel);
            roomDetailWindow.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = (RoomInformation)dgHotel.SelectedItem;
            if(roomInformation == null)
            {
                MessageBox.Show("Please select a room to update");
                return;
            }
            RoomDetailWindow roomDetailWindow = new(false, roomInformation, dgHotel);
            roomDetailWindow.Show();
        }
    }
}