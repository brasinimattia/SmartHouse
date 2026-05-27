using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.AddCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.DetNightModeCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.lockCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.RemoveCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetNormalModeCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetPasswordCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.StartRecordingCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.StopRecordingCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SwitchOffCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SwitchOnCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.ToggleCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.UnlockCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries; 
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries.GetAllCCTV;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.CCTVs.InMemory;
using MediatR;

namespace BlaisePascal.SmartHouse.WPF.Views
{
    public partial class CCTVView : UserControl
    {
        static IMediator _mediator;

        private CCTVDto SelectedCCTV { get; set; } = null;

        public CCTVView(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            RefreshCctvList();
        }

        private async void RefreshCctvList()
        {
            var selectedId = SelectedCCTV?.Id;
            CctvList.Items.Clear();

            var result = await _mediator.Send(new GetAllCCTVQuery());
            if (result.IsFailure)
            {
                MessageBox.Show(result.Error.Code, "Error in Refreshing CCTV list");
                return;
            }

            foreach (var cctv in result.Value)
            {
                CctvList.Items.Add(cctv);
                if (cctv.Id == selectedId)
                    CctvList.SelectedItem = cctv;
            }
        }

        private async void CctvList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CctvList.SelectedItem is CCTVDto cctv)
            {
                SelectedCCTV = cctv;
            }
        }

        // ADD CCTV
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewCctvNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Insert a CCTV name");
                    return;
                }

                var result = await _mediator.Send(new AddCCTVCommand(name));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Adding CCTV");
                    return;
                }

                NewCctvNameTextBox.Clear();
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // REMOVE CCTV
        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new RemoveCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in removing CCTV");
                    return;
                }
                SelectedCCTV = null;
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SWITCH ON
        private async void On_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new SwitchOnCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Switching On CCTV");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SWITCH OFF
        private async void Off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new SwitchOffCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Switching Off CCTV");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // TOGGLE
        private async void Toggle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new ToggleCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Toggling CCTV");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // NORMAL MODE
        private async void NormalMode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new SetNormalModeCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Setting Normal Mode");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // NIGHT MODE
        private async void NightMode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new SetNightModeCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Setting Night Mode");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // START RECORDING
        private async void StartRec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new StartRecordingCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Starting Recording");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // STOP RECORDING
        private async void StopRec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                var result = await _mediator.Send(new StopRecordingCCTVCommand(SelectedCCTV.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in Stopping Recording");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // LOCK CCTV
        private async void Lock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                string key = KeyTextBox.Text.Trim();
                var result = await _mediator.Send(new LockCCTVCommand(SelectedCCTV.Id, key));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in locking CCTV");
                    return;
                }
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Security Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // UNLOCK CCTV
        private async void Unlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;
                string key = KeyTextBox.Text.Trim();
                var result = await _mediator.Send(new UnlockCCTVCommand(SelectedCCTV.Id, key));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in unlocking CCTV");
                    return;
                }
                RefreshCctvList();
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
                if (SelectedCCTV == null) return;
                string key = KeyTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(key))
                {
                    MessageBox.Show("Password cannot be empty");
                    return;
                }

                var result = await _mediator.Send(new SetPasswordCCTVCommand(SelectedCCTV.Id, key));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error in setting password");
                    return;
                }
                KeyTextBox.Clear();
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SORT BY NAME
        private async void SortName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await _mediator.Send(new GetAllCCTVQuery());
                if (result.IsFailure) return;

                var sorted = result.Value.OrderBy(c => c.Name).ToList();

                CctvList.Items.Clear();
                foreach (var cctv in sorted)
                {
                    CctvList.Items.Add(cctv);
                    if (SelectedCCTV != null && cctv.Id == SelectedCCTV.Id)
                        CctvList.SelectedItem = cctv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SORT BY STATUS
        private async void SortStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await _mediator.Send(new GetAllCCTVQuery());
                if (result.IsFailure) return;

                var sorted = result.Value.OrderBy(c => c.Status).ToList();

                CctvList.Items.Clear();
                foreach (var cctv in sorted)
                {
                    CctvList.Items.Add(cctv);
                    if (SelectedCCTV != null && cctv.Id == SelectedCCTV.Id)
                        CctvList.SelectedItem = cctv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    }
