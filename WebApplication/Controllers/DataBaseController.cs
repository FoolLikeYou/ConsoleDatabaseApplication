using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/database")]
    public class DataBaseController : ControllerBase
    {
        private readonly IRepository _repository;

        public DataBaseController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _repository.GetUsers();
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        
        [HttpGet("sort-user")]
        public async Task<IActionResult> SortUser([FromBody] SortInfo usersInfo)
        {
            var result = await _repository.SortUser(usersInfo);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
       
        [HttpGet("select-user")]
        public async Task<IActionResult> SelectUser([FromBody] SelectInfo usersInfo)
        {
            var result = await _repository.SelectUser(usersInfo);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
    
        [HttpPost("insert-user")]
        public async Task<IActionResult> InsertUser([FromBody] UsersInfo usersInfo)
        {
            var result = await _repository.InsertUser(usersInfo);
            if (result.IsSuccess)
                return CreatedAtAction("GetUsers", result.Value);
            return BadRequest(result.Error);
        }
        
        [HttpPut("change-user")]
        public async Task<IActionResult> ChangeUser([FromBody] UsersInfo usersInfo)
        {
            var result = await _repository.ChangeUser(usersInfo);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteInfo usersInfo)
        {
            var result = await _repository.DeleteUser(usersInfo);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        
    }
}
