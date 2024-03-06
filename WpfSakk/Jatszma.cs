using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfSakk
{

    public enum BabuSzine { Világos, Sötét }
    public enum Sakkfigura { Király, Vezér, Bástya, Futó, Huszár, Gyalog }

    #region Mezo osztály
    public class Mezo
    {
        private char oszlop;
        private char sor;
        private Sakkfigura? babu = null;
        private BabuSzine? szin = null;
        public char Oszlop
        {
            get => this.oszlop;
            set
            {
                if (value < 'a' || value > 'h')
                {
                    throw new ArgumentException("Érvénytelen oszlop!");
                }
                this.oszlop = value;
            }
        }

        public char Sor
        {
            get => this.sor;
            set
            {
                if (value < '1' || value > '8')
                {
                    throw new ArgumentException("Érvénytelen sor!");
                }
                this.sor = value;
            }
        }

        public Sakkfigura? Babu { get => babu; set => babu = value; }
        public BabuSzine? Szin { get => szin; set => szin = value; }

        public Mezo(char oszlop, char sor, Sakkfigura? babu = null, BabuSzine? szine = null)
        {
            Oszlop = oszlop;
            Sor = sor;
            this.babu = babu;
            this.szin = szine;
        }

        public Mezo(string ujMezo, Sakkfigura? babu = null, BabuSzine? szine = null) : this(ujMezo[0], ujMezo[1], babu, szine) { }
    }
    #endregion

    public class Lepes
    {
        static public Dictionary<char, Sakkfigura> FigurákNeve = new() {
            { 'K', Sakkfigura.Király },
            { 'V', Sakkfigura.Vezér },
            { 'B', Sakkfigura.Bástya },
            { 'F', Sakkfigura.Futó },
            { 'H', Sakkfigura.Huszár },
            { 'G', Sakkfigura.Gyalog }
        };

        private string lepesKodja;

        public Lepes(String algebraiAlak)
        {
            this.lepesKodja = algebraiAlak;
        }

        public Sakkfigura Babu => FigurákNeve[FigurákNeve.Keys.Contains(lepesKodja[0]) ? lepesKodja[0] : 'G'];
        public Mezo CelMezo => new Mezo(this.lepesKodja[^2], this.lepesKodja[^1]);
        public bool VanUtes => this.lepesKodja.Contains('x');
    }

    //todo Állapottér reprezentáció kialakítása
    public class Sakktabla
    {
        List<Mezo> mezok;
        public Sakktabla()
        {
            mezok = new List<Mezo>();
        }

        public void NyitoAllapotLetrehozasa()
        {
            for (char oszlop = 'a'; oszlop <= 'h'; oszlop++)
            {
                mezok.Add(new Mezo(oszlop, '7', Sakkfigura.Gyalog, BabuSzine.Sötét));
                mezok.Add(new Mezo(oszlop, '2', Sakkfigura.Gyalog, BabuSzine.Világos));
            }
            //Sötét tisztek
            mezok.Add(new Mezo("a8", Sakkfigura.Bástya, BabuSzine.Sötét));
            mezok.Add(new Mezo("h8", Sakkfigura.Bástya, BabuSzine.Sötét));
            mezok.Add(new Mezo("b8", Sakkfigura.Huszár, BabuSzine.Sötét));
            mezok.Add(new Mezo("g8", Sakkfigura.Huszár, BabuSzine.Sötét));
            mezok.Add(new Mezo("c8", Sakkfigura.Futó, BabuSzine.Sötét));
            mezok.Add(new Mezo("f8", Sakkfigura.Futó, BabuSzine.Sötét));
            mezok.Add(new Mezo("d8", Sakkfigura.Vezér, BabuSzine.Sötét));
            mezok.Add(new Mezo("e8", Sakkfigura.Király, BabuSzine.Sötét));

            //Világos tisztek
            mezok.Add(new Mezo("a1", Sakkfigura.Bástya, BabuSzine.Világos));
            mezok.Add(new Mezo("h1", Sakkfigura.Bástya, BabuSzine.Világos));
            mezok.Add(new Mezo("b1", Sakkfigura.Huszár, BabuSzine.Világos));
            mezok.Add(new Mezo("g1", Sakkfigura.Huszár, BabuSzine.Világos));
            mezok.Add(new Mezo("c1", Sakkfigura.Futó, BabuSzine.Világos));
            mezok.Add(new Mezo("f1", Sakkfigura.Futó, BabuSzine.Világos));
            mezok.Add(new Mezo("d1", Sakkfigura.Vezér, BabuSzine.Világos));
            mezok.Add(new Mezo("e1", Sakkfigura.Király, BabuSzine.Világos));
        }

        public void Lep(String ujLepes)
        {
            //hibák vizsgálata
            throw new Exception();


            //todo léptesse oda a bábut, ahová kell
        }

        //TODO számon analitika, hasznos lekérdezések, stb.

        public int TablanLevoBabukSzama => mezok.Count;

        public int BabukErteke(BabuSzine melyikSzin)
        {
            const byte GYALOG_ERTEKE = 1;
            const byte KONNYUTISZT_ERTEKE = 3;
            const byte BASTYA_ERTEKE = 5;
            const byte VEZER_ERTEKE = 9;

            var szinreSzurt = mezok.Where(x => x.Szin == melyikSzin);
            int ertek = szinreSzurt.Count(x => x.Babu == Sakkfigura.Gyalog) * GYALOG_ERTEKE;
            ertek += szinreSzurt.Count(x => x.Babu == Sakkfigura.Huszár || x.Babu == Sakkfigura.Futó) * KONNYUTISZT_ERTEKE;
            ertek += szinreSzurt.Count(x => x.Babu == Sakkfigura.Bástya) * BASTYA_ERTEKE;
            ertek += szinreSzurt.Count(x => x.Babu == Sakkfigura.Vezér) * VEZER_ERTEKE;
            return ertek;
        }

        public List<Mezo> Mezok { get => mezok; }

        public void BabuLep(Mezo honnan, Mezo hova)
        {
            hova.Babu = honnan.Babu;
            hova.Szin = honnan.Szin;
            mezok.Add(hova);
            mezok.Remove(honnan);
        }
    }


    public class Jatszma
    {
        List<Lepes> lepesek;
        //todo Állapottér reprezentáció kialakítása V2.0

        /// <summary>
        /// Üres játék létrehozása
        /// </summary>
        /// 
        private Sakktabla sakktabla;
        public Jatszma()
        {
            lepesek = new();
            sakktabla = new Sakktabla();
            sakktabla.NyitoAllapotLetrehozasa();

        }
        public Jatszma(String fajlSor) : this()
        {
            foreach (var lepesKodja in fajlSor.Trim().Split('\t'))
            {
                BabuLep(new Lepes(lepesKodja)); 
            }
        }

        public void BabuLep(Lepes ujLepes)
        {
            lepesek.Add(ujLepes);
            sakktabla.BabuLep(HonnanLep(ujLepes), ujLepes.CelMezo);
        }

        private Mezo HonnanLep(Lepes ujLepes)
        {
            return sakktabla.Mezok.First(mezo => mezo.Babu == ujLepes.Babu);
        }

        public int LepesekSzama => lepesek.Count();

        public char JatszmaNyertese => LepesekSzama % 2 == 0 ? 's' : 'v';

        //public int HuszarokLepesszama => lepesek.Count(lepes => lepes[0] == 'H');
        public int HuszarokLepesszama => FiguraLepesszama(Sakkfigura.Huszár);

        public int FiguraLepesszama(Sakkfigura megadottFigura)
        {
            return lepesek.Count(lepes => lepes.Babu == megadottFigura);
        }

        /// <summary>
        /// todo: Keresse meg mindkét vezér (királynő) utolsó pozícióját és nézze meg, hogy ott ütötték-e ezt a pozíciót? (vmi x poz)
        /// </summary>
        public bool VezertUttotek => true;

        public Sakktabla Sakktabla { get => sakktabla; }
    }
}
