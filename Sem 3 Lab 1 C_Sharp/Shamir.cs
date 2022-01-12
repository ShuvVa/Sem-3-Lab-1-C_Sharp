using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptions
{
    //  Алгоритм шифрования Шамира
    internal class Shamir : User
    {
        private long p;
        private Dictionary <long, long> key = new Dictionary<long, long>();
        //	Конструктор класса по умолчанию
        public Shamir()
        {
            p = 0;
        }
        //	Конструктор класса для инициализации пользователя
        public Shamir(long _p, bool _sender = false)
        {
            p = _p;
            sender = _sender;
            if (sender)
            {
                InsertMessage();
            }
        }
        //	Конструктор класса для работы с отправителем и получателем
        public Shamir(ref Shamir first_user, ref Shamir second_user)
        {
            sender = false;

            p = 0;
            message = new ();

            first_user.Encode(first_user.Get_Message());

            second_user.Encode(first_user.Get_Message());

            first_user.Decode(second_user.Get_Message());

            second_user.Decode(first_user.Get_Message());
        }

        //  Метод для проверки наличия одинаковых ключей в словаре
        public bool check_for_equal_keys(long _key) {
            List <long> k = new ();

            k.AddRange(key.Keys.ToArray());
            
            for (int i = 0; i < k.Count; i++)
            {
                if (k[i] == _key) return true;
            }

            return false;
        }
        //  Метод для проверки наличия одинаковых значений в словаре
        public bool check_for_equal_values(long _value)
        {
            List <long> v = new ();

            v.AddRange(key.Values.ToArray());

            for (int i = 0; i < v.Count; i++)
            {
                if (v[i] == _value) return true;
            }

            return false;
        }
        
        //	Метод для генерации с и d по принятому размеру сообщения
        public void Generate_key()
        {
            for (int i = 0; i < message.Count; i++)
                do
                {
                    long c = 0;
                    do
                    {
                        c = rand.Next(2, 1000);
                    } while (GCD(c, p - 1) != 1);

                    long d = Mod_Inverse(c, p - 1);
                    if ((!check_for_equal_keys(c)) && (!check_for_equal_values(d)))
                    {
                        key.Add(c, d);
                        break;
                    }
                } while (true);
        }

        //	Метод для шифрования принятого сообщения
        public void Encode(List<long> _message)
        {
            message = _message;
            Generate_key();

            List<long> k = new ();
            k.AddRange(key.Keys.ToArray());

            List<long> temp = new();

            for (int i = 0; i < message.Count; i++)
            {
                temp.Add(Mod_Exp(message[i], k[i], p));
            }

            message = temp;
        }
        //	Метод для дешифрования принятого сообщения
        public void Decode(List<long> _message)
        {
            message.Clear();
            message = _message;

            

            List<long> v = new();
            v.AddRange(key.Values.ToArray());

            List<long> temp = new();

            for (int i = 0; i < message.Count; i++)
            {
                temp.Add(Mod_Exp(message[i], v[i], p));
            }

            message = temp;
        }

        //	Метод для вывода данных класса
        public void PrintData()
        {
            Console.WriteLine($"Name:  {User_Name}\n");
            Console.WriteLine($"sender:  {sender}\n");
            Console.WriteLine($"p:  {p}\n");
            Console.WriteLine($"message array:  {Print_List(ref message)}\n");
            Console.WriteLine($"message:  {Message_ToString()}\n");
            Console.WriteLine($"key:  {Print_Dictionary(ref key)}\n");
            Console.WriteLine();
        }
    }
}
