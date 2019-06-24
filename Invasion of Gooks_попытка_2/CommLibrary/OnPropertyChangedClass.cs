using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CommLibrary
{
    /// <summary>Базовый класс с реализацией INPC </summary>
    public class OnPropertyChangedClass : INotifyPropertyChanged
    {
        #region Событие PropertyChanged и метод для его вызова
        /// <summary>Событие для извещения об изменения свойства</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>Метод для вызова события извещения об изменении свойства</summary>
        /// <param name="prop">Изменившееся свойство</param>
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (string.IsNullOrEmpty(prop))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                return;
            }
            string[] names = prop.Split("\\/\r \n()\"\'-".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            switch (names.Length)
            {
                case 0: break;
                case 1:
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
                    break;
                default:
                    OnPropertyChanged(names);
                    break;
            }

        }

        /// <summary>Метод для вызова события извещения об изменении списка свойств</summary>
        /// <param name="propList">Список имён свойств</param>
        public void OnPropertyChanged(IEnumerable<string> propList)
        {
            foreach (string prop in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>Метод для вызова события извещения об изменении списка свойств</summary>
        /// <param name="propList">Список свойств</param>
        public void OnPropertyChanged(IEnumerable<PropertyInfo> propList)
        {
            foreach (PropertyInfo prop in propList)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name));
        }

        /// <summary>Метод для вызова события извещения об изменении всех свойств</summary>
        /// <param name="propList">Список свойств</param>
        public void OnAllPropertyChanged() 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        #endregion
    }
}
