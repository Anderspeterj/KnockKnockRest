using KnockKnockRest.Interfaces;
using KnockKnockRest.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KnockKnockRest.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeparturesController : ControllerBase
    {
        private IDepaturesRepository _repository;

        public DeparturesController(IDepaturesRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<DeparturesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Departure>> Get()
        {
            List<Departure> result = _repository.GetAll();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        // GETBYQR api/<DeparturesController>/6
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("qr={qr}")]
        public ActionResult<List<Departure>> GetByQr(int qr)
        {
            var departures = _repository.GetAll().Where(a => a.QrCode == qr).ToList();
            if (departures.Count == 0)
            {
                return NotFound("No departures with that QR code exist");
            }
            return departures;
        }

        // GETBYID api/<DeparturesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id={id}")]
        public ActionResult<Departure> GetById(int id)
        {
            if (_repository.GetByID(id) == null)
                return NotFound("No departure with that id exists");
            return _repository.GetByID(id);
        }

        // POST api/<DeparturesController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Departure> Post([FromBody] Departure newDeparture)
        {
            try
            {
                Departure createdDeparture = _repository.Add(newDeparture);
                return Created("api/Departures/" + createdDeparture.Id, createdDeparture);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                       ex is ArgumentOutOfRangeException ||
                                       ex is ArgumentException ||
                                       ex is InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<DeparturesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Departure> Delete(int id)
        {
            Departure? deletedDeparture = _repository.DeleteById(id);
            if (deletedDeparture == null)
            {
                return NotFound();
            }
            return Ok(deletedDeparture);
        }
    }
}
