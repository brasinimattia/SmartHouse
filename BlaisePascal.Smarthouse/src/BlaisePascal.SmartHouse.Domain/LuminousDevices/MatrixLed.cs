using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices
{
    public class MatrixLed
    {
        //Properties
        public AbstractLamp[,] Matrix { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        //Constructor
        public MatrixLed() { }

        public MatrixLed(int rows, int columns)
        {
            Matrix = new AbstractLamp[rows, columns];
            for(int r=0; r<rows; r++)
            {
                for(int c=0; c<columns; c++)
                {
                    Matrix[r, c] = new Lamp($"Lamp({r},{c}");
                }
            }
            Rows = rows;
            Columns = columns;
        }

        public MatrixLed(AbstractLamp[,] m)
        {
            Matrix = m;
        }

        //Methods
        public void TurnAllOn()
        {
            for (int r = 0; r <Rows; r++)
            {
                for (int c = 0; c <Columns; c++)
                {
                    if (Matrix[r, c].Status != Device.DeviceStatus.On)
                        Matrix[r, c].SwitchOn();
                }
            }
        }

        public void TurnAllOff()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Matrix[r, c].Status != Device.DeviceStatus.Off)
                        Matrix[r, c].SwitchOff();
                }
            }
        }

        public void TurnRowOn(int rowIdx)
        {
            for(int c=0; c<Columns; c++)
            {
                if (Matrix[rowIdx, c].Status != Device.DeviceStatus.On)
                    Matrix[rowIdx, c].SwitchOn();
            }
        }

        public void TurnRowOff(int rowIdx)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (Matrix[rowIdx, c].Status != Device.DeviceStatus.Off)
                    Matrix[rowIdx, c].SwitchOff();
            }
        }

        public void TurnColumnOn(int colIdx)
        {
            for (int r = 0; r<Rows; r++)
            {
                if (Matrix[r, colIdx].Status != Device.DeviceStatus.On)
                    Matrix[r,colIdx].SwitchOn();
            }
        }

        public void TurnColumnOff(int colIdx)
        {
            for (int r = 0; r <Rows ; r++)
            {
                if (Matrix[r, colIdx].Status != Device.DeviceStatus.Off)
                    Matrix[r, colIdx].SwitchOff();
            }
        }

        public void IncreaseAllBrightness()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Matrix[r, c].IncreaseBrightness();
                }
            }
        }

        public void DecreaseAllBrightness()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Matrix[r, c].DecreaseBrightness();
                }
            }
        }

        public void TurnOnSingleLamp(int rowIdx, int colIdx)
        {
            if (Matrix[rowIdx, colIdx].Status != Device.DeviceStatus.On)
                Matrix[rowIdx, colIdx].SwitchOn();
        }

        public void TurnOffSingleLamp(int rowIdx, int colIdx)
        {
            if (Matrix[rowIdx, colIdx].Status != Device.DeviceStatus.Off)
                Matrix[rowIdx, colIdx].SwitchOff();
        }

        public void IncreaseSingleLampBrightness(int rowIdx, int colIdx)
        {
            Matrix[rowIdx, colIdx].IncreaseBrightness();
        }

        public void DecreaseSingleLampBrightness(int rowIdx, int colIdx)
        {
            Matrix[rowIdx, colIdx].DecreaseBrightness();
        }

        public void ChangeSingleLampBrightness(int rowIdx, int colIdx, int brightness)
        {
            Matrix[rowIdx, colIdx].ChangeBrightness(brightness);
        }
    }
}
