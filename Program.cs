using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

    class Control
    {
        private int code, pun;
        public char[] kur = new char[255];
        public int Kur(int maxi)
        {
            ConsoleKey btn = Console.ReadKey().Key;
            if (btn == ConsoleKey.Enter || btn == ConsoleKey.Spacebar)
            {
                kur[pun - 1] = ' ';
                return pun;
            }
            if (btn == ConsoleKey.A || btn == ConsoleKey.RightArrow)
            {
                if (pun == 1)
                {

                    kur[0] = ' ';
                    pun = maxi;

                    kur[maxi - 1] = '#';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun - 2] = '#';
                    pun--;
                    return 0;
                }
            }
            else if (btn == ConsoleKey.DownArrow || btn == ConsoleKey.D)
            {
                if (pun == maxi)
                {
                    kur[maxi - 1] = ' ';
                    pun = 1;
                    kur[0] = '#';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun] = '#';
                    pun++;
                    return 0;
                }
            }

            else return 0;
        }
        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        public int Pun
        {
            get { return pun; }
            set { pun = value; }
        }
        public void Set()
        {
            code = 0; pun = 1; kur[0] = '#';
        }
    }

    class Game
    {
        private int _GlobalTime;
        Player _Player = new Player();
        List<Factory> FactoryList = new List<Factory>();
        Factory _Factory = new Factory();
        Control Ctrl = new Control();
        Monopoly _Monopoly = new Monopoly();


        void Time(object args)
        {
            _GlobalTime = +1;
            _Player.Money += _Monopoly.Earning;
        }

        void PrintMonopoly()
        {
            Console.WriteLine("Ваши предприятия:");
            Program.SetCursor(3, Console.CursorTop);
            
            for (int i = 0; i < Enum.GetValues(typeof(FactoryType)).Length; i++)
            {
                int factorycount = _Monopoly.TypeLot((FactoryType)i);
                int factoryprofit = factorycount * Convert.ToInt32(_Factory.Price(i));
            Console.Write("{0}[{1}](+{2})\t",_Factory.Type(i),factorycount,factoryprofit);
            }
        }

        public int GlobalTime
        {
            get { return _GlobalTime; }
            set { _GlobalTime = value; }
        }
        public void Start()
        {           
            do
            {
                Console.Clear();
                Program.SpCur(1);
                Console.Write("Введите имя:");
                _Player.Name = Console.ReadLine();
            } while (_Player.Name == "");
            _Player.Money = 100;
            _Player.PrintInfo();
            Timer time = new Timer(Time, null, 0, 1000);
            Update();
        }
        public void Update()
        {
            _Monopoly.List = FactoryList;
            Ctrl.Set();
            do
            {
                Console.Clear();
                _Player.PrintInfo();
                PrintMonopoly();
                Program.SetCursor(2, Console.CursorTop+1);
                Console.WriteLine("{0}Купить Ларек\t\t{1}Купить СуперМаркет",Ctrl.kur[0], Ctrl.kur[1]);
                Program.SetCursor(3, Console.CursorTop+1);
                

                Ctrl.Code = Ctrl.Kur(2);
            } while (Ctrl.Code == 0);
            switch (Ctrl.Code)
            {      
                case 1:
                    if (_Player.Money >= 10)
                    {
                        FactoryList.Add(new Factory() { FactoryType = FactoryType.Little });                      
                        _Player.Money -= 10;
                    }
                    Update();
                    break;
                case 2:
                    if (_Player.Money >= 100)
                    {
                        FactoryList.Add(new Factory() { FactoryType = FactoryType.Big });
                        _Player.Money -= 100;
                    }
                    
                    Update();
                    break;
           
            }
            
        }
    }

    class Monopoly
    {
        List<Factory> _Factorys = new List<Factory>();
        private int _Profit;

        public string FactoryName(int index)
        {
            return _Factorys[index].Name;
        }

        public int TypeLot(FactoryType type)
        {
            int x = 0;
            for (int i = 0; i < _Factorys.Count; i++)
            {
                if (_Factorys[i].FactoryType == type)
                {
                    x++;
                }
            }
            return x;
        }

        public int Count
        {
            get { return _Factorys.Count; }
        }
        
        public List<Factory> List
        {
            set { _Factorys = value; }
            get { return _Factorys; }
        }
        public void Profit()
        {
            _Profit = 0;
            for(int i = 0; i < _Factorys.Count; i++)
            {
                _Profit += _Factorys[i].Renta;
            }            
        }

        public int Earning
        {
            get { Profit();  return _Profit; }
        }
    }

    class Factory
    {
        private string[,] FactoryTyps = new string[2, 5] {{"Ларек","Продуктовый Магазин", "СуперМаркет", "Гипермаркет","Торговый Центр" },
                                                         {"10",        "100",              "1000",          "10000",   "50000"}};
        private string _Name;
        private int _Renta;
        private FactoryType type;

        private void FactorySet()
        {
            _Name = FactoryTyps[0, (int)type];
            _Renta = Convert.ToInt32(FactoryTyps[1, (int)type]);                      
        }
        public string Price(int index)
        {
            return FactoryTyps[1, index];
        }

        public string Type(int index)
        {
            return FactoryTyps[0,index];
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
            Program.SetCursor(3, 1);
            Console.WriteLine("Имя:{0}\t\tДенег:{1}\n",_Name,_Money);
        }
    }

    class Program
    {
        static public Game _Game = new Game();
        public static void SetCursor(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        public static void SpCur(int x)
        {
            SetCursor(3, Console.CursorTop+x);
        }      

        static void Setting()
        {
            Console.SetBufferSize(140, 30);
            Console.SetWindowSize(140, 30);
            Console.CursorVisible = false;
        }

        
        static void Main(string[] args)
        {
            Setting();
            _Game.Start();
            Console.ReadKey();
        }
    }
}
