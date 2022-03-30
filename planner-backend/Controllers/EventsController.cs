#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using planner_backend.Data;
using planner_backend.Models;

namespace planner_backend.Controllers
{ 

    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private EventsRepository eventsRepository;
        

    public EventsController(EventsContext context)
    {
        this.eventsRepository = new EventsRepository(context);
    }

    // GET: /events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        List<Event> events = await eventsRepository.GetAllEvents() as List<Event>;

        if (events == null || events.Count == 0)
        {
          return StatusCode(404, "Event with that id does not exist!");
        }

        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
      var _event = await this.eventsRepository.GetEventById(id);

      if (_event == null)
      {
        return StatusCode(404, "Event with that id does not exist!");
      }

      return _event;
    }

    // PUT: api/Events/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("/update")]
    public async Task PutEvent(Event _event)
    {
      try
      {
        await this.eventsRepository.UpdateEvent(_event);
      }
      catch(Exception ex)
      {
        Debug.WriteLine(ex.Message);
        throw;
      }
    }

    [HttpPost("/eventsByMonth")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsFromMonth(CustomDate date)
    {
      var dateTime = new DateTime(date.Year, date.Month, date.Day);

      var result = await this.eventsRepository.GetEventsByMonth(dateTime);

      if (result == null)
      {
        return StatusCode(404, "Couldn't get the events from month of: " + date.ToString());
      }

      return Ok(result);
    }

    [HttpPost("/eventsByDate")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsFromDate(CustomDate date)
    {
        var dateTime = new DateTime(date.Year, date.Month, date.Day);

        var result = await this.eventsRepository.GetEventsByDate(dateTime);

        if (result == null)
        {
          return StatusCode(404, "Couldn't get the events from day: " + date.ToString());
        }

        return Ok(result);
      }

    // POST: api/Events
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostEvent(Event _event)
    {
      try
      {
        await this.eventsRepository.AddEvent(_event);
        return Ok();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        throw;
      }
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(int id)
    {
      try
      {
        await this.eventsRepository.DeleteEvent(id);
        return Ok();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        throw;
      }
    }

  }
}
