using System;

namespace Invasion_of_Gooks.Model
{
    [Serializable]
    public class DataGamer : IComparable<DataGamer>
    {
        public string Name { get; set; }
        public int Scr { get; set; }
        public DataGamer() { }

        public DataGamer(string name, int scr)
        {
            Name = name;
            Scr = scr;
        }

        public int CompareTo(DataGamer other) => -this.Scr.CompareTo(other.Scr);

    }
}
