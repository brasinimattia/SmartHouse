using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json
{
    public class LocalPathHelper
    {
        public static string GetSolutionRoot()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "src")))
            {
                dir = dir.Parent;
            }

            if (dir == null)
                throw new Exception("Solution root not found");
6767676767676767676767676767676767676
            return dir.FullName;
        }
    }
}
