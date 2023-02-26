using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public ActionResult<IEnumerable<Member>> GetAllMembers()
        {
            try
            {
                var result = repository.MembersRepository.FindAll().ToList();
                if(result.Any())
                {
                    return result;              
                }
                else { return NoContent();}
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message.ToString());
                return StatusCode(500,"Something went wrong");
            }
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
                logger.LogError(ex.Message.ToString());
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("Create")]
        public ActionResult Create(Member member)
        {
           try
            {
                if (member != null)
                {
                    repository.MembersRepository.Create(member);
                    repository.Save();
                    return Ok(member);
                }
                else
                {
                    return BadRequest("Invalid data");
                }
            }

            catch(Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                return StatusCode(500, "internal server error");
            }
        }


        [HttpPut("Edit")]
        public ActionResult Edit(Member member)
        {
           try
            {
                if (member != null)
                {
                    repository.MembersRepository.Update(member);
                    repository.Save();
                    return Ok(member);
                }
             else
                { return BadRequest("Nothing to update"); }
            }
            catch(Exception ex)
            { 
                logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        public ActionResult Delete(Member member)
        {
            try 
            {
                repository.MembersRepository.Delete(member);
                repository.Save();
                return Ok(member);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                return StatusCode(500, "internal server error");
            }
        }
    }
}
