using BlaisePascal.SmartHouse.Domain.abstraction.Errors;
using BlaisePascal.SmartHouse.Domain.abstraction.Events;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.SharedKernel;
using System;

namespace BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;

public class CCTV : AbstractDevice, ICCTV, ILockable  
{
    // Properties
    public CCTVMode Mode { get; private set; }
    public bool isRecording { get; private set; }
    public LockingStatus LockingStatus { get; private set; }
    public Password Password { get; private set; }
    private bool PasswordSetted => !string.IsNullOrWhiteSpace(Password?.Key);

    // Constructors
    public CCTV(string name) : base(name)
    {
        Mode = CCTVMode.NoMode;
        isRecording = false;
    }

    public CCTV(string name, Guid id) : base(id, name)
    {
        Mode = CCTVMode.NoMode;
        isRecording = false;
    }

    public CCTV(string name, Guid id, DeviceStatus status, CCTVMode mode, bool recording, LockingStatus lockingStatus, string password, DateTime created, DateTime modified)
        : base(id, name)
    {
        Status = status;
        Mode = mode;
        isRecording = recording;
        LockingStatus = lockingStatus;
        Password = Password.Create(password);
        CreatedAtUtc = created;
        LastModifiedAtUtc = modified;
    }

    // Methods
    public override Result SwitchOn()
    {
        if (Status == DeviceStatus.On)
            return Result.Failure(CCTVErrors.AlreadyOn);

        Status = DeviceStatus.On;
        Raise(new DeviceSwitchedOnEvent(Id));
        Touch();

        return Result.Success();
    }

    public override Result SwitchOff()
    {
        if (Status == DeviceStatus.Off)
            return Result.Failure(CCTVErrors.AlreadyOff);

        Status = DeviceStatus.Off;
        Raise(new DeviceSwitchedOffEvent(Id)); 
        Touch();

        return Result.Success();
    }

    public Result SetNormalMode() => SetMode(CCTVMode.Normal);

    public Result SetNightMode() => SetMode(CCTVMode.Night);

    public Result SetMode(CCTVMode mode)
    {
        OnValidator();

        if (Mode == mode)
            return Result.Failure(CCTVErrors.ModeAlreadySet);

        Mode = mode;
        Touch();

        return Result.Success();
    }

    public Result StartRecording()
    {
        OnValidator();

        if (isRecording)
            return Result.Failure(CCTVErrors.AlreadyRecording);

        isRecording = true;
        Touch();

        return Result.Success();
    }

    public Result StopRecording()
    {
        OnValidator();

        if (!isRecording)
            return Result.Failure(CCTVErrors.NotRecording);

        isRecording = false;
        Touch();

        return Result.Success();
    }

    public Result SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Result.Failure(CCTVErrors.TemporaryError); //Errore Temporaneo 

        Password = Password.Create(password);
        Touch();

        return Result.Success();
    }

    public Result Unlock(string key)
    {
        OnValidator();

        bool noPassword = !PasswordSetted;
        bool correctPassword = PasswordSetted && Password.Key == key;

        if (LockingStatus == LockingStatus.Locked && (noPassword || correctPassword))
        {
            LockingStatus = LockingStatus.Unlocked;
            Touch();
            return Result.Success();
        }

        return Result.Failure(CCTVErrors.CannotUnlock);
    }

    public Result Lock(string key)
    {
        OnValidator();

        bool noPassword = !PasswordSetted;
        bool correctPassword = PasswordSetted && Password.Key == key;

        if (LockingStatus == LockingStatus.Unlocked && (noPassword || correctPassword))
        {
            LockingStatus = LockingStatus.Locked;
            Touch();
            return Result.Success();
        }

        return Result.Failure(CCTVErrors.CannotLock);
    }
}