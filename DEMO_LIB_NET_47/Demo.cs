using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_LIB_NET_47
{
    public class Demo
    {
        public void Void()
        {

        }

        public string String()
        {
            return nameof(String);
        }

        public int Int()
        {
            return 0;
        }

        public string ToUpper(string value)
        {
            return value.ToUpper();
        }

        public string ToUpper(string value1, string value2)
        {
            return value1.ToUpper() + " & " + value2.ToUpper();
        }

        public int AddInt(int val1, int val2)
        {
            return val1 + val2;
        }

    }
}
