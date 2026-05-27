using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.AddLamp;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.ChangeBrightness;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.RemoveLamp;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOff;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOn;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries.GetAll;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json;
using MediatR;
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
        static IMediator _mediator;

        private LampDto SelectedLamp { get; set; } = null;

        public LampView(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;   
            RefreshLampList();
        }

        private async void RefreshLampList()
        {
            var selectedId = SelectedLamp?.Id;
            LampList.Items.Clear();

            var result = await _mediator.Send(new GetAllLampsQuery());
            foreach (var lamp in result.Value)
            {
                LampList.Items.Add(lamp);
                if (lamp.Id == selectedId)
                    LampList.SelectedItem = lamp;
            }
        }


        private async void LampList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = LampList.SelectedIndex;
            var result = await _mediator.Send(new GetAllLampsQuery());

            if (index >= 0 && index < result.Value.Count)
                SelectedLamp = result.Value[index];
        }

        //CHANGE BRIGHTNESS VIA SLIDER
        private async void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (BrightnessPercentageText != null) BrightnessPercentageText.Text = $"{(int)e.NewValue}%";
        }


        // ADD LAMP
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewLampNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Insert a lamp name");
                    return;
                }

                var result = await _mediator.Send(new AddLampCommand(name));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error Adding Lamp");
                    return;
                }

                if (int.TryParse(NewLampIntensityTextBox.Text.Trim(), out int intensity))
                {
                    var lamps = await _mediator.Send(new GetAllLampsQuery());
                    var addedLamp = lamps.Value.Last();
                    var switchon = await _mediator.Send(new SwitchOnLampCommand(addedLamp.Id));
                    var changebrightness = await _mediator.Send(new ChangeBrightnessLampCommand(addedLamp.Id, intensity));
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
        private async void On_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null) return;
                var result = await _mediator.Send(new SwitchOnLampCommand(SelectedLamp.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error Switching On Lamp");
                    return;
                }
                RefreshLampList();
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
                if (SelectedLamp == null) return;
                var result = await _mediator.Send(new SwitchOffLampCommand(SelectedLamp.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error Switching Off Lamp");
                    return;
                }
                RefreshLampList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SET BRIGHTNESS
        private async void ApplyIntensity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp != null)
                {

                    var result = await _mediator.Send(new ChangeBrightnessLampCommand(SelectedLamp.Id, (int)BrightnessSlider.Value));
                    if (result.IsFailure)
                    {
                        MessageBox.Show(result.Error.Code, "Error Changing Brightness");
                        return;
                    }
                    RefreshLampList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // REMOVE LAMP
        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null) return;
                var result = await _mediator.Send(new RemoveLampCommand(SelectedLamp.Id));
                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error Removing Lamp");
                    return;
                }
                SelectedLamp = null;
                RefreshLampList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SORT LAMPS BY NAME
        private async void Sort_Click(object sender, RoutedEventArgs e)
        {
            var lamps = await _mediator.Send(new GetAllLampsQuery());
            var sortedLamps = lamps.Value.OrderBy(l => l.Name).ToList();
            LampList.Items.Clear();
            foreach (var lamp in sortedLamps)
                LampList.Items.Add(lamp);
        }

        private async void Sort_Click_ByIntensity(object sender, RoutedEventArgs e)
        {
            var lamps = await _mediator.Send(new GetAllLampsQuery());
            var sortedLamps = lamps.Value.OrderByDescending(l => l.Brightness).ToList();
            LampList.Items.Clear();
            foreach (var lamp in sortedLamps)
                LampList.Items.Add(lamp);
        }
    }
}