using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.Doors.InMemory;

namespace BlaisePascal.SmartHouse.WPF.Views
{
    /// <summary>
    /// Logica di interazione per DoorView.xaml
    /// </summary>
    public partial class DoorView : UserControl
    {
        static IDoorRepository _doorRepository;

        private DoorDto SelectedDoor { get; set; } = null;

        public DoorView()
        {
            InitializeComponent();
            _doorRepository = new InMemoryDoorRepository();
            RefreshDoorList();
        }

        private void RefreshDoorList()
        {
            var selectedId = SelectedDoor?.Id;
            DoorList.Items.Clear();

            var doors = new GetAllDoorQuery(_doorRepository).Execute();
            foreach (var door in doors)
            {
                DoorList.Items.Add(door);
                if (door.Id == selectedId)
                    DoorList.SelectedItem = door;
            }
        }

        private void DoorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoorList.SelectedItem is DoorDto door)
            {
                SelectedDoor = door;
            }
        }

        // ADD DOOR
        private void AddDoor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewDoorNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Insert a door name");
                    return;
                }

                new AddDoorCommand(_doorRepository).Execute(name);

                NewDoorNameTextBox.Clear();
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // OPEN DOOR
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                new OpenDoorCommand(_doorRepository).Execute(SelectedDoor.Id);
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // CLOSE DOOR
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                new CloseDoorCommand(_doorRepository).Execute(SelectedDoor.Id);
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // LOCK DOOR
        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                string key = DoorPasswordBox.Password; // Legge dal PasswordBox della UI
                new LockDoorCommand(_doorRepository).Execute(SelectedDoor.Id, key);
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Security Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // UNLOCK DOOR
        private void Unlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                string key = DoorPasswordBox.Password;
                new UnlockDoorCommand(_doorRepository).Execute(SelectedDoor.Id, key);
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Security Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SET PASSWORD
        private void SetPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                string key = DoorPasswordBox.Password;

                if (string.IsNullOrWhiteSpace(key))
                {
                    MessageBox.Show("Password cannot be empty");
                    return;
                }

                new SetPasswordDoorCommand(_doorRepository).Execute(SelectedDoor.Id, key);
                DoorPasswordBox.Clear();
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // REMOVE DOOR
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                new RemoveDoorCommand(_doorRepository).Execute(SelectedDoor.Id);
                SelectedDoor = null;
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
