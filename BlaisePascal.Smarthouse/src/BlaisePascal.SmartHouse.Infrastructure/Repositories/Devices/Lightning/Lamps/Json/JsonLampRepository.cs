using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Mappers;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json
{
    public class JsonLampRepository : ILampRepository
    {
        private readonly string _filePath = "Lamp.json";

        public JsonLampRepository()
        {
            var solutionRoot = LocalPathHelper.GetSolutionRoot();
            var dataFolder = Path.Combine(solutionRoot, "data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "Lamp.json");

            if (!File.Exists(_filePath))
            {
                Save(new List<Lamp>());
            }
        }

        public Result<List<Lamp>> GetAll()
        {
            return Load();
        }

        public Result<Lamp> GetById(Guid id)
        {
            return Load().First(l => l.Id == id);
        }

        public Result Add(Lamp lamp)
        {
            if (lamp is null)
                return Result.Failure(Error.NullValue);
            var lamps = Load();
            lamps.Add(lamp);
            Save(lamps);
            return Result.Success();

        }

        public Result Update(Lamp lamp)
        {
            var lamps = Load();

            var index = lamps.FindIndex(l => l.Id == lamp.Id);
            if (index == -1)
                return Result.Failure(Error.NotFound("LampJson.NotFound","The lamp with the specified ID was not found."));
            lamps[index] = lamp;
            Save(lamps);
            return Result.Success();
        }

        public Result Remove(Guid id)
        {
            var lamps = Load();
            var lamp = lamps.First(l => l.Id == id);
            lamps.Remove(lamp);
            Save(lamps);
            return Result.Success();
        }

        private List<Lamp> Load()
        {
            var json = File.ReadAllText(_filePath);

            var dtos = JsonSerializer.Deserialize<List<LampDto>>(json) ?? new List<LampDto>();

            return dtos.Select(LampMapper.ToDomain).ToList();
        }

        private void Save(List<Lamp> lamps)
        {
            var dtos = lamps.Select(LampMapper.ToDto).ToList();

            var json = JsonSerializer.Serialize(dtos, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
    }
}
