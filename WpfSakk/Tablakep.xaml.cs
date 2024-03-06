using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Tablakep(Sakktabla tablakep)
        {
            InitializeComponent();

            //Dinamikusan felépítem a 8x8-as rácsot, abba Button elemeket teszek.

            for (int i = 0; i < 8; i++)
            {
                gdTabla.ColumnDefinitions.Add(new ColumnDefinition());
                gdTabla.RowDefinitions.Add(new RowDefinition());
            }

            Button[,] mezok = new Button[8, 8];
            char sor, oszlop;
            for (int sorIndex = 0; sorIndex < 8; sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < 8; oszlopIndex++)
                {
                    oszlop = (char)(97 + oszlopIndex);
                    sor = Convert.ToChar(48 + 8 - sorIndex);

                    string neve = "";
                    if (tablakep.Mezok.Any(x => x.Oszlop == oszlop && x.Sor == sor))
                    {
                        var mezo = tablakep.Mezok.First(x => x.Oszlop == oszlop && x.Sor == sor);

                        neve = $"{mezo.Szin}-{mezo.Babu}";

                    }
                    Button gomb = new Button();
                    gomb.Content = $"{oszlop}:{sor}\n{neve}";
                    gomb.Background = new SolidColorBrush(Colors.Chartreuse);
                    Grid.SetColumn(gomb, oszlopIndex);
                    Grid.SetRow(gomb, sorIndex);
                    gdTabla.Children.Add(gomb);
                }

            }

        }
    }
}
