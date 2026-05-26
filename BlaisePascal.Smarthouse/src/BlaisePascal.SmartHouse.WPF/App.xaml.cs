using System.Configuration;
using System.Data;
using System.Windows;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.AddCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.DetNightModeCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.lockCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.RemoveCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetModeCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetNewNameCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetNormalModeCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetPasswordCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.StartRecordingCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.StopRecordingCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SwitchOffCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SwitchOnCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.ToggleCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.UnlockCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries.GetAllCCTV;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries.GetCCTVById;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.AddDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.CloseDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.LockDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.OpenDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.RemoveDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SetPasswordDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SwitchOffDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SwitchOnDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.UnlockDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries.GetAllDoor;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries.GetDoorById;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.AddLamp;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.ChangeBrightness;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.RemoveLamp;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOff;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOn;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries.GetAll;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries.GetById;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.CCTVs.Json;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.Doors.Json;
using BlaisePascal.SmartHouse.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlaisePascal.SmartHouse.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddLogging(cfg =>
            {
                cfg.AddDebug(); // manda i log alla finestra "Output" di Visual Studio
            });

            services.AddSingleton<ILampRepository, JsonLampRepository>();
            services.AddSingleton<IDoorRepository, JsonDoorRepository>();
            services.AddSingleton<ICCTVRepository, JsonCCTVRepository>();

            services.AddMediatR(cfg =>
            {
                //LAMP
                cfg.RegisterServicesFromAssembly(typeof(AddLampCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RemoveLampCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ChangeBrightnessLampCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SwitchOffLampCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SwitchOnLampCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLampsQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetLampByIdQuery).Assembly);
                //DOOR
                cfg.RegisterServicesFromAssembly(typeof(AddDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RemoveDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CloseDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(LockDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(OpenDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SwitchOffDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SwitchOnDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UnlockDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SetPasswordDoorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllDoorQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetDoorByIdQuery).Assembly);
                //CCTV
                cfg.RegisterServicesFromAssembly(typeof(AddCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RemoveCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(LockCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UnlockCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SetPasswordCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SwitchOffCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SwitchOnCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SetModeCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SetNormalModeCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SetNightModeCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(StartRecordingCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(StopRecordingCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SetNewNameCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ToggleCCTVCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllCCTVQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetCCTVByIdQuery).Assembly);
            });

            services.AddSingleton<LampView>();
            services.AddSingleton<DoorView>();
            services.AddSingleton<CCTVView>();
            services.AddSingleton<MainWindow>();

            // IMPORTANT: copia qui le tue registrazioni esistenti
            // es. services.AddTransient<Views.LampView>();
            // registra il ServiceProvider stesso così può essere iniettato
            services.AddSingleton<IServiceProvider>(sp => sp);

            // registra MainWindow in DI (così riceve IServiceProvider)
            services.AddTransient<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            var main = _serviceProvider.GetRequiredService<MainWindow>();
            main.Show();
        }

    }
}
