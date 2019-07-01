using System;

namespace InvasionModel
{
    [Serializable]
    public class DataGamer : IComparable<DataGamer>
    {
        /// <summary>Имя игрока</summary>
        public string Name { get; set; }
        /// <summary>Очки набранные игроком</summary>
        public int PointsScored { get; set; }

        /// <summary>Безпараметрический конструктор.
        /// Нужен для десериализации</summary>
        public DataGamer() { }

        /// <summary>Конструктор</summary>
        /// <param name="name">Имя игрока</param>
        /// <param name="pointsScored">Очки набранные игроком</param>
        public DataGamer(string name, int pointsScored)
        {
            Name = name;
            PointsScored = pointsScored;
        }

        public int CompareTo(DataGamer other) => PointsScored.CompareTo(other.PointsScored);
    }
}
