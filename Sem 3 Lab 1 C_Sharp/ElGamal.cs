using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptions
{
    //  Алгоритм шифрования Эль-Гамаля
    internal class ElGamal : User
    {
        private int g, p;
        private List<long> c = new (), d= new (), k= new (), r= new ();
        private Dictionary<long, long> data = new ();

        //	Конструктор класса по умолчанию
        public ElGamal()
        {
            g = 0;
            p = 0;
        }
        //	Конструктор класса для инициализации пользователя
        public ElGamal(int _p, int _g, bool _sender = false)
        {
            p = _p;
            g = _g;
            sender = _sender;
            if (sender)
            {
                InsertMessage();
                Generate_k_r();
            }
        }
        //	Конструктор класса для работы с отправителем и получателем
        public ElGamal(ref ElGamal first_user, ref ElGamal second_user)
        {
            p = 0;
            g = 0;
            sender = false;

            second_user.Generate_c_d(first_user.Get_Message_Size());

            first_user.Encrypt(second_user.Get_d());

            second_user.SetData(first_user.Get_data());

            second_user.Decrypt();
        }

        //	Метод для генерации с и d по принятому размеру сообщения
        public void Generate_c_d(long _message_size)
        {
            c.Clear();
            d.Clear();
            long _c;
            for (int i = 0; i < _message_size; i++)
            {
                do
                {
                    _c = rand.Next(2, p - 1);
                    if ((_c < p - 1) && (_c > 1))
                    {
                        c.Add(_c);
                        d.Add(Mod_Exp(g, c[i], p));
                        break;
                    }
                } while (true);
            }
        }
        //	Метод для генерации k и r 
        public void Generate_k_r()
        {
            k.Clear();
            r.Clear();
            long _k, _r;
            for (int i = 0; i < message.Count; i++)
            {
                do
                {
                    _k = rand.Next(1, p - 2);
                    if ((_k >= 1) && (_k <= p - 2)) break;
                } while (true);

                _r = Mod_Exp(g, _k, p);
                k.Add(_k);
                r.Add(_r);
            }
        }

        //  Метод для возвращения пар "ключ - закодированное сообщение"
        public Dictionary<long, long> Get_data()
        {
            return data;
        }
        //  Метод для возвращения d
        public List<long> Get_d()
        {
            return d;
        }

        //	Метод для шифрования сообщения по принятому d
        public void Encrypt(List<long> _d)
        {
            List<long> temp = new();

            for (int i = 0; i < message.Count; i++)
            {
                temp.Add( Mod_Exp(message[i] * Mod_Exp(_d[i], k[i], p), 1, p));
            }

            message = temp;

            SetData();
        }
        //	Метод для дешифрования сообщения
        public void Decrypt()
        {
            r.Clear();
            message.Clear();

            r.AddRange(data.Keys.ToArray()); message.AddRange(data.Values.ToArray());

            List<long> temp = new();

            for (int i = 0;i < message.Count; i++)
            {
                temp.Add(Mod_Exp(message[i] * Mod_Exp(r[i], p - 1 - c[i], p), 1, p));
            }

            message = temp;
        }

        //	Метод для установки полученного p
        public void Set_p(int _p)
        {
            p = _p;
        }
        //	Метод для установки полученного g
        public void Set_g(int _g)
        {
            g = _g;
        }
        //	Метод для задания пар "ключ - закодированное сообщение"
        public void SetData()
        {
            data.Clear();

            for (int i = 0; i < message.Count; i++)
            {
                data.Add(r[i], message[i]);
            }
        }
        //	Метод для установки принятых пар "ключ - закодированное сообщение"
        public void SetData(Dictionary<long, long> _data)
        {
            data.Clear();

            data = _data;
        }
        
        //	Метод для вывода данных класса
        public void PrintData()
        {
            Console.WriteLine($"Name: {User_Name}");
            Console.WriteLine($"sender: {sender}");
            Console.WriteLine($"g: {g}");
            Console.WriteLine($"p: {p}"); 
            Console.WriteLine($"c: {Print_List(ref c)}"); 
            Console.WriteLine($"d: {Print_List(ref d)}");
            Console.WriteLine($"k: {Print_List(ref k)}");
            Console.WriteLine($"r: {Print_List(ref r)}");
            Console.WriteLine($"message: {Print_List(ref message)}");
            Console.WriteLine($"converted message: {Message_ToString()}");
            Console.WriteLine($"data: {Print_Dictionary(ref data)}");
            Console.WriteLine();
        }
    }
}
