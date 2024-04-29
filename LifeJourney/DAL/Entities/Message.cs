using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Message : Base
    {
        public string Text { get; set; } = string.Empty;
        public bool IsBot { get; set; } = false;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
