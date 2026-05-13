using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.abstraction.Errors
{
    public record CCTVErrors: Error
    {
        public static readonly Error AlreadyOn = new("CCTV.AlreadyOn", "La telecamera è già accesa.", ErrorType.Conflict);
        public static readonly Error AlreadyOff = new("CCTV.AlreadyOff", "La telecamera è già spenta.", ErrorType.Conflict);
        public static readonly Error AlreadyRecording = new("CCTV.AlreadyRecording", "La registrazione è già in corso.", ErrorType.Conflict);
        public static readonly Error NotRecording = new("CCTV.NotRecording", "Nessuna registrazione in corso da fermare.", ErrorType.Conflict);
        public static readonly Error ModeAlreadySet = new("CCTV.ModeAlreadySet", "La modalità selezionata è già attiva.", ErrorType.Conflict);
        public static readonly Error AlreadyLocked = new("CCTV.AlreadyLocked", "Il dispositivo è già bloccato.", ErrorType.Conflict);
        public static readonly Error AlreadyUnlocked = new("CCTV.AlreadyUnlocked", "Il dispositivo è già sbloccato.", ErrorType.Conflict);
        public static readonly Error CannotLock = new("CCTV.CannotLock", "Impossibile bloccare la CCTV nelle condizioni attuali.", ErrorType.Conflict);
        public static readonly Error CannotUnlock = new("CCTV.CannotUnlock", "Impossibile sbloccare la CCTV. Verificare la password.", ErrorType.Conflict);
        public static readonly Error CannotSetPassword = new("CCTV.CannotSetPassword", "Cannot set password if the new password is empty or white space.", ErrorType.Failure);

        public CCTVErrors(string code, string description, ErrorType type) : base(code, description, type)
        {
        }
    }
}
