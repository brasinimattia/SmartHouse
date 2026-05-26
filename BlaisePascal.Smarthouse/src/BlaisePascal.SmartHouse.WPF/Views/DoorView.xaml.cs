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
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.AddDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.CloseDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.LockDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.OpenDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.RemoveDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SetPasswordDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries.GetAllDoor;
using BlaisePascal.SmartHouse.Domain.abstraction;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.Doors.InMemory;
using MediatR;

namespace BlaisePascal.SmartHouse.WPF.Views
{
    /// <summary>
    /// Logica di interazione per DoorView.xaml
    /// </summary>
    public partial class DoorView : UserControl
    {
        private readonly IMediator _mediator;

        private DoorDto SelectedDoor { get; set; } = null;

        public DoorView(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            //RefreshDoorList();
        }

        private async void RefreshDoorList()
        {
            var selectedId = SelectedDoor?.Id;
            DoorList.Items.Clear();

            var result = await _mediator.Send(new GetAllDoorQuery());

            if (result.IsFailure)
            {
                MessageBox.Show(result.Error.Code, "Error in locking door");
                return;
            }
            foreach (var door in result.Value)
            {
                DoorList.Items.Add(door);
                if (door.Id == selectedId)
                    DoorList.SelectedItem = door;
            }
        }

        private async void DoorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoorList.SelectedItem is DoorDto door)
            {
                SelectedDoor = door;
            }
        }

        // ADD DOOR
        private async void AddDoor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewDoorNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Insert a door name");
                    return;
                }

                var result = await _mediator.Send(new AddDoorCommand(name));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in adding door");
                    return;
                }

                NewDoorNameTextBox.Clear();
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // OPEN DOOR
        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                var result = await _mediator.Send(new OpenDoorCommand(SelectedDoor.Id));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in opening door");
                    return;
                }
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // CLOSE DOOR
        private async void Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                var result = await _mediator.Send(new CloseDoorCommand(SelectedDoor.Id));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in closing door");
                    return;
                }
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // LOCK DOOR
        private async void Lock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                string key = DoorPasswordBox.Password; // Legge dal PasswordBox della UI
                var result = await _mediator.Send(new LockDoorCommand(SelectedDoor.Id, key));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in locking door");
                    return;
                }
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Security Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // UNLOCK DOOR
        private async void Unlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                string key = DoorPasswordBox.Password;
                var result = await _mediator.Send(new LockDoorCommand(SelectedDoor.Id, key));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in unlocking door");
                    return;
                }
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Security Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SET PASSWORD
        private async void SetPassword_Click(object sender, RoutedEventArgs e)
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

                var result = await _mediator.Send(new SetPasswordDoorCommand(SelectedDoor.Id, key));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in setting new password");
                    return;
                }
                DoorPasswordBox.Clear();
                RefreshDoorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // REMOVE DOOR
        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDoor == null) return;
                var result = await _mediator.Send(new RemoveDoorCommand(SelectedDoor.Id));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in removing door");
                    return;
                }
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
