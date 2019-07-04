using CommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionViewModel
{
    public class ViewModelStatic : OnPropertyChangedClass
    {
        public ViewModelGameStaticProperty GameProperties => ViewModelGameStaticProperty.SingularExemplar;
        public ViewModelStartStaticProperty StartProperties => ViewModelStartStaticProperty.SingularExemplar;
    }

    public class ViewModelStartStaticProperty: OnPropertyChangedClass
    {
        public static ViewModelStartStaticProperty SingularExemplar { get; } = new ViewModelStartStaticProperty();
        private ViewModelStartStaticProperty() { }
    }

    public class ViewModelGameStaticProperty : OnPropertyChangedClass
    {
        public static ViewModelGameStaticProperty SingularExemplar { get; } = new ViewModelGameStaticProperty();
        private ViewModelGameStaticProperty() { }
        private bool _isWorked = false;
        private bool _isPause =  true;
        private bool _isEnded = false;

        /// <summary>Игра запущена</summary>
        public bool IsWorked { get => _isWorked; set { _isWorked = value; OnPropertyChanged(); } }
        /// <summary>Игра в паузе</summary>
        public bool IsPause { get => _isPause; set { _isPause = value; OnPropertyChanged(); OnPropertyChanged(nameof(StopAnumationGif)); } }
        /// <summary>Идентификатор остановки анимации GIF для ImageBehavior.RepeatBehavior</summary>
        public string StopAnumationGif => IsPause ? "0" : "1";
        /// <summary>Игра закончена</summary>
        public bool IsEnded { get => _isEnded; set { _isEnded = value; OnPropertyChanged(); } }

    }
}
