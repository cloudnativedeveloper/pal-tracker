using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _repository;
        private readonly IOperationCounter<TimeEntry> _operationCounter;

        public TimeEntryController(ITimeEntryRepository repository, IOperationCounter<TimeEntry> operationCounter)
        {
            _repository = repository;
            _operationCounter = operationCounter;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            var createdTimeEntry = _repository.Create(timeEntry);
            _operationCounter.Increment(TrackedOperation.Create);

            return CreatedAtRoute("GetTimeEntry", new { id = createdTimeEntry.Id }, createdTimeEntry);
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            if (_repository.Contains(id))
            {
                _operationCounter.Increment(TrackedOperation.Read);
                return Ok(_repository.Find(id));
            } else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            var result = Ok(_repository.List());
            _operationCounter.Increment(TrackedOperation.List);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            if (_repository.Contains(id))
            {
                _operationCounter.Increment(TrackedOperation.Update);
                return Ok(_repository.Update(id, timeEntry));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!_repository.Contains(id))
            {
                return NotFound();
            }

            _repository.Delete(id);

            _operationCounter.Increment(TrackedOperation.Delete);

            return NoContent();
        }
    }
}