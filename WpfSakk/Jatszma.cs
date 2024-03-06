using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSakk
{
    public class Lepes
    {
        string _lepes;
        public char Tiszt => _lepes[0];
    }

    //todo Állapottér reprezentáció kialakítása
    public class Tabla
    {
        const char URES_MEZO = ' ';
        char[,] _tabla = new char[8, 8];

        public Tabla()
        {
            for (int i = 0; i < _tabla.GetLength(0); i++)
            {
                for (int j = 0; j < _tabla.GetLength(1); i++)
                {
                    _tabla[i, j] = URES_MEZO;
                }
            }
        }

        public void  NyitoAllapotLetrehozasa()
        {
            //TODO bábuk felhelyzése
        }

        public void Lep(String ujLepes)
        {
            //hibák vizsgálata
            throw new Exception();    


            //todo léptesse oda a bábut, ahová kell
        }

        //TODO számon analitika, hasznos lekérdezések, stb.
    }


    public class Jatszma
    {
        List<String> lepesek;
        //todo Állapottér reprezentáció kialakítása V2.0

        /// <summary>
        /// Üres játék létrehozása
        /// </summary>
        public Jatszma()
        {
            lepesek = new List<String>();
        }
        public Jatszma(String fajlSor)
        {
            lepesek = new List<String>();
            foreach (var item in fajlSor.Trim().Split('\t'))
            {
                lepesek.Add(item);
            }
        }

        public int LepesekSzama => lepesek.Count();

        public char Nyertes => LepesekSzama % 2 == 0 ? 's' : 'v';

        //public int HuszarokLepesszama => lepesek.Count(lepes => lepes[0] == 'H');
        public int HuszarokLepesszama => TisztLepesszama('H');

        public int TisztLepesszama(char tisztJele)
        {
            return lepesek.Count(lepes => lepes[0] == tisztJele);
        }

        /// <summary>
        /// todo: Keresse meg mindkét vezér (királynő) utolsó pozícióját és nézze meg, hogy ott ütötték-e ezt a pozíviót? (vmi x poz)
        /// </summary>
        public bool VezertUttotek => true;
    }
}
