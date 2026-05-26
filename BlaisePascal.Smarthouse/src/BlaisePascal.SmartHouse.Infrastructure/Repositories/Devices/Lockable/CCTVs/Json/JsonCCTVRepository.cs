using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Mappers;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.CCTVs.Json
{
    public class JsonCCTVRepository : ICCTVRepository
    {
        private readonly string _filePath = "CCTV.json";

        public JsonCCTVRepository()
        {
            var solutionRoot = LocalPathHelper.GetSolutionRoot();
            var dataFolder = Path.Combine(solutionRoot, "data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "CCTV.json");

            if (!File.Exists(_filePath))
            {
                Save(new List<CCTV>());
            }
        }

        public Result<List<CCTV>> GetAll()
        {
            return Load();
        }

        public Result<CCTV> GetById(Guid id)
        {
            return Load().First(c => c.Id == id);
        }

        public Result Add(CCTV cctv)
        {
            if (cctv is null)
                return Result.Failure(Error.NullValue);

            var cctvs = Load();
            cctvs.Add(cctv);
            Save(cctvs);
            return Result.Success();
        }

        public Result Update(CCTV cctv)
        {
            var cctvs = Load();

            var index = cctvs.FindIndex(c => c.Id == cctv.Id);
            if (index == -1)
                return Result.Failure(Error.NotFound("CCTVJson.NotFound", "The CCTV with the specified ID was not found."));

            cctvs[index] = cctv;
            Save(cctvs);
            return Result.Success();
        }

        public Result Remove(Guid id)
        {
            var cctvs = Load();
            var cctv = cctvs.First(c => c.Id == id);
            cctvs.Remove(cctv);
            Save(cctvs);
            return Result.Success();
        }

        private List<CCTV> Load()
        {
            var json = File.ReadAllText(_filePath);

            var dtos = JsonSerializer.Deserialize<List<CCTVDto>>(json) ?? new List<CCTVDto>();

            return dtos.Select(CCTVMapper.ToDomain).ToList();
        }

        private void Save(List<CCTV> cctvs)
        {
            var dtos = cctvs.Select(CCTVMapper.ToDto).ToList();

            var json = JsonSerializer.Serialize(dtos, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
    }
}
