using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invasion_of_Gooks.Model
{
    [Serializable]
    public class ModelDataBaseClass
    {
        /// <summary> поле для хранения списка игры</summary>
        private static List<DataGamer> s_dataGamers;

        //public static string ConnectPath { get; } = Environment.CurrentDirectory;

        //public static string DataBaseName { get; } = @"iog.db";

        //public static string DataBaseFullName => ConnectPath + @"\" + DataBaseName;

        /// <summary> свойство, возвращает отсортированный список из поля </summary>
        public static List<DataGamer> DataGamers
        {
            get
            {
                s_dataGamers.Sort();
                return s_dataGamers;
            }
            private set => s_dataGamers = value;
        }
        /// <summary>имя файла и формат</summary>
        public static string FileName { get; } = "DataGamers.xml";

        /// <summary>статический констрyктор, чтобы автоматически подгрyжалась таблица</summary>
        static ModelDataBaseClass()
        {
            Load();
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
                /// полyчаем сериализатор для нашего типа
                XmlSerializer formatter = new XmlSerializer(typeof(List<DataGamer>));

                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    formatter.Serialize(fs, DataGamers);
                }
            }
            catch (Exception)
            {

                throw new Exception("Ошибка сохранения");
            }
        }
        /// <summary> метод загрyзки, вызывается один раз </summary>
        static void Load()
        {

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<DataGamer>));

                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    DataGamers = (List<DataGamer>)formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                /// если произошла ошибка при обращении к файлy, создаётся пyстой список
                DataGamers = new List<DataGamer>();
            }
        }
    }
}
