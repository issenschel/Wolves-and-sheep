using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wolves_and_sheep.Models;
using Wolves_and_sheep.ViewModels.Base;

namespace Wolves_and_sheep.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private Board board = new Board();
        private ICommand ?newGameCommand;
        private ICommand ?clearCommand;
        private ICommand ?cellCommand;
        private CellValueEnum currentPlayer = CellValueEnum.WhiteSheep;


        public Board Board
        {
            get => board;
            set
            {
                board = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewGameCommand => newGameCommand ??= new RelayCommand(parameter =>
        {
            SetupBoard();
        });

        public ICommand ClearCommand => clearCommand ??= new RelayCommand(parameter =>
        {
            Board = new Board();
        });

        public ICommand CellCommand => cellCommand ??= new RelayCommand(parameter =>
        {
            Cell cell = (Cell)parameter;
            Cell? activeCell = Board.FirstOrDefault(x => x.Act);
            if (cell.Cellvalueenum != CellValueEnum.Empty)
            {
                if (!cell.Act && activeCell != null)
                    activeCell.Act = false;
                cell.Act = !cell.Act;
            }
            else if (activeCell != null &&
                // проверяем, что выбранные клетки соседние
                Math.Abs(activeCell.Row - cell.Row) == 1 &&
                Math.Abs(activeCell.Column - cell.Column) == 1)
            {
                activeCell.Act = false;
                cell.Cellvalueenum = activeCell.Cellvalueenum;
                activeCell.Cellvalueenum = CellValueEnum.Empty;
                currentPlayer = currentPlayer == CellValueEnum.WhiteSheep ? CellValueEnum.BlackWolf : CellValueEnum.WhiteSheep;
            }
        }, parameter => parameter is Cell cell && (Board.Any(x => x.Act) || cell.Cellvalueenum != CellValueEnum.Empty && cell.Cellvalueenum == currentPlayer));

        private void SetupBoard()
        {
            Board board = new Board();
            board[7, 0] = CellValueEnum.WhiteSheep;
            board[0, 1] = CellValueEnum.BlackWolf;
            board[0, 3] = CellValueEnum.BlackWolf;
            board[0, 5] = CellValueEnum.BlackWolf;
            board[0, 7] = CellValueEnum.BlackWolf;
            Board = board;
        }

        public MainViewModel()
        {

        }
    }
}
