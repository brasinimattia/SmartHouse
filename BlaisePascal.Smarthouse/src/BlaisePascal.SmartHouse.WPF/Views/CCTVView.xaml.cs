using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries; 
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Dto;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.CCTVs.InMemory;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BlaisePascal.SmartHouse.WPF.Views
{
    public partial class CCTVView : UserControl
    {
    
        static ICCTVRepository _cctvRepository;
        private CCTVDto SelectedCCTV { get; set; } = null;

        public CCTVView()
        {
            InitializeComponent();

            if (_cctvRepository == null)
            {
                _cctvRepository = new InMemoryCCTVRepository();
            }

            RefreshCctvList();
        }

        private void RefreshCctvList()
        {
            var selectedId = SelectedCCTV?.Id;
            CctvList.Items.Clear();

            var cctvs = new GetAllCCTVQuery(_cctvRepository).Execute();

            foreach (var cctv in cctvs)
            {
                CctvList.Items.Add(cctv);
                if (cctv.Id == selectedId)
                    CctvList.SelectedItem = cctv;
            }
        }

        private void CctvList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CctvList.SelectedItem is CCTVDto selectedDto)
            {
                SelectedCCTV = selectedDto;
            }
        }

        // --- GESTIONE AGGIUNTA E RIMOZIONE ---

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewCctvNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name) || name == "Device Name")
                {
                    MessageBox.Show("Insert a valid CCTV name");
                    return;
                }

                new AddCCTVCommand(_cctvRepository).Execute(name);

                NewCctvNameTextBox.Text = "Device Name";
                NewCctvNameTextBox.Foreground = System.Windows.Media.Brushes.Gray;
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedCCTV == null) return;

                new RemoveCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id);
                SelectedCCTV = null;
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- GESTIONE STATO E MODALITA' ---

        private void On_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceCommand(() => new SwitchOnCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id));
        }

        private void Off_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceCommand(() => new SwitchOffCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id));
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceCommand(() => new ToggleCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id));
        }

        private void NormalMode_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceCommand(() => new SetNormalModeCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id));
        }

        private void NightMode_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceCommand(() => new SetNightModeCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id));
        }

        // --- GESTIONE REGISTRAZIONE ---

        private void StartRec_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceCommand(() => new StartRecordingCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id));
        }

        private void StopRec_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceCommand(() => new StopRecordingCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id));
        }

        // --- GESTIONE SICUREZZA (LOCK/UNLOCK/PASSWORD) ---

        private void Unlock_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceSecurityCommand((key) => new UnlockCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id, key));
        }

        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceSecurityCommand((key) => new LockCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id, key));
        }

        private void SetPassword_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeviceSecurityCommand((key) => new SetPasswordCCTVCommand(_cctvRepository).Execute(SelectedCCTV.Id, key));
        }

        // --- ORDINAMENTI ---

        private void SortName_Click(object sender, RoutedEventArgs e)
        {
            var cctvs = new GetAllCCTVQuery(_cctvRepository).Execute();
            var sorted = cctvs.OrderBy(c => c.Name).ToList();

            CctvList.Items.Clear();
            foreach (var cctv in sorted) CctvList.Items.Add(cctv);
        }

        private void SortStatus_Click(object sender, RoutedEventArgs e)
        {
            var cctvs = new GetAllCCTVQuery(_cctvRepository).Execute();
            var sorted = cctvs.OrderBy(c => c.Status).ToList();

            CctvList.Items.Clear();
            foreach (var cctv in sorted) CctvList.Items.Add(cctv);
        }

        // --- METODI HELPER ---

        private void ExecuteDeviceCommand(Action action)
        {
            if (SelectedCCTV == null)
            {
                MessageBox.Show("Please select a CCTV first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                action();
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteDeviceSecurityCommand(Action<string> action)
        {
            if (SelectedCCTV == null)
            {
                MessageBox.Show("Please select a CCTV first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                string key = KeyTextBox.Text.Trim();
                if (key == "Enter Password/Key") key = ""; // Gestione del placeholder

                action(key);
                RefreshCctvList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

 
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text == "Device Name" || textBox.Text == "Enter Password/Key")
                {
                    textBox.Text = "";
                    textBox.Foreground = System.Windows.Media.Brushes.Black;
                }
            }
        }
    }
}
