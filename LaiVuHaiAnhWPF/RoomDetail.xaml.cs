using BusinessObjects.Models;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaiVuHaiAnhWPF
{
    /// <summary>
    /// Interaction logic for RoomDetail.xaml
    /// </summary>
    public partial class RoomDetail : Window
    {
        private RoomTypeService roomTypeRepository;
        public RoomDetail()
        {
            InitializeComponent();
            roomTypeRepository = new RoomTypeRepository();
        }
        public RoomInformationService RoomInformationRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public RoomInformation RoomInformation { get; set; }

        private void LoadRoomType()
        {
            try
            {
                var roomTypeList = roomTypeRepository.GetRoomTypes();
                cboRoomType.ItemsSource = roomTypeList;
                cboRoomType.DisplayMemberPath = "RoomTypeName";
                cboRoomType.SelectedValuePath = "RoomTypeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of room type");
            }

        }

        private void LoadStatus()
        {
            var statuses = new List<Status>()
            {
                new Status { StatusID = 1, StatusName = "Active" },
                new Status { StatusID = 2, StatusName = "Inactive" }
            };

            cboStatus.ItemsSource = statuses;
            cboStatus.DisplayMemberPath = "StatusName";
            cboStatus.SelectedValuePath = "StatusID";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRoomType();
            LoadStatus();
            cboStatus.SelectedIndex = 0;
            if (InsertOrUpdate)
            {
                txtRoomID.Text = RoomInformation.RoomId.ToString();
                txtDescription.Text = RoomInformation.RoomDetailDescription;
                txtPrice.Text = RoomInformation.RoomPricePerDay.ToString();
                txtCapacity.Text = RoomInformation.RoomMaxCapacity.ToString();
                txtRoomNumber.Text = RoomInformation.RoomNumber.ToString();
                cboStatus.SelectedValue = RoomInformation.RoomStatus;
                cboRoomType.SelectedValue = RoomInformation.RoomTypeId;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string description = txtDescription.Text;
                int capacity = int.Parse(txtCapacity.Text);
                string number = txtRoomNumber.Text;
                decimal price = decimal.Parse(txtPrice.Text);
                int roomType = (int)cboRoomType.SelectedValue;
                byte status = (byte)cboStatus.SelectedValue;

                RoomInformation roomInformation = new RoomInformation()
                {
                    RoomDetailDescription = description,
                    RoomMaxCapacity = capacity,
                    RoomPricePerDay = price,
                    RoomNumber = number,
                    RoomStatus = status,
                    RoomTypeId = roomType
                };

                // Here you can call a method to save the customer
                if (InsertOrUpdate)
                {
                    int roomID = int.Parse(txtRoomID.Text);
                    roomInformation.RoomId = roomID;
                    RoomInformationRepository.UpdateRoomInformation(roomInformation);
                    this.Close();
                }
                else
                {
                    RoomInformationRepository.SaveRoomInformation(roomInformation);
                    this.Close();
                }
                MessageBox.Show("Room saved successfully!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving room: {ex.Message}", "Error");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => Close();

        private void txtRoomNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtRoomNumber_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return text.All(char.IsDigit);
        }

        private static bool IsTextAllowed(string text, TextBox textBox)
        {
            string currentText = textBox.Text.Insert(textBox.SelectionStart, text);
            return Regex.IsMatch(currentText, @"^\d*\.?\d*$");
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, (TextBox)sender);
        }

        private void txtPrice_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                TextBox textBox = (TextBox)sender;
                if (!IsTextAllowed(text, textBox))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
