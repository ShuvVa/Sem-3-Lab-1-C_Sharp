using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptions
{
    //  Алгоритм шифрования Диффи-Хеллмана
    internal class Diffie_Hellman : User
    {
        private long g, p, X, Y, Z;

        //	Конструктор класса по умолчанию
        public Diffie_Hellman()
        {
            g = 0;
            p = 0;
            X = 0;
            Y = 0;
            Z = 0;
        }
        //	Конструктор класса для инициализации пользователя
        public Diffie_Hellman(long _g, long _p)
        {
            g = _g;
            p = _p;
            Generate_X();
            Y = 0;
            Z = 0;
            Calc_Y();
        }
        //	Конструктор класса для работы с отправителем и получателем
        public Diffie_Hellman(ref Diffie_Hellman first_user, ref Diffie_Hellman second_user)
        {
            g = 0;
            p = 0;
            X = 0;
            Y = 0;
            Z = 0;

            first_user.Calc_Z(second_user.Get_Y());
            second_user.Calc_Z(first_user.Get_Y());
        }

        //	Метод для установки g
        public void Set_g(long _g)
        {
            g = _g;
        }
        //	Метод для установки p
        public void Set_p(long _p)
        {
            p = _p;
        }
        //	Метод для установки X
        public void Set_X(long _x)
        {
            X = _x;
        }
        //	Метод для генерации X
        public void Generate_X()
        {
            X = rand.Next(2, 1000);
        }
        //	Метод для получения g
        public long Get_g()
        {
            return g;
        }
        //	Метод для получения p
        public long Get_p()
        {
            return p;
        }
        //	Метод для получения X
        public long Get_X()
        {
            return X;
        }
        //	Метод для получения Y
        public long Get_Y()
        {
            return Y;
        }
        //	Метод для получения Z
        public long Get_Z()
        {
            return Z;
        }
        //	Метод для расчета Y
        public void Calc_Y()
        {
            Y = Mod_Exp(g, X, p);
        }
        //	Метод для расчета Z
        public void Calc_Z(long _y)
        {
            Z = Mod_Exp(_y, X, p);
        }
        //	Метод для вывода данных класса
        public void PrintData()
        {
            Console.WriteLine($"Name:  {User_Name}");
            Console.WriteLine($"g: {g}");
            Console.WriteLine($"p: {p}");
            Console.WriteLine($"X: {X}");
            Console.WriteLine($"Y: {Y}");
            Console.WriteLine($"Z: {Z}");
            Console.WriteLine();
        }
    }
}
