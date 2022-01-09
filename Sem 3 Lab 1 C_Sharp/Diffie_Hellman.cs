using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    internal class Diffie_Hellman : User
    {
        private int g, p, X, Y, Z;

        public Diffie_Hellman()
        {
            g = 0;
            p = 0;
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Diffie_Hellman(int _g, int _p)
        {
            g = _g;
            p = _p;
            Generate_X();
            Y = 0;
            Z = 0;
            Calc_Y();
        }

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


        public void Set_g(int _g)
        {
            g = _g;
        }

        public void Set_p(int _p)
        {
            p = _p;
        }
        
        public void Set_X(int _x)
        {
            X = _x;
        }
        
        public void Generate_X()
        {
            X = rand.Next(2, 1000);
        }
        
        public int Get_g()
        {
            return g;
        }
        
        public int Get_p()
        {
            return p;
        }
        
        public int Get_X()
        {
            return X;
        }
        
        public int Get_Y()
        {
            return Y;
        }
        
        public int Get_Z()
        {
            return Z;
        }
        
        public void Calc_Y()
        {
            Y = Mod_Exp(g, X, p);
        }
        
        public void Calc_Z(int _y)
        {
            Z = Mod_Exp(_y, X, p);
        }
        
        public void PrintData()
        {
            Console.WriteLine($"Name:  {User_Name}");
            Console.WriteLine($"g: {g}");
            Console.WriteLine($"p: {p}");
            Console.WriteLine($"X: {X}");
            Console.WriteLine($"Y: {Y}");
            Console.WriteLine($"Z: {Z}");
        }
    }
}
