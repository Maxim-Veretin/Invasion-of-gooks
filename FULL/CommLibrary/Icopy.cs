using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary
{
   /// <summary>Интерфейс для создания копии экземпляра того же типа
                /// и копирования значений в другой или из другого экземпляра</summary>
                /// <typeparam name="T"></typeparam>
    public interface ICopy<T> : ICloneable where T : new()
    {
        ///// <summary>Создание копии экземпляра</summary>
        ///// <returns>Новый экземпляр в том же типе</returns>
        //T Copy();
        /// <summary>Создание копии экземпляра</summary>
        /// <returns>Новый экземпляр в заданном типе</returns>
        T1 Copy<T1>() where T1 : T, new();
        /// <summary>Копирование значений экземпляра в другой экземпляр</summary>
        /// <param name="other">Другой экземпляр в который надо скопировать значения</param>
        void CopyTo(T other);
        /// <summary>Копирование значений экземпляра из другого экземпляра</summary>
        /// <param name="other">Другой экземпляр из которого надо скопировать значения</param>
        void CopyFrom(T other);
    }
  
}
