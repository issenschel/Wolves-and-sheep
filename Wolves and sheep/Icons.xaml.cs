using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wolves_and_sheep.Models;

namespace Wolves_and_sheep
{
    /// <summary>
    /// Логика взаимодействия для Icons.xaml
    /// </summary>
    public partial class Icons : UserControl
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(CellValueEnum), typeof(Icons));

        public CellValueEnum Icon
        { 
            get => (CellValueEnum)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public Icons()
        {
            InitializeComponent();
        }
    }
}
