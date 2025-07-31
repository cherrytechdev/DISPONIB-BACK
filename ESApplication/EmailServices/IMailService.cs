using ESApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.EmailServices
{
    public interface IMailService
    {
        Task SendEmailAsync();
    }
}
