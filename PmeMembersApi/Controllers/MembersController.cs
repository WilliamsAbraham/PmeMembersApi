using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PmeMembersApi.Controllers
{
    [Route("Members")]
    [ApiController]

    public class MembersController : Controller
    {
        private IRepositoryWrapper repository;
        private ILoggerManager logger;
        public MembersController(IRepositoryWrapper _repo, ILoggerManager logger)
        {
            repository = _repo;
            this.logger = logger;

        }
        
        [HttpGet("GetAll")]
        public IEnumerable<Member> GetAllMembers()
        {
           return repository.MembersRepository.FindAll();
        }

        
        [HttpGet("Get/{id:int}")]
        public IActionResult FindByCondition(int? id)
        {
            
            try
            {
                var result = repository.MembersRepository.FindByCondition(member => member.Id == id).FirstOrDefault();
                if (result == null) 
                { 
                    logger.LogError($"Member with Id {id} was not found");
                    return NotFound();
                }

                else { 
                    logger.LogInfo($"Member with Id{id} was found");
                    return Ok(result);
                } 
            }

            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("Create")]
        public void Create(Member member)
        {
            repository.MembersRepository.Create(member);
            repository.Save();
        }

        [HttpPut("Edit")]
        public void Edit(Member member)
        {
            repository.MembersRepository.Update(member);
            repository.Save();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public void Delete(Member member)
        {
            repository.MembersRepository.Delete(member);
            repository.Save();
        }
    }
}
