using BL.DTOs;
using BL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementations
{
    public class MessageRepo : IMessageRepo
    {
        private ApplicationDBContext _context;
        public MessageRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task Add(AddMessageDTO dto)
        {
            var message = new Message
            {
                IsBot = dto.IsBot,
                Text = dto.Text,
                CreateOn = DateTime.Now,
                UserId = dto.UserId,
            };
            await _context.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public List<GetMessageDTO> Get(int userId)
        {
            return _context.Messages.Include(_ => _.User).Where(_ => _.UserId == userId).Select(_ => new GetMessageDTO
            {
                Id = _.Id,
                IsBot = _.IsBot,
                Text = _.Text,
                CreatedOn = _.CreateOn,
                Username = _.IsBot == true ? "Bot" : _.User.Name
            }).ToList();
        }
    }
}
