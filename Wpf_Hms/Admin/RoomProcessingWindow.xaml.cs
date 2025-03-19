using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for RoomProcessingWindow.xaml
    /// </summary>
    public partial class RoomProcessingWindow : Window
    {
        private readonly IService<RoomInformation> _RoomInformationService;

        private readonly IService<RoomType> _RoomTypeService;

        private int newRoomId;

        private bool isCreateAction;

        private DataGrid dgHotel;

        public RoomProcessingWindow(bool isCreateAction, RoomInformation roomInformation, DataGrid dgHotel)
        {
            InitializeComponent();
            _RoomInformationService = RoomInformationService.GetInstance();
            _RoomTypeService = RoomTypeService.GetInstance();
            this.isCreateAction = isCreateAction;
            this.dgHotel = dgHotel;
            SetDefaultData(roomInformation);
        }

        private void SetDefaultData(RoomInformation roomInformation)
        {
            LoadRoomStatus();
            LoadRoomType();

            if (isCreateAction)
            {
                List<RoomInformation> roomInformations = _RoomInformationService.GetAll();
                newRoomId = roomInformations.Count + 1;

                bool isNotExisted = roomInformations.FirstOrDefault(x => x.RoomID == newRoomId) == null;

                if (isNotExisted)
                {
                    lbTitle.Content = "Create Hotel Room";
                    txtRoomId.Text = newRoomId.ToString();
                } 
                else
                {
                    MessageBox.Show("There is an error!");
                }
            }
            else
            {
                lbTitle.Content = "Update Hotel Room";
                txtRoomId.Text = roomInformation.RoomID.ToString();
                txtRoomNumber.Text = roomInformation.RoomNumber?.ToString();
                txtCapacity.Text = roomInformation.RoomMaxCapacity.ToString();
                txtRoomDescription.Text = roomInformation.RoomDescription?.ToString();
                txtPrice.Text = roomInformation.RoomPricePerDate.ToString();
                cbxStatus.SelectedValue = (int)roomInformation.RoomStatus;
                cbxRoomType.SelectedValue = roomInformation.RoomType?.RoomTypeID;
            }
        }

        private void LoadRoomStatus()
        {
            var roomStatus = Enum.GetValues(typeof(RoomStatus))
                            .Cast<RoomStatus>()
                            .Select(x => new {RoomStatusId = (int)x, RoomStatusName = x.ToString()});

            cbxStatus.ItemsSource = roomStatus;
            cbxStatus.SelectedValuePath = "RoomStatusId";
            cbxStatus.DisplayMemberPath = "RoomStatusName";
        }

        private void LoadRoomType()
        {
            List<RoomType> roomTypes = _RoomTypeService.GetAll();
            cbxRoomType.ItemsSource = roomTypes;
            cbxRoomType.SelectedValuePath = "RoomTypeID";
            cbxRoomType.DisplayMemberPath = "RoomTypeName";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = new()
            {
                RoomID = int.Parse(txtRoomId.Text),
                RoomNumber = txtRoomNumber.Text,
                RoomDescription = txtRoomDescription.Text,
                RoomMaxCapacity = int.Parse(txtCapacity.Text),
                RoomStatus = (RoomStatus)cbxStatus.SelectedValue,
                RoomPricePerDate = decimal.Parse(txtPrice.Text),
                RoomTypeID = (int)cbxRoomType.SelectedValue
            };

            if (isCreateAction)
            {   
                var dataAdd = _RoomInformationService.Add(roomInformation);
                
                if (dataAdd != null)
                {
                    MessageBox.Show("Create successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There is an error!");
                }
            } 
            else
            {
                var dataUpdate = _RoomInformationService.Update(roomInformation);

                if (dataUpdate != null)
                {
                    MessageBox.Show("Update successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There is an error!");
                }
            }

            dgHotel.ItemsSource = _RoomInformationService.GetAll();
            dgHotel.Items.Refresh();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
