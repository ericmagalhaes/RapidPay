using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Repository.Repositories;
using RapidPay.Shared;
using RapidPay.Web.API.Services;

namespace RapidPay.Web.API.Controllers
{
    public class IdentityController:ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Username or pass are invalid" });
            
            var token = TokenService.GenerateToken(user);
            
            user.Password = "";
            
            return new
            {
                user = user,
                token = token
            };
        }
    }
}