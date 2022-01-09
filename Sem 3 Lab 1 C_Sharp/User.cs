using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class User
    {
        protected string User_Name;
        protected bool sender;
        protected List<int> message;
        public Random rand = new Random();

        public void SetUser_Name(string _str)
        {
            User_Name = _str;
        }
        public void SetSender(bool _sender)
        {
            sender = _sender;
        }

        public int Get_Message_Size()
        {
            return message.Count;
        }

        public List<int> Get_Message()
        {
            return message;
        }

        public void InsertMessage()
        {
            char choice;
            bool stop = true;
            string _message;
            do
            {
                stop = true;
                Console.Write("Выберите тип вводимого сообщения:\n1. Число.\n2. Сообщение.\n\n: ");
                choice = Convert.ToChar(Console.ReadKey());
                Console.WriteLine();
                switch (choice)
                {
                    case '1':
                        do
                        {
                            stop = true;
                            Console.WriteLine("Ввод числового сообщения:\n");
                            Console.Write("Введите сообщение :\n\n: ");
                            _message = Console.ReadLine();
                            Console.WriteLine();

                            foreach (char i in _message)
                            {
                                if ((i < '0') || (i > '9'))
                                {
                                    Console.WriteLine("Ввод числового сообщения:\n");
                                    stop = false;
                                    break;
                                }
                                if (stop) break;
                            }
                        } while (true);

                        SetMessage(_message);
                        stop = true;
                        break;
                    case '2':
                        Console.WriteLine("Ввод строкового сообщения:\n");
                        Console.Write("Введите сообщение :\n: ");
                        _message = Console.ReadLine();
                        Console.WriteLine();

                        SetMessage(_message);
                        stop = true;
                        break;
                    default:
                        Console.WriteLine("Вы неверно ввели номер варианта. Повторите ввод еще раз.");
                        stop = false;
                        break;
                }

                if (stop) break;
            } while (true);
        }

        public void SetMessage(int _message)
        {
            message.Clear();

            message.Add(_message);
        }
        public void SetMessage(string _message)
        {
            message.Clear();

            foreach (char i in _message)
            {
                message.Add(i);
            }
        }

        public void SetMessage(List<int> _message)
        {
            message.Clear();

            message = _message;
        }

        public string Message_ToString()
        {
            //string _message;



            //_message = message.ToString();

            //return _message;

            return message.ToString();
        }


        public bool Prime(int number)
        {
            var sqrt_number = Math.Sqrt(number)+1;

            for (int i = 2; i < sqrt_number; i++)
            {
                if (number % i == 0) 
                    return false;
            }

            return true;
        }

        public int PrimeRandom(int begining, int end)
        {
            int number = rand.Next(begining, end);
            return Prime(number) ? number : PrimeRandom(begining,end);
        }

        public void Generate_g_p(ref int g, ref int p)
        {
            p = PrimeRandom(1000000, 10000000);
            var q = (p - 1) / 2;
            g = rand.Next(2, p - 1);

            if (!((g < p - 1) && (Mod_Exp(g, q, p) != 1) && (GCD(g, p) == 1))) Generate_g_p(ref g, ref p);
        }

        public int GCD(int firstNumber, int secondNumber)
        {
            return secondNumber != 0 ? GCD(secondNumber, firstNumber % secondNumber) : firstNumber;
        }

        public int Mod_Exp(int basis, int power, int divisor)
        {
            return (power == 0) ? (1 % divisor) : ((power % 2 == 1) ? ((Mod_Exp(basis, power - 1, divisor) * basis) % divisor) : Mod_Exp((basis * basis) % divisor, power / 2, divisor));
        }

        public int Mod_Inverse(int firstNumber, int secondNumber)
        {
            Tuple<int,int> u = new (secondNumber,0), v = new (firstNumber,1), t;
            
            for (; v.Item1 > 0;)
            {
                var q = u.Item1/v.Item1;
                t = new (u.Item1 % v.Item1, u.Item2 - q * v.Item2);
                u = v;
                v = t;
            }

            if (u.Item2 < 0) u = new(u.Item1, u.Item2 + secondNumber);

            return u.Item2;
        }

    }
}
