using EmailService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Interfaces
{
    public interface IEmailManager
    {
        Task SendEmailAsync(EmailParams emailparams);
    }
}
