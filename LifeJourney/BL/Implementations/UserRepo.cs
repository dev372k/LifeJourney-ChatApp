using BL.DTOs;
using BL.Interfaces;
using DAL;
using DAL.Entities;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementations
{
    public class UserRepo : IUserRepo
    {
        private ApplicationDBContext _context;
        public UserRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public GetUserDTO Get(string email)
        {
            var user = _context.Users.FirstOrDefault(_ => _.Email == email);
            if (user != null)
                return new GetUserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.PasswordHash
                };
            return null;
        }
        
        public GetUserDTO Add(AddUserDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = SecurityHelper.GenerateHash(dto.Password)
            };
           _context.Users.Add(user);

            _context.SaveChanges();

            return new GetUserDTO();
        }
    }
}
