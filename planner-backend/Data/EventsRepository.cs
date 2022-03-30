using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using planner_backend.Models;
using System.Linq.Expressions;

namespace planner_backend.Data
{
  public class EventsRepository : IEventsRepository
  {
    private readonly EventsContext _context;

    public EventsRepository(EventsContext context)
    {
      this._context = context;
    }

    public async Task AddEvent(Event _event)
    {
      await this._context.AddAsync(_event);
      await this._context.SaveChangesAsync();
    }

    public async Task DeleteEvent(int id)
    {
      Event eventToDelete = _context.Event.FirstOrDefault(e => e.event_id == id);

      _context.Event.Remove(eventToDelete);
      await _context.SaveChangesAsync();
      
    }

    public async Task<IEnumerable<Event>> GetAllEvents()
    {
      return await _context.Event.ToListAsync();
    }

    public async Task<Event> GetEventById(int id)
    {
      return await _context.Event.FirstOrDefaultAsync(e => e.event_id == id);
    }

    public async Task<IEnumerable<Event>> GetEventsByDate(DateTime dateTime)
    {
      string query = "SELECT * FROM event WHERE YEAR(event_datetime)= " + dateTime.Year + " AND MONTH(event_datetime)=" + dateTime.Month + "AND DAY(event_datetime)=" + dateTime.Day;

      return await _context.Event.FromSqlRaw(query).ToListAsync();
      
    }

    public async Task<IEnumerable<Event>> GetEventsByMonth(DateTime dateTime)
    {
      string query = "SELECT * FROM event WHERE YEAR(event_datetime)= " + dateTime.Year + " AND MONTH(event_datetime)=" + dateTime.Month;
      return await _context.Event.FromSqlRaw(query).ToListAsync();
    }

    public async Task UpdateEvent(Event _event)
    {
      Event eventToUpdate = _context.Event.FirstOrDefault(e => e.event_id == _event.event_id);

      _context.Entry(eventToUpdate).CurrentValues.SetValues(_event);
      await _context.SaveChangesAsync();
    }
  }
}
