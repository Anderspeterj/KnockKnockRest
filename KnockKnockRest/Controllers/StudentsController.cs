using Microsoft.AspNetCore.Mvc;
using KnockKnockRest.Models;
using KnockKnockRest.Repositories;
using KnockKnockRest.Interfaces;
using Microsoft.AspNetCore.Cors;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KnockKnockRest.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private IStudentsRepository _repository;

        public StudentsController(IStudentsRepository repository)
        {
            _repository = repository;   
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            List<Student> result = _repository.GetAll();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/<StudentsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{qr}")]
        public ActionResult<Student> Get(int qr)
        {
            if (_repository.GetByID(qr) == null)
                return NotFound("No student with that qr exists");
            return _repository.GetByID(qr);
        }

        // POST api/<StudentsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student newStudent)
        {
            try
            {
                Student createdStudent = _repository.Add(newStudent);
                return Created("api/Students/" + createdStudent.Id, createdStudent);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                       ex is ArgumentOutOfRangeException ||
                                       ex is ArgumentException)
            {
                return BadRequest(ex.InnerException);
            }
        }

        // PUT api/<StudentsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Student> Put(int id, [FromBody] Student updates)
        {
            try
            {
                Student? updatedStudent = _repository.Update(id, updates);
                if (updatedStudent == null)
                {
                    return NotFound();
                }
                return Ok(updatedStudent);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                       ex is ArgumentOutOfRangeException ||
                                       ex is ArgumentException)
            {
                return BadRequest(ex.InnerException);
            }
        }

        // DELETE api/<StudentsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Student> Delete(int id)
        {
            Student? deletedStudent = _repository.Delete(id);
            if (deletedStudent == null)
            {
                return NotFound();
            }
            return Ok(deletedStudent);
        }
    }
}
