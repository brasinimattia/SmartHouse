using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.AddLamp;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.ChangeBrightness;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.RemoveLamp;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOff;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOn;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json;
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

namespace BlaisePascal.SmartHouse.WPF.Views
{
    /// <summary>
    /// Logica di interazione per LampView.xaml
    /// </summary>
    public partial class LampView : UserControl
    {
        static ILampRepository _lampRepository;

        private LampDto SelectedLamp { get; set; } = null;

        public LampView()
        {
            InitializeComponent();
            _lampRepository = new JsonLampRepository();
            RefreshLampList();
        }

        private void RefreshLampList()
        {
            var selectedId = SelectedLamp?.Id;
            LampList.Items.Clear();

            var lamps = new GetAllLampsQuery(_lampRepository).Execute();
            foreach (var lamp in lamps)
            {
                LampList.Items.Add(lamp);
                if (lamp.Id == selectedId)
                    LampList.SelectedItem = lamp;
            }
        }


        private void LampList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = LampList.SelectedIndex;
            var lamps = new GetAllLampsQuery(_lampRepository).Execute();

            if (index >= 0 && index < lamps.Count)
                SelectedLamp = lamps[index];
        }

        //CHANGE BRIGHTNESS VIA SLIDER
        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (BrightnessPercentageText != null) BrightnessPercentageText.Text = $"{(int)e.NewValue}%";
        }


        // ADD LAMP
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewLampNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Insert a lamp name");
                    return;
                }

                new AddLampCommand(_lampRepository).Execute(name);

                if (int.TryParse(NewLampIntensityTextBox.Text.Trim(), out int intensity))
                {
                    var addedLamp = new GetAllLampsQuery(_lampRepository).Execute().Last();
                    new SwitchOnLampCommand(_lampRepository).Execute(addedLamp.Id);
                    new ChangeBrightnessLampCommand(_lampRepository).Execute(addedLamp.Id, intensity);
                }

                NewLampNameTextBox.Clear();
                NewLampIntensityTextBox.Clear();
                RefreshLampList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SWITCH ON
        private void On_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null) return;
                new SwitchOnLampCommand(_lampRepository).Execute(SelectedLamp.Id);
                RefreshLampList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SWITCH OFF
        private void Off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null) return;
                new SwitchOffLampCommand(_lampRepository).Execute(SelectedLamp.Id);
                RefreshLampList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SET BRIGHTNESS
        private void ApplyIntensity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp != null)
                {
                    new ChangeBrightnessLampCommand(_lampRepository).Execute(SelectedLamp.Id, (int)BrightnessSlider.Value);
                    RefreshLampList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // REMOVE LAMP
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null) return;
                new RemoveLampCommand(_lampRepository).Execute(SelectedLamp.Id);
                SelectedLamp = null;
                RefreshLampList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SORT LAMPS BY NAME
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            var lamps = new GetAllLampsQuery(_lampRepository).Execute();
            var sortedLamps = lamps.OrderBy(l => l.Name).ToList();
            LampList.Items.Clear();
            foreach (var lamp in sortedLamps)
                LampList.Items.Add(lamp);
        }

        private void Sort_Click_ByIntensity(object sender, RoutedEventArgs e)
        {
            var lamps = new GetAllLampsQuery(_lampRepository).Execute();
            var sortedLamps = lamps.OrderByDescending(l => l.Brightness).ToList();
            LampList.Items.Clear();
            foreach (var lamp in sortedLamps)
                LampList.Items.Add(lamp);
        }
    }
}

