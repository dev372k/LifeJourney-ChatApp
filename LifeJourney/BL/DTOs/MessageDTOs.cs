using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class AddMessageDTO
    {
        public string Text { get; set; } = string.Empty;
        public bool IsBot { get; set; } = false;
        public int UserId { get; set; }
    }

    public class GetMessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsBot { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public string Username { get; set; }
    }
}
