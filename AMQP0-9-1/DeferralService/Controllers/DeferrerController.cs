using System;
using DeferralService.Models;
using DeferralService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeferralService.Controllers
{
    [ApiController]
    [Route("defer")]
    public class DeferrerController
    {
        private readonly IDeferralRepository _repo;

        public DeferrerController(IDeferralRepository repo)
        {
            _repo = repo;
        }
        
        [HttpPost]
        public IActionResult DeferMessage(DeferMessageCommand cmd)
        {
            _repo.Enqueue(Guid.NewGuid(), cmd);

            return new OkResult();
        }
    }
}