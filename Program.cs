using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame
{
    public enum FactoryType
    {
        Little,
        Small,
        Big,
        Huge,
        Maximal
    }

    class Game
    {
        public void Start()
        {
            
        }
    }

    class Monopoly
    {
        List<Factory> _Factorys = new List<Factory>();
        private int _Profit;
        public List<Factory> Factorys
        {
            get { return _Factorys; }
            set { _Factorys = value; }
        }

        private void Profit()
        {
            for(int i = 0; i < _Factorys.Count; i++)
            {
                _Profit += _Factorys[i].Renta;
            }            
        }

        public int Earning
        {
            get { Profit(); return _Profit; }
        }
    }

    class Factory
    {
        public string[,] FactoryTyps = new string[2, 4] {{"Ларек","Продуктовый Магазин", "СуперМаркет","Торговый Центр" },
                                                         {"100",        "1000",              "5000",     "100000"}};
        private string _Name;
        private int _Renta;
        private FactoryType type;

        private void FactorySet()
        {
            _Name = FactoryTyps[0, (int)type];
            _Renta = Convert.ToInt32(FactoryTyps[1, (int)type]);                      
        }
        public int Renta
        {
            get { return _Renta; }
        }
        public string Name
        {
            get { return _Name; }            
        }
        public FactoryType FactoryType
        {
            get { return type; }
            set { type = value; FactorySet(); }
        }
    }

    class Player
    {
        private int _Money;
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Money
        {
            get { return _Money; }
            set { _Money = value; }
        }
        public void PrintInfo()
        {
            Program.SpCur();
            Console.WriteLine("Имя:{0}\tДенег:{1}",_Name,_Money);
        }
    }

    class Program
    {
        static void SetCursor(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        public static void SpCur()
        {
            SetCursor(3, Console.CursorTop);
        }
        static void Main(string[] args)
        {
            Console.CursorTop += 1;
            Console.CursorVisible = false;
            Player player = new Player();
            player.Name = "Лошара";
            player.Money = 50;
            player.PrintInfo();

            List<Factory> _Factorys = new List<Factory>();

            Factory small = new Factory();
            small.FactoryType = FactoryType.Small;
            _Factorys.Add(small);

            Monopoly monopoly = new Monopoly();
            monopoly.Factorys = _Factorys;

            SpCur();
            Console.WriteLine(monopoly.Earning);
            Console.ReadKey();
        }
    }
}
