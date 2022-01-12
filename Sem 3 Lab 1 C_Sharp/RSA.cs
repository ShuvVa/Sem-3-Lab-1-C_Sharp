using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptions
{
	//	Алгоритм шифрования RSA
	internal class RSA : User
    {
        private List<long> c= new (), d= new ();
		private List<int> p = new (), q= new (), n= new (), fi= new ();
        //	Конструктор класса по умолчанию
        public RSA()
        {
			sender = false;
		}
		//	Конструктор класса для инициализации пользователя
		public RSA(bool _sender = false)
        {
			sender = _sender;
			if (sender)
			{
				InsertMessage();
			}
		}
		//	Конструктор класса для работы с отправителем и получателем
		public RSA(ref RSA first_user, ref RSA second_user)
        {
			second_user.Generate_p(first_user.Get_Message_Size());
			first_user.Encrypt(second_user.Get_d(), second_user.Get_n());
			second_user.SetMessage(first_user.Get_Message());
			second_user.Decrypt();
		}

		//	Метод для генерации p
		public void Generate_p()
        {
			p.Clear();
			for (int i = 0; i < message.Count; i++)
				p.Add(PrimeRandom(1000, 10000));
			Generate_q();
		}
		//	Метод для генерации p по полученному размеру сообщения
		public void Generate_p(long _message_size)
        {
			p.Clear();
			for (int i = 0; i < _message_size; i++)
				p.Add(PrimeRandom(1000, 10000));
			Generate_q();
		}
		//	Метод для генерации q
		public void Generate_q()
        {
			q.Clear();
			for (int i = 0; i < p.Count; i++)
				q.Add(PrimeRandom(1000, 10000));

			Calc_n();
			Calc_fi();
			Generate_d();
			Calc_c();
		}
		//	Метод для генерации d
		public void Generate_d()
        {
			d.Clear();
			long _d;
			for (int i = 0; i < p.Count; i++)
				do
				{
					_d = rand.Next(100000, fi[i]);
					if ((_d < fi[i]) && (GCD(_d, fi[i]) == 1))
					{
						d.Add(_d);
						break;
					}
				} while (true);
		}

		//	Метод для возвращения d
		public List<long> Get_d()
        {
			return d;
		}
		//	Метод для возвращения n
		public List<int> Get_n()
        {
			return n;
		}

		//	Метод для вычисления n
		public void Calc_n()
        {
			n.Clear();
			for (int i = 0; i < p.Count; i++)
				n.Add(p[i] * q[i]);
		}
		//	Метод для вычисления fi
		public void Calc_fi()
        {
			fi.Clear();
			for (int i = 0; i < p.Count; i++)
				fi.Add((p[i] - 1) * (q[i] - 1));
		}
		//	Метод для вычисления c
		public void Calc_c()
        {
			c.Clear();
			for (int i = 0; i < p.Count; i++)
				c.Add(Mod_Inverse(d[i], fi[i]));
		}

		//	Метод для шифрования сообщения по принятым d и n
		public void Encrypt(List<long> _d, List<int> _n)
        {
			List<long> temp = new();

			for (int i = 0; i < message.Count; i++)
				//message[i] = Mod_Exp(message[i], _d[i], _n[i]);
				temp.Add(Mod_Exp(message[i], _d[i], _n[i]));

			message = temp;
		}
		//	Метод для дешифрования сообщения
		public void Decrypt()
        {
			List<long> temp = new();

			for (int i = 0; i < message.Count; i++)
				//message[i] = Mod_Exp(message[i], c[i], n[i]);
				temp.Add(Mod_Exp(message[i], c[i], n[i]));

			message = temp;
		}

		//	Метод для вывода данных класса
		public void PrintData()
        {
			Console.WriteLine($"Name: {User_Name}");
			Console.WriteLine($"sender: {sender}");
			Console.WriteLine($"p: {Print_List(ref p)}");
			Console.WriteLine($"q: {Print_List(ref q)}");
			Console.WriteLine($"n: {Print_List(ref n)}");
			Console.WriteLine($"fi: {Print_List(ref fi)}");
			Console.WriteLine($"c: {Print_List(ref c)}");
			Console.WriteLine($"d: {Print_List(ref d)}");
			Console.WriteLine($"message: {Print_List(ref message)}");
			Console.WriteLine($"converted message: {Message_ToString()}");
			Console.WriteLine();
		}
	}
}
