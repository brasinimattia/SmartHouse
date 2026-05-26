using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlaisePascal.SmartHouse.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            LoadDefaultView();
            _serviceProvider = serviceProvider;
        }
        private void LoadDefaultView()
        {
            MainContentArea.Content = _serviceProvider.GetRequiredService<Views.LampView>();
        }
        private void NavLamps_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = _serviceProvider.GetRequiredService<Views.LampView>();
        }

        private void NavCctv_Click(object sender, RoutedEventArgs e)
        {
            // MainContentArea.Content = new Views.CctvView();
            MainContentArea.Content = _serviceProvider.GetRequiredService<Views.CCTVView>();
        }
        private void NavDoors_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = _serviceProvider.GetRequiredService<Views.DoorView>();
            //MessageBox.Show("Door View non ancora implementata.", "Info");
        }
    }
    
}