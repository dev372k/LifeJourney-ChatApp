using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IMessageRepo
    {
        Task Add(AddMessageDTO dto);
        List<GetMessageDTO> Get(int userId);
    }
}
