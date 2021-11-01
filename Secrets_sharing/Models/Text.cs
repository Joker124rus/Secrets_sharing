using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secrets_sharing.Models
{
    public class Text
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public User User { get; set; }
        public bool DeleteOnDownload { get; set; }
    }
}
