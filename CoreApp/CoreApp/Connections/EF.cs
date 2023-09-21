using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Connections
{
    public class EFCORE
    {
        public static EFContext _context;
        public static void OpenEF()
        {
            _context = new EFContext();
        }
    }
}
