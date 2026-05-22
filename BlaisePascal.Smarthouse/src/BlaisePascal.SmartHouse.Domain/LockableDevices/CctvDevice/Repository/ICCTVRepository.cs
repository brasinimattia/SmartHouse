using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository
{
    public interface ICCTVRepository
    {
        Result Add(CCTV cctv);
        Result Update(CCTV cctv);
        Result Remove(Guid id);
        Result<CCTV> GetById(Guid id);
        Result<List<CCTV>> GetAll();
    }
}
