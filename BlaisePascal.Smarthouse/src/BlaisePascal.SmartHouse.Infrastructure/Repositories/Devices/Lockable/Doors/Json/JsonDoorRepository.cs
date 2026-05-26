using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Mapper;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.Doors.Json
{
    public class JsonDoorRepository : IDoorRepository
    {
        private readonly string _filePath = "Door.json";

        public JsonDoorRepository()
        {
            var solutionRoot = LocalPathHelper.GetSolutionRoot();
            var dataFolder = Path.Combine(solutionRoot, "data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "Door.json");

            if (!File.Exists(_filePath))
            {
                Save(new List<Door>());
            }
        }

        public Result<List<Door>> GetAll()
        {
            return Load();
        }

        public Result<Door> GetById(Guid id)
        {
            return Load().First(d => d.Id == id);
        }

        public Result Add(Door door)
        {
            if (door is null)
                return Result.Failure(Error.NullValue);

            var doors = Load();
            doors.Add(door);
            Save(doors);
            return Result.Success();
        }

        public Result Update(Door door)
        {
            var doors = Load();

            var index = doors.FindIndex(d => d.Id == door.Id);
            if (index == -1)
                return Result.Failure(Error.NotFound("DoorJson.NotFound", "The door with the specified ID was not found."));

            doors[index] = door;
            Save(doors);
            return Result.Success();
        }

        public Result Remove(Guid id)
        {
            var doors = Load();
            var door = doors.First(d => d.Id == id);
            doors.Remove(door);
            Save(doors);
            return Result.Success();
        }

        private List<Door> Load()
        {
            var json = File.ReadAllText(_filePath);

            var dtos = JsonSerializer.Deserialize<List<DoorDto>>(json) ?? new List<DoorDto>();

            return dtos.Select(DoorMapper.ToDomain).ToList();
        }

        private void Save(List<Door> doors)
        {
            var dtos = doors.Select(DoorMapper.ToDto).ToList();

            var json = JsonSerializer.Serialize(dtos, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
    }
}
