using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSafety.Models
{
    public class Comments
    {
        public int ArticleId { get; set; }
        public string? UserId { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }

    }
}
