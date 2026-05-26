
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.Doors.InMemory
{
    public class InMemoryDoorRepository : IDoorRepository
    {
        private readonly List<Door> _doors;
        public InMemoryDoorRepository()
        {
            _doors = new List<Door>()
            {
                new Door("Door1")
            };
        }
        public Result<List<Door>> GetAll()
        {
            return _doors;
        }

        public Result<Door> GetById(Guid id)
        {
            return _doors.First(l => l.Id == id);
        }

        public Result Add(Door door)
        {
            if (door == null)
                return Result.Failure(Error.NullValue);
            _doors.Add(door);
            return Result.Success();
        }

        public Result Remove(Guid id)
        {
            Door door = GetById(id).Value;
            if (door != null)
                return Result.Failure(Error.NullValue);
            _doors.Remove(door);
            return Result.Success();
        }

        public Result Update(Door door)
        {
            //Todo: implement update logic
            return null;
        }
    }
}
