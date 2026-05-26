using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lockable.CCTVs.InMemory
{
    public class InMemoryCCTVRepository: ICCTVRepository
    {
        private readonly List<CCTV> _cctvs;

        public InMemoryCCTVRepository()
        {
            _cctvs = new List<CCTV>() 
            { 
                new CCTV("CCTV1")
            };
        }

        public Result<List<CCTV>> GetAll()
        {
            return _cctvs;
        }

        public Result<CCTV> GetById(Guid id)
        {
            return _cctvs.First(c => c.Id == id);
        }

        public Result Add(CCTV cctv)
        {
            if (cctv == null)
                return Result.Failure(Error.NullValue);
            _cctvs.Add(cctv);
            return Result.Success();
        }

        public Result Remove(Guid id)
        {
            CCTV cctv = GetById(id).Value;
            if (cctv == null)
                return Result.Failure(Error.NullValue);
            _cctvs.Remove(cctv);
            return Result.Success();
        }

        public Result Update(CCTV cctv)
        {
            //To do: implement update logic
            return Result.Success();
        }
    }
}
