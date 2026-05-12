using BlaisePascal.SmartHouse.Domain.Device;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices
{
    public class LampsRow
    {
        



        //Properties
        public List<AbstractLamp> Lamps { get; private set; }
        
        //Constructor
        public LampsRow()
        {
            Lamps = new List<AbstractLamp>();
            
        }

        public LampsRow(int numLamp)
        {
            Lamps = new List<AbstractLamp>();
            for (int i = 0; i < numLamp; i++)
            {

                Lamps.Add(new Lamp($"Lamp{i + 1}"));
            }
            
        }

        //Methods
        public void AddLamp(AbstractLamp lamp)
        {
            Lamps.Add(lamp);
        }


        public void SwitchAllOn()
        {
            for (int i = 0; i < Lamps.Count; i++)
            {
                if (Lamps[i].Status == DeviceStatus.Off)
                    Lamps[i].Toggle();

            }
        }

        public void SwitchAllOff()
        {
            for (int i = 0; i < Lamps.Count; i++)
            {
                if (Lamps[i].Status == DeviceStatus.On)
                    Lamps[i].Toggle();
            }
        }

        public void ToggleOneLamp(Guid id)
        {
            for (int i = 0; i < Lamps.Count; i++)
            {
                if (Lamps[i].Id == id)
                {
                    Lamps[i].Toggle();
                }
            }
        }

        

        public void IncreaseAllBrightness()
        {
            for (int i = 0; i < Lamps.Count; i++)
            {
                Lamps[i].IncreaseBrightness();
            }
        }

        public void DecreaseeAllBrightness()
        {
            for (int i = 0; i < Lamps.Count; i++)
            {
                Lamps[i].DecreaseBrightness();
            }
        }

        public void ChangeOneLampBrightness(Guid id, int newBrightness)
        {
            for (int i = 0; i < Lamps.Count; i++)
            {
                if (Lamps[i].Id == id)
                    Lamps[i].ChangeBrightness(newBrightness);
            }
        }

        public void RemoveLampInPoosition(int position)
        {
            if(position >= 0 && position < Lamps.Count)
                Lamps.RemoveAt(position);
        }

        public AbstractLamp? FindLampWithMaxBrightness()
        {
            AbstractLamp? maxLamp = null;
            int maxBrightness = 0;

            foreach(AbstractLamp l in Lamps)
            {
                if(maxBrightness < l.Brightness.Value)
                {
                    maxBrightness = l.Brightness.Value;
                    maxLamp = l;
                }
            }

            return maxLamp;
        }

        public AbstractLamp? FindLampWithMinBrightness()
        {
            AbstractLamp? minLamp = null;
            int minBrightness = 0;

            foreach(AbstractLamp l in Lamps)
            {
                if(minBrightness == 0 || minBrightness > l.Brightness.Value)
                {
                    minBrightness = l.Brightness.Value;
                    minLamp = l;
                }
            }

            return minLamp;
        }

        public List<AbstractLamp> FindLampsByIntensityRange(int min, int max)
        {
            List<AbstractLamp> lamps = new List<AbstractLamp>();

            foreach(AbstractLamp l in Lamps)
            {
                if(l.Brightness.Value >= min && l.Brightness.Value <= max)
                {
                    lamps.Add(l);
                }
            }

            return lamps;
        }

        public List<AbstractLamp> FindAllOn()
        {
            List<AbstractLamp> lamps = new List<AbstractLamp>();

            foreach(AbstractLamp l in Lamps)
            {
                if(l.Status == DeviceStatus.On)
                {
                    lamps.Add(l);
                }
            }

            return lamps;
        }

        public List<AbstractLamp> FindAllOff()
        {
            List<AbstractLamp> lamps = new List<AbstractLamp>();

            foreach(AbstractLamp l in Lamps)
            {
                if(l.Status == DeviceStatus.Off)
                {
                    lamps.Add(l);
                }
            }

            return lamps;
        }

        public AbstractLamp? FindLampById(Guid id)
        {
            AbstractLamp? lamp = null;
            foreach(AbstractLamp l in Lamps)
            {
                if(l.Id == id)
                {
                    lamp = l;
                }
            }

            return lamp;
        }

        public List<AbstractLamp> SortByBrightness(bool descending)
        {
            List<AbstractLamp> sortedLamps = new List<AbstractLamp>();
            AbstractLamp? lampToRemove = null;

            if (descending)
            {
                while(Lamps.Count != 0)
                {
                    lampToRemove = FindLampWithMaxBrightness();
                    sortedLamps.Add(lampToRemove);
                    Lamps.Remove(lampToRemove);
                }
            }
            else
            {
                while(Lamps.Count != 0)
                {
                    lampToRemove = FindLampWithMinBrightness();
                    sortedLamps.Add(lampToRemove);
                    Lamps.Remove(lampToRemove);
                }
            }
            Lamps = sortedLamps;
            return Lamps;
        }


    }
}

