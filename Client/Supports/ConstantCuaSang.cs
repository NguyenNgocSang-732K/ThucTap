using System.Net;

namespace Supports
{
    public class ConstantCuaSang
    {
        public static string IP4()
        {
            string ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            return ip;
        }

        public static string search = "Collate latin1_general_ci_ai";
        public static int size = 5;
    }
}
