using System.Windows;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class RoomDetailWindow : Window
    {
        public int CountRoom { get; set; }

        public RoomDetailWindow(int countRoom)
        {
            InitializeComponent();
            CountRoom = ++countRoom;
            SetDefaultData();
        }

        private void SetDefaultData()
        {
            txtRoomId.Text = GenerateRoomId(CountRoom);
        }

        private string GenerateRoomId(int countRoom)
        {
            string roomId = "000";

            roomId = countRoom switch
            {
                < 10 => $"00{countRoom}",
                < 100 => $"0{countRoom}",
                < 1000 => $"{countRoom}",
                _ => string.Empty
            };

            return roomId;
        }

    }
}
