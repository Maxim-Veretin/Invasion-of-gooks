using CommLibrary;
using Invasion_of_Gooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invasion_of_Gooks.ViewModel
{
    public partial class ViewModelClass
    {
        //    public DataGamer DtGamer { get => _dtGamer; private set { _dtGamer = value; OnPropertyChanged(); } } 
        //    private DataGamer _dtGamer;

        //    public RelayCommand StartCommand { get; }
        //    public bool StartCanMetod(object parameter)
        //    {
        //        return (parameter != null && !string.IsNullOrWhiteSpace(parameter.ToString()));
        //    }

        //    public void StartMetod(object parameter)
        //    {
        //        if (StartCanMetod(parameter))
        //        {
        //            DtGamer = new DataGamer() { Name = parameter.ToString() };
        //        }
        //    }

        //}

        public string NameGamer { get; set; }
    }
}
