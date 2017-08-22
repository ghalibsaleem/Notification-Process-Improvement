using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EmailMessageEntity
    {
        
        public EmailMessageEntity(string subject, string body, DateTime date)
        {
            Subject = subject;
            this.Body = body;
            this.Date = date;
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }

        
    }
}
