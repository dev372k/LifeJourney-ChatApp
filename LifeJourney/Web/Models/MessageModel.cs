using DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class MessageModel
    {
        public string Text { get; set; } = string.Empty;
        public bool IsBot { get; set; } = false;
        public string CreatedOn { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
    }
}
