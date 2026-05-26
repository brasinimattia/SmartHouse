using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.InMemory
{
    public class InMemoryLampRepository: ILampRepository
    {
        private readonly List<Lamp> _lamps;
        public InMemoryLampRepository()
        {
            _lamps = new List<Lamp>()
            {
                new Lamp("Lamp1"),
                new Lamp("Lamp2"),
                new Lamp("Lamp3")
            };
        }
        public Result<List<Lamp>> GetAll()
        {
            return _lamps;
        }

        public Result<Lamp> GetById(Guid id)
        {
            return _lamps.First(l => l.Id == id);
        }

        public Result Add(Lamp lamp)
        {
            if(lamp == null)
                return Result.Failure(Error.NullValue);
            _lamps.Add(lamp);
            return Result.Success();
        }

        public Result Remove(Guid id)
        {
            Lamp lamp = GetById(id).Value;
            if(lamp == null)
                return Result.Failure(Error.NullValue);
            _lamps.Remove(lamp);
            return Result.Success();
        }

        public Result Update(Lamp lamp)
        {
            //Todo: implement update logic
            return Result.Success();
        }
    }
}
