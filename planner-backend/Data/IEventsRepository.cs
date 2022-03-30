using Microsoft.AspNetCore.Mvc;
using planner_backend.Models;

namespace planner_backend.Data
{
  public interface IEventsRepository
  {
    Task<IEnumerable<Event>> GetAllEvents();
    Task<IEnumerable<Event>> GetEventsByDate(DateTime dateTime);
    Task<IEnumerable<Event>> GetEventsByMonth(DateTime dateTime);
    Task<Event> GetEventById(int eventId);

    Task AddEvent(Event _event);

    Task DeleteEvent(int id);

    Task UpdateEvent(Event _event);
  }
}
