using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Models
{
    public class AdUser
    {
        
        public string userid
        {
            get;
            set;
        }
        public string username
        {
            get;
            set;
        }
        public string email
        {
            get;
            set;
        }
        public string mobile
        {
            get;
            set;
        } 
        public string jwttoken
        {
            get;
            set;
        }

    }
}
