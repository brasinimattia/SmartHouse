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
        void Update(CCTV cctv);
        void Remove(Guid id);
        CCTV GetById(Guid id);
        List<CCTV> GetAll();
    }
}
