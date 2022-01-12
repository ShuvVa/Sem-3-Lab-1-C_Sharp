using Encryptions;

namespace Program
{
    public class Program
    {
		static void DH()
		{
			User Server = new ();
			long g = 0, p = 0;
			Server.Generate_g_p(ref g, ref p);

			Diffie_Hellman Alice = new(g, p), Bob = new (g, p);

			Alice.SetUser_Name("Alice");
			Bob.SetUser_Name("Bob");

			Diffie_Hellman DataExchage = new (ref Alice, ref Bob);

			Alice.PrintData();
			Bob.PrintData();
		}
		//	Вызов тестового экзмемпляра работы шифра Шамира
		static void SH()
		{
			User Server = new ();
			long p = Server.PrimeRandom(1000000, 10000000);

			Shamir Alice = new (p, true), Bob = new(p);

			Alice.SetUser_Name("Alice");
			Bob.SetUser_Name("Bob");

			Alice.PrintData();

			Shamir DataExchange = new (ref Alice, ref Bob);

			Bob.PrintData();
		}
		//	Вызов тестового экзмемпляра работы шифра Эль-Гамаля
		static void EL()
		{
			User Server = new ();
			int g = 0, p = 0;
			Server.Generate_g_p(ref g, ref p);

			ElGamal Alice = new (p, g, true), Bob = new (p, g);

			Alice.SetUser_Name("Alice");
			Bob.SetUser_Name("Bob");

			Alice.PrintData();

			ElGamal ExchangeData = new (ref Alice, ref Bob);

			Bob.PrintData();
		}
		//	Вызов тестового экзмемпляра работы шифра RSA
		static void Rsa()
		{
			RSA Alice = new (true), Bob = new ();

			Alice.SetUser_Name("Alice");
			Bob.SetUser_Name("Bob");

			Alice.PrintData();

			RSA ExchangeData = new (ref Alice, ref Bob);

			Bob.PrintData();
		}

		static void Main(string[] args)
		{

			char choice;
			Console.Write("Введите первую букву алгоритма для его запуска:\n D: Алгоритм Диффи-Хеллмана;\n S: Шифр Шамира;\n E: Шифр Эль-Гамаля;\n R: Шифр RSA.\n\n: ");
			choice = Convert.ToChar(Console.ReadKey().KeyChar);
			Console.WriteLine();
			switch (choice)
			{

				case '1':
					{
						DH();
						break;
					}
				case '2':
					{
						SH();
						break;
					}
				case '3':
					{
						EL();
						break;
					}
				case '4':
					{
						Rsa();
						break;
					}
				case 'E':
					{
						EL();
						break;
					}
				case 'e':
					{
						EL();
						break;
					}
				case 'D':
					{
						DH();
						break;
					}
				case 'd':
					{
						DH();
						break;
					}
				case 'R':
					{
						Rsa();
						break;
					}
				case 'r':
					{
						Rsa();
						break;
					}
				case 'S':
					{
						SH();
						break;
					}
				case 's':
					{
						SH();
						break;
					}
				default:
					{
						Console.WriteLine( "Вы ввели неправильный символ. До свидания." );
						break;
					}

			}

		}
	}
}