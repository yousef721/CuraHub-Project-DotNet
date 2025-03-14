using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; } = null!;
        [EmailAddress]
        public string SenderEmail { get; set; } = null!;

        public string SenderMessage { get; set; } = null!;
    }
}
