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
    public class ArrivalsController : ControllerBase
    {
        private IArrivalsRepository _repository;
        private IDepaturesRepository _departuresRepository;

        public ArrivalsController(IArrivalsRepository repository, IDepaturesRepository departureRepo)
        {
            _repository = repository;
            _departuresRepository = departureRepo;
        }

        // GET: api/<ArrivalsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Arrival>> Get()
        {
            List<Arrival> result = _repository.GetAll();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        // GETBYQR api/<ArrivalsController>/6
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("qr={qr}")]
        public ActionResult<List<Arrival>> GetByQr(int qr)
        {
            var arrivals = _repository.GetAll().Where(a => a.QrCode == qr).ToList();
            if (arrivals.Count == 0)
            {
                return NotFound("No arrivals with that QR code exist");
            }
            return arrivals;
        }

        // GETBYID api/<ArrivalsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id={id}")]
        public ActionResult<Arrival> GetById(int id)
        {
            if (_repository.GetByID(id) == null)
                return NotFound("No arrival with that id exists");
            return _repository.GetByID(id);
        }

        // POST api/<ArrivalsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult Post([FromBody] Arrival newArrival)
        {
            try
            {
                // Get the current date without the time
                var currentDate = DateTime.Now.Date;

                // Check the counts of Arrivals and Departures with the same QR code exists today
                var arrivalCount = _repository.GetAll().Count(a =>
                    a.QrCode == newArrival.QrCode &&
                    a.ArrivalTime.Date == currentDate);

                var departureCount = _departuresRepository.GetAll().Count(d =>
                    d.QrCode == newArrival.QrCode &&
                    d.DepartureTime.Date == currentDate);

                // If there are more arrivals than departures, add a departure
                if (arrivalCount > departureCount)
                {
                    var departure = new Departure
                    {
                        Id = 0,
                        QrCode = newArrival.QrCode,
                        Name = newArrival.Name,
                        DepartureTime = DateTime.Now
                    };

                    _departuresRepository.Add(departure);
                    return Created("api/Departures/" + departure.Id, departure);
                }

                // If there are less arrivals than departures, add an arrival
                Arrival createdArrival = _repository.Add(newArrival);
                return Created("api/Arrivals/" + createdArrival.Id, createdArrival);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                       ex is ArgumentOutOfRangeException ||
                                       ex is ArgumentException ||
                                       ex is InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }



        // PUT api/<ArrivalsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Arrival> Put(int id, [FromBody] Arrival updates)
        {
            try
            {
                Arrival? updatedArrival = _repository.Update(id, updates);
                if (updatedArrival == null)
                {
                    return NotFound();
                }
                return Ok(updatedArrival);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                       ex is ArgumentOutOfRangeException ||
                                       ex is ArgumentException)
            {
                return BadRequest(ex.InnerException);
            }
        }

        // DELETE api/<ArrivalsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Arrival> Delete(int id)
        {
            Arrival? deletedArrival = _repository.Delete(id);
            if (deletedArrival == null)
            {
                return NotFound();
            }
            return Ok(deletedArrival);
        }
    }
}