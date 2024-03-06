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
using System.Windows.Shapes;

namespace WpfSakk
{
    /// <summary>
    /// Interaction logic for Tablakep.xaml
    /// </summary>
    public partial class Tablakep : Window
    {
        public Tablakep(Tabla tablakep)
        {
            InitializeComponent();

            //Dinamikusan felépítem a 8x8-as rácsot, abba Button elemeket teszek.
        }
    }
}
