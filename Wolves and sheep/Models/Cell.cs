using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolves_and_sheep.ViewModels.Base;

namespace Wolves_and_sheep.Models
{
    public class Cell : NotifyPropertyChanged
    {
        private CellValueEnum cellvalueenum;
        private bool act;

        public CellValueEnum Cellvalueenum
        {
            get => cellvalueenum;
            set
            {
                cellvalueenum = value;
                OnPropertyChanged();
            }
        }
        public bool Act
        {
            get => act;
            set
            {
                act = value;
                OnPropertyChanged();
            }
        }
    }  
}
