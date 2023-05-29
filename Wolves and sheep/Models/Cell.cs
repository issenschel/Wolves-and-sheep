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
        private int row;
        private int column;
        private Board ?board;

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

        public int Row
        {
            get => row;
            set
            {
            row = value;
            OnPropertyChanged();
            }
        }

        public int Column
        {
            get => column;
            set
            {
            column = value;
            OnPropertyChanged();
            }
        }

        public Board Board
        {
            get => board!;
            set
            {
            board = value;
            OnPropertyChanged();
            }
        }

        public Cell(int row, int column, Board board)
        {
        Row = row;
        Column = column;
        Board = board;
        }
    }
}
