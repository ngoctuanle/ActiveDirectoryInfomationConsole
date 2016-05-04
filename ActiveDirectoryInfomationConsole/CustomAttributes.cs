using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAD2
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]
    class CustomAttributes : System.Attribute
    {
        private string OUname;

        public CustomAttributes(string OUname)
        {
            this.OUname = OUname;
        }

        public string getOUname()
        {
            return OUname;
        }
    }
}
