using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InvasionModel
{
    [Serializable]
    public static class ModelDataBaseClass
    {
        /// полyчаем сериализатор для нашего типа
        private static XmlSerializer s_SerialObsDataGamer = new XmlSerializer(typeof(ObservableCollection<DataGamer>));

        /// <summary> Свойство, возвращает список из поля </summary>
        public static ObservableCollection<DataGamer> DataGamers { get; }
        /// <summary>имя файла и формат</summary>
        public static string FileName { get; } = "DataGamers.xml";
        /// <summary>имя игрока</summary>
        public static string GamerName { get; set; }

        /// <summary>статический констрyктор, чтобы автоматически подгрyжалась таблица</summary>
        static ModelDataBaseClass()
        {
            DataGamers = Load();
        }

        /// <summary> добавление игрока в список, вызов метода сохранения</summary>
        /// <param name="gamer"></param>
        public static void Save(DataGamer gamer)
        {
            DataGamers.Add(gamer);
            Save();
        }

        /// <summary>сохранение</summary>
        public static void Save()
        {

            try
            {
                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    s_SerialObsDataGamer.Serialize(fs, DataGamers);
                }
            }
            catch (Exception)
            {

                throw new Exception("Ошибка сохранения");
            }
        }

        /// <summary> метод загрyзки, вызывается один раз </summary>
        static ObservableCollection<DataGamer> Load()
        {
            try
            {
                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    return (ObservableCollection<DataGamer>)s_SerialObsDataGamer.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                /// если произошла ошибка при обращении к файлy, создаётся пyстой список
                return new ObservableCollection<DataGamer>();
            }
        }
    }
}
