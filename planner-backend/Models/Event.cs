using System.ComponentModel.DataAnnotations;

namespace planner_backend.Models
{
  public class Event
  {
    [Key]
    public int event_id { get; set; }
    public string event_text { get; set; }

    public DateTime event_datetime { get; set; }
  }
}
