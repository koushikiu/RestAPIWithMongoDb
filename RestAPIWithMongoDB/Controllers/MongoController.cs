using Microsoft.AspNetCore.Mvc;
using RestAPIWithMongoDB.DataModel;

namespace RestAPIWithMongoDB.Controllers
{
    [ApiController]
    [Route("Mongo")]
    public class MongoController : Controller
    {
        private readonly IMongoRepository _mongoRepositotyRepository;
       public MongoController(IMongoRepository mongoRepositotyRepository)
        {
            _mongoRepositotyRepository = mongoRepositotyRepository;
        }
        [HttpGet("Id")]
        public IActionResult GetUser(Guid guid)
        {
           User user =  _mongoRepositotyRepository.GetUser(guid);
           
            if (user == null)
                return NotFound();
            else return Ok(user);
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _mongoRepositotyRepository.GetUsers();
        }
        [HttpPost]
        public IActionResult Post(User user)
        {
            return Ok(_mongoRepositotyRepository.CreateUserAsync(user));
        }
        [HttpPut("Id")]
        public async Task<IActionResult>  UpdateUser(Guid guid, [FromBody] User userNeedToUpdate)
        {
            User user = _mongoRepositotyRepository.GetUser(guid);

            if (user == null)
                return NotFound();

            var newUser = user with {  Department = userNeedToUpdate.Department, Name = userNeedToUpdate.Name, Role = userNeedToUpdate.Role };
            await _mongoRepositotyRepository.UpdateUser(newUser);
            return NoContent();
        }
    }
}
