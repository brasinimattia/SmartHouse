using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;

namespace BlaisePascal.SmartHouse.Consoles.Devices.LockableDevices.DoorController.Consoles
{
    public class DoorController
    {
        private readonly IDoorRepository _repository;
        public DoorController(IDoorRepository doorRepository)
        {
            _repository = doorRepository;
        }

        public void AddDoor()
        {
            Console.Write("Door name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name");
                return;
            }

            new AddDoorCommand(_repository).Execute(name);
            Console.WriteLine("Door added!");
        }

        public void ShowDoors()
        {
            List<DoorDto> doors = new GetAllDoorQuery(_repository).Execute();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("DOORS: ");
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();
            if (doors.Count == 0)
            {
                System.Console.WriteLine("No doors available");
                return;
            }
            for (int i = 0; i < doors.Count; i++)
            {
                DoorDto door = doors[i];
                Console.WriteLine($"{i + 1}. {door.Name}\n{door}");
            }
        }

        private DoorDto SelectDoor()
        {
            List<DoorDto> doors = new GetAllDoorQuery(_repository).Execute();

            if (doors.Count == 0)
            {
                Console.WriteLine("No doors available");
                return null;
            }

            Console.Write("Door number: ");
            int index;

            if (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Invalid number");
                return null;
            }

            if (index < 1 || index > doors.Count)
            {
                Console.WriteLine("Door not found");
                return null;
            }

            return doors[index - 1];
        }

        public void SwitchOn()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;

            new SwitchOnDoorCommand(_repository).Execute(door.Id);
            Console.WriteLine("Switched on!");
        }

        public void SwitchOff()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;

            new SwitchOffDoorCommand(_repository).Execute(door.Id);
            Console.WriteLine("Switched off!");
        }

        public void RemoveDoor()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;

            new RemoveDoorCommand(_repository).Execute(door.Id);
            Console.WriteLine("Door removed!");
        }

        public void CloseDoor()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;
            try
            {
                new CloseDoorCommand(_repository).Execute(door.Id);
                Console.WriteLine("Door closed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }            
        }

        public void OpenDoor()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;

            try
            {
                new OpenDoorCommand(_repository).Execute(door.Id);
                Console.WriteLine("Door closed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }
        }

        public void LockDoor()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;

            Console.WriteLine("Insert Password");
            string key = Console.ReadLine();
            try
            {
                new LockDoorCommand(_repository).Execute(door.Id, key);
                Console.WriteLine("Door closed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }
        }

        public void UnlockDoor()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;

            Console.WriteLine("Insert Password");
            string key = Console.ReadLine();
            try
            {
                new UnlockDoorCommand(_repository).Execute(door.Id, key);
                Console.WriteLine("Door closed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }

        }

        public void SetPassword()
        {
            DoorDto door = SelectDoor();
            if (door == null) return;
            Console.WriteLine("Insert Password");
            string key = Console.ReadLine();
            if (key == door.Password)
            {
                Console.WriteLine("Insert new Password");
                string newkey = Console.ReadLine();
                try
                {
                    new SetPasswordDoorCommand(_repository).Execute(door.Id, key);
                    Console.WriteLine("Password set!");
                }
                catch (ArgumentException Aex)
                {
                    Console.WriteLine($"Error: {Aex.Message}");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return;
                }
                new SetPasswordDoorCommand(_repository).Execute(door.Id, newkey);
            }
            else
            {
                Console.WriteLine("Wrong password");
            }
        }
        public static void ShowDoorMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("DOOR CONTROLLER");
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("1. Add door");
            Console.WriteLine("2. Remove door");
            Console.WriteLine("3. Lock door");
            Console.WriteLine("4. Unlock door");
            Console.WriteLine("5. Open Door");
            Console.WriteLine("6. Close Door");
            Console.WriteLine("7. Switch on");
            Console.WriteLine("8. Switch off");
            Console.WriteLine("9. Set Password");
            Console.WriteLine("0. Back");
        }
    }
}
