using CainAbel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CainAbel.Util
{
    public class Uniqe
    {
       readonly TablesConection context;
        public Uniqe(TablesConection context)
        {
            this.context = context;
        }

      public  int isUnique(User u) {
         bool username_is_uniqe=context.UserTable.Where(t => t.Username == u.Username).ToList().Count == 0;
            bool email_is_unique = context.UserTable.Where(t => t.Email == u.Email).ToList().Count == 0;
            if (!username_is_uniqe) return 1;
            if (!email_is_unique) return 2;
            return 3;
        
        }
    }
}
