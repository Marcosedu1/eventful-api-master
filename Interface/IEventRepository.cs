using eventful_api_master.Models;

namespace eventful_api_master.Interface
{
    public interface IEventRepository
    {
        public List<Event> GetEvents();
        public Task<Event?> GetEvent(int id);
        public Task<Event> AddEvent(Event eventData);
        public Task UpdateEvent(Event eventData);
        public Task<Event> DeleteEvent(Event eventData);
        public bool CheckEvent(int id);
    }
}
