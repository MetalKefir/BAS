using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBAS.BASServiceModelClient client = new ServiceBAS.BASServiceModelClient("BasicHttpBinding_BAS.ServiceModel");
        }
    }
}
