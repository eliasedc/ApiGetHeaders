using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {        
        [HttpPost(Name = "Login")]
        public List<string> Login(LoginDTO pLogin)
        {            
            List<string> result = new List<string>();
            this.Request.Headers.ToList().ForEach(r => result.Add($"Key: {r.Key} Value: {r.Value}"));

            return result;
        }
    }
}
