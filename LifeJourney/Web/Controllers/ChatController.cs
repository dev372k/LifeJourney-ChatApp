using BL.DTOs;
using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ChatController : Controller
    {
        private IMessageRepo _messageRepo;
        private StateHelper _stateHelper;

        public ChatController(IMessageRepo messageRepo,
            StateHelper stateHelper) 
        {
            _messageRepo = messageRepo;
            _stateHelper = stateHelper;
        }
        [Authorize]
        [HttpGet("Chat")]
        public IActionResult Index()
        {
            return View(_messageRepo.Get(_stateHelper.GetUserData().Id));
        }

        [Authorize]
        //[HttpGet("Chat")]
        [HttpPost]
        public IActionResult Message(AddMessageDTO dto)
        {
            dto.UserId = _stateHelper.GetUserData().Id;
            _messageRepo.Add(dto).GetAwaiter().GetResult();
            return View();
        }
    }
}
