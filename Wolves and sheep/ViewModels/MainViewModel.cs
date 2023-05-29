using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wolves_and_sheep.Models;
using Wolves_and_sheep.ViewModels.Base;

namespace Wolves_and_sheep.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private Board board = new ();
        private ICommand ?newGameCommand;
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
            currentPlayer = CellValueEnum.WhiteSheep;
        });

        public ICommand CellCommand => cellCommand ??= new RelayCommand(parameter =>
        {
            Cell cell = (Cell)parameter;
            Cell? activeCell = Board.FirstOrDefault(x => x.Act);

            if (cell.Cellvalueenum != CellValueEnum.Empty)
            {
                if (!cell.Act && (activeCell == null || cell == activeCell))
                    cell.Act = true;
                else
                    cell.Act = false; 
            }
            else if (activeCell != null &&
                Math.Abs(activeCell.Row - cell.Row) == 1 &&
                Math.Abs(activeCell.Column - cell.Column) == 1 &&
                (currentPlayer == CellValueEnum.WhiteSheep || cell.Row > activeCell.Row))
            {
                activeCell.Act = false;
                cell.Cellvalueenum = activeCell.Cellvalueenum;
                activeCell.Cellvalueenum = CellValueEnum.Empty;

                if (cell.Row == 0)
                {
                    ShowEndGameMessage(false);
                    SetupBoard();
                }
                else
                {
                    currentPlayer = currentPlayer == CellValueEnum.WhiteSheep ? CellValueEnum.BlackWolf : CellValueEnum.WhiteSheep;
                }
                if (currentPlayer == CellValueEnum.BlackWolf)
                {
                    bool hasPossibleMoves = Board.Any(x =>
                    x.Cellvalueenum == CellValueEnum.Empty &&
                    Math.Abs(x.Row - cell.Row) == 1 &&
                    Math.Abs(x.Column - cell.Column) == 1);
                    if (!hasPossibleMoves)
                    {
                        ShowEndGameMessage(true);
                        SetupBoard();
                        return;
                    }
                }
            }

            bool hasPossibleMoves1 = Board.Any(x =>
            x.Cellvalueenum == CellValueEnum.WhiteSheep &&
            (Board.Any(y => y.Cellvalueenum == CellValueEnum.Empty &&
            Math.Abs(y.Row - x.Row) == 1 &&
            Math.Abs(y.Column - x.Column) == 1) || Board.Any(y =>
            y.Cellvalueenum == CellValueEnum.BlackWolf &&
            Math.Abs(y.Row - x.Row) <= 0 &&
            Math.Abs(y.Column - x.Column) <= 0)));

            if (!hasPossibleMoves1)
            {
                ShowEndGameMessage(true);
                SetupBoard();
            }

        }, parameter => parameter is Cell cell && (Board.Any(x => x.Act) || cell.Cellvalueenum != CellValueEnum.Empty && cell.Cellvalueenum == currentPlayer));

        private void SetupBoard()
        {
            Board board = new ();
            board[7, 0] = CellValueEnum.WhiteSheep;
            board[0, 1] = CellValueEnum.BlackWolf;
            board[0, 3] = CellValueEnum.BlackWolf;
            board[0, 5] = CellValueEnum.BlackWolf;
            board[0, 7] = CellValueEnum.BlackWolf;
            Board = board;
        }

        public static void ShowEndGameMessage(bool isSheepWinner)
        {
            string winner = isSheepWinner ? "Чёрные волки" : "Белая овца";

            MessageBox.Show($"Игра окончена. Победитель - {winner}.", "Конец игры", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public MainViewModel()
        {
            SetupBoard();
        }
    }
}
