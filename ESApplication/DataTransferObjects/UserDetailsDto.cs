using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace ESApplication.AggregateModels
{
    public class UserDetailsDto
    {
        public string userid { get; set; }
        public string roleid { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; } 
        public string email { get; set; }
        public string mobile { get; set; } 
        public string jwttoken { get; set; }
        public DateTime createdon { get; set; }
        public int isactive { get; set; }
        public Int16 type { get; set; }
        public string businessid { get; set; }
        public string comment { get; set; }
        public Int16 isquiz { get; set; }
        public string title { get; set; }


    }
}
