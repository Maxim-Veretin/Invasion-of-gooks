using CommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionViewModel
{
    /// <summary>Класс для глобальных (статических) значений</summary>
    public class ViewModelStatic : OnPropertyChangedClass
    {
        /// <summary>Ссылка на единственный экземпляр ViewModelGameStaticProperty</summary>
        public ViewModelGameStaticProperty GameProperties => ViewModelGameStaticProperty.SingularExemplar;
        /// <summary>Ссылка на единственный экземпляр ViewModelStartStaticProperty</summary>
        public ViewModelStartStaticProperty StartProperties => ViewModelStartStaticProperty.SingularExemplar;
    }

    /// <summary>Класс для глобальных (статических) значений стартовой информации</summary>
    public class ViewModelStartStaticProperty: OnPropertyChangedClass
    {
        /// <summary>Ссылка на единственный экземпляр</summary>
        public static ViewModelStartStaticProperty SingularExemplar { get; } = new ViewModelStartStaticProperty();
         /// <summary>Сокрытие конструтора</summary>
       private ViewModelStartStaticProperty() { }
    }

    /// <summary>Класс для глобальных (статических) значений игровой информации</summary>
    public class ViewModelGameStaticProperty : OnPropertyChangedClass
    {
        /// <summary>Ссылка на единственный экземпляр</summary>
        public static ViewModelGameStaticProperty SingularExemplar { get; } = new ViewModelGameStaticProperty();
         /// <summary>Сокрытие конструтора</summary>
        private ViewModelGameStaticProperty() { }

        private bool _isWorked = false;
        private bool _isPause =  true;
        private bool _isEnded = false;

        /// <summary>Игра запущена</summary>
        public bool IsStarted { get => _isWorked; set { _isWorked = value; OnPropertyChanged(); } }
        /// <summary>Игра в паузе</summary>
        public bool IsPause { get => _isPause; set { _isPause = value; OnPropertyChanged(); OnPropertyChanged(nameof(StopAnumationGif)); } }
        /// <summary>Идентификатор остановки анимации GIF для ImageBehavior.RepeatBehavior</summary>
        public string StopAnumationGif => IsPause ? "0" : "1";
        /// <summary>Игра закончена</summary>
        public bool IsEnded { get => _isEnded; set { _isEnded = value; OnPropertyChanged(); } }

    }
}
