using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptions
{
    //	Класс, содержащий в себе основные методы, используемые при работе алгоритмов шифрования
    public class User
    {
        //	Имя пользователя
        protected string User_Name = "";
        //	Тип пользователя (отправитель - true/получатель - false)
        protected bool sender = false;
        //	Передаваемое сообщение
        protected List<long> message = new ();
        //	Переменная, используемая в качестве генератора случайных значений
        public Random rand = new Random();

        //	Метод для установки имени пользователя
        public void SetUser_Name(string _str)
        {
            User_Name = _str;
        }
        //	Метод для установки типа пользователя (отправитель/принимающий)
        public void SetSender(bool _sender)
        {
            sender = _sender;
        }

        //	Метод для возвращения размера сообщения
        public long Get_Message_Size()
        {
            return message.Count;
        }
        //	Метод для возвращения сообщения
        public List<long> Get_Message()
        {
            return message;
        }
        //	Метод для выбора типа сообщения и его ввода
        public void InsertMessage()
        {
            char choice;
            bool stop;
            string _message;
            do
            {
                
                Console.Write("Выберите тип вводимого сообщения:\n1. Число.\n2. Сообщение.\n\n: ");
                choice = Convert.ToChar(Console.ReadKey().KeyChar);
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
        //	Метод для установки числового сообщения
        public void SetMessage(long _message)
        {
            message.Clear();

            message.Add(_message);
        }
        //	Метод для установки строкового сообщения
        public void SetMessage(string _message)
        {
            message.Clear();

            foreach (char i in _message)
            {
                message.Add(i);
            }
        }
        //	Метод для установки сообщения от другого пользователя
        public void SetMessage(List<long> _message)
        {
            message.Clear();

            message = _message;
        }
        //	Метод для конвертации сообщения в строку
        public string Message_ToString()
        {
            string _message = "";


            for (int i = 0; i < message.Count; i++)
            {
                _message += Convert.ToChar(message[i]);
            }
            

            return _message;
        }

        //	Проверка на то является ли полученное функцией число простым: если x - простое, функция возвращает значение TRUE, если нет - FALSE
        public bool Prime(long number)
        {
            var sqrt_number = Math.Sqrt(number)+1;

            for (int i = 2; i < sqrt_number; i++)
            {
                if (number % i == 0) 
                    return false;
            }

            return true;
        }
        //	Проверка на то является ли полученное функцией число простым: если x - простое, функция возвращает значение TRUE, если нет - FALSE
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
        //	Псевдогенератор простых чисел
        public int PrimeRandom(int begining, int end)
        {
            int number = rand.Next(begining, end);
            return Prime(number) ? number : PrimeRandom(begining, end);
        }

        //	Генератор g и p
        public void Generate_g_p(ref long g, ref long p)
        {
            int _p = PrimeRandom(1000000, 10000000);
            var q = (_p - 1) / 2;
            g = rand.Next(2, _p - 1);
            p = _p;
            if (!((g < p - 1) && (Mod_Exp(g, q, p) != 1) && (GCD(g, p) == 1))) Generate_g_p(ref g, ref p);
        }
        //	Генератор g и p
        public void Generate_g_p(ref int g, ref int p)
        {
            p = PrimeRandom(1000000, 10000000);
            var q = (p - 1) / 2;
            g = rand.Next(2, p - 1);

            if (!((g < p - 1) && (Mod_Exp(g, q, p) != 1) && (GCD(g, p) == 1))) Generate_g_p(ref g, ref p);
        }



        //	Наибольший общий делитель/The greatest common divisor (GCD)
        public long GCD(long firstNumber, long secondNumber)
        {
            return secondNumber != 0 ? GCD(secondNumber, firstNumber % secondNumber) : firstNumber;
        }
        //	Наибольший общий делитель/The greatest common divisor (GCD)
        public int GCD(int firstNumber, int secondNumber)
        {
            return secondNumber != 0 ? GCD(secondNumber, firstNumber % secondNumber) : firstNumber;
        }
        //	Возведение в степень по модулю/Modular exponentiation
        public long Mod_Exp(long basis, long power, long divisor)
        {
            return (power == 0) ? (1 % divisor) : ((power % 2 == 1) ? ((Mod_Exp(basis, power - 1, divisor) * basis) % divisor) : Mod_Exp((basis * basis) % divisor, power / 2, divisor));
        }
        //	Возведение в степень по модулю/Modular exponentiation
        public int Mod_Exp(int basis, int power, int divisor)
        {
            return (power == 0) ? (1 % divisor) : ((power % 2 == 1) ? ((Mod_Exp(basis, power - 1, divisor) * basis) % divisor) : Mod_Exp((basis * basis) % divisor, power / 2, divisor));
        }
        // Инверсия числа по cd mod m = 1 или d = c^-1 mod m. firstNumbr - c, secondNumber - m
        public long Mod_Inverse(long firstNumber, long secondNumber)
        {
            Tuple<long,long> u = new (secondNumber,0), v = new (firstNumber,1), t;
            
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
        // Инверсия числа по cd mod m = 1 или d = c^-1 mod m. firstNumbr - c, secondNumber - m
        public int Mod_Inverse(int firstNumber, int secondNumber)
        {
            Tuple<int,int> u = new (secondNumber,0), v = new (firstNumber,1), t;

            for (; v.Item1 > 0;)
            {
                var q = u.Item1/v.Item1;
                t = new(u.Item1 % v.Item1, u.Item2 - q * v.Item2);
                u = v;
                v = t;
            }

            if (u.Item2 < 0) u = new(u.Item1, u.Item2 + secondNumber);

            return u.Item2;
        }
        //  Метод для вывода словаря, содержащего ключи и значения типа long (int64)
        public string Print_Dictionary(ref Dictionary<long,long> _dict)
        {
            string dictionary = "{ ";

            List<long> _key = _dict.Keys.ToList();
            List<long> _value = _dict.Values.ToList();

            for (int i = 0; i < _dict.Count; i++)
            {
                dictionary += $"{_key[i]}: {_value[i]}; ";
            }
            dictionary += "}";

            return dictionary;
        }
        //  Метод для вывода List, содержащего значения типа int (int32)
        public string Print_List(ref List<int> _list)
        {
            string list = "{ ";

            for (int i = 0; i < _list.Count; i++)
            {
                list += $"{_list[i]} ";
            }

            list += "}";

            return list;
        }
        //  Метод для вывода List, содержащего значения типа long (int64)
        public string Print_List(ref List<long> _list)
        {
            string list = "{ ";

            for (int i = 0; i < _list.Count; i++)
            {
                list += $"{_list[i]} ";
            }

            list += "}";

            return list;
        }
    }
}
