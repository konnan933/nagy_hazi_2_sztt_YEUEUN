using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Logic
{
    public class CurrentUser
    {
        private static CurrentUser instance;
        public static CurrentUser Instance => instance ??= new CurrentUser();

        private CurrentUser() { }

        public int GetCurrentUserId() => 17; // Fix beégetett azonosító
    }
}
