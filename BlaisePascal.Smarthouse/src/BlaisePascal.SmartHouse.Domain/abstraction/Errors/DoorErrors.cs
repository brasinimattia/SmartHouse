using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.abstraction.Errors
{
    public record DoorErrors: Error
    {
        public static readonly Error AlreadyOpen = new("Door.AlreadyOpen", "The door is already open.", ErrorType.Conflict);
        public static readonly Error AlreadyClosed = new("Door.AlreadyClosed", "The door is already closed.", ErrorType.Conflict);
        public static readonly Error AlreadyLocked = new("Door.AlreadyLocked", "The door is already locked.", ErrorType.Conflict);
        public static readonly Error AlreadyUnlocked = new("Door.AlreadyUnlocked", "The door is already unlocked.", ErrorType.Conflict);
        public static readonly Error CannotLockDoor = new("Door.CannotLockDoor", "Cannot lock door.", ErrorType.Conflict);
        public static readonly Error CannotOpenDoor = new("Door.CannotOpenDoor", "Cannot open the door.", ErrorType.Conflict);
        public static readonly Error CannotUnlockDoor = new("Door.CannotUnlockDoor", "Cannot unlock door.", ErrorType.Conflict);
        public static readonly Error CannotCloseDoor = new("Door.CannotCloseDoor", "Cannot close the door.", ErrorType.Conflict);
        public DoorErrors(string code, string description, ErrorType type) : base(code, description, type)
        {
        }
    }
}
