using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    internal class Shamir : User
    {
        private int p;
        private Tuple<int, int>[] key;

        Shamir()
        {
            p = 0;
            key.Append(new Tuple<int, int>(0,0));
        }
        
        Shamir(int _p, bool _sender = false)
        {
            p = _p;
            sender = _sender;
            if (sender)
            {
                InsertMessage();
                Generate_key();
            }
        }
        
        Shamir(ref Shamir first_user, ref Shamir second_user)
        {
            sender = false;

            p = 0;
            message = new ();
            key.Append(new Tuple<int, int>(0, 0));

            first_user.Encode(first_user.Get_Message());

            second_user.Encode(first_user.Get_Message());

            first_user.Decode(second_user.Get_Message());

            second_user.Decode(first_user.Get_Message());
        }

        
        bool check_for_equal_keys(int _key) {
            List <int> k = new ();

            Get_map_key(k);
            

        }
        bool check_for_equal_values(unsigned long long int _value)
        {

        }

        void Get_map_key(ref List <int>_key)
        {
            _key.Clear();
            
            for (int i = 0; i < key.;)
        }
        void Get_map_value(Vector_1D_long_long_int& _value)
        {

        }
        void Generate_key()
        {

        }

        void Encode(Vector_1D_long_long_int _message)
        {

        }
        void Decode(Vector_1D_long_long_int _message)
        {

        }

        void PrintData()
        {

        }
    }
}
