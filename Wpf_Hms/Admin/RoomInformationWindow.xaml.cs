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
            int countRoom = _Service.Count();

            RoomDetailWindow roomDetailWindow = new RoomDetailWindow(countRoom);
            roomDetailWindow.Show();
        }
    }
}