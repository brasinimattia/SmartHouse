using BlaisePascal.SmartHouse.Domain.Device;
using System.ComponentModel.DataAnnotations;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices
{
    public class Lamp: AbstractLamp
    {
        //Constructor
        public Lamp() { }

        public Lamp(string name): base(name) { }
        
        public Lamp(Guid newID, string name): base(newID, name) { }

        public Lamp(string name, Guid id, DeviceStatus status, DateTime created, DateTime modified, int brightness) : base(name, id, status, created, modified, brightness) { }


    }
}