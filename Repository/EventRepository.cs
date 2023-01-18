using eventful_api_master.Context;
using eventful_api_master.Interface;
using eventful_api_master.Models;
using Microsoft.EntityFrameworkCore;

namespace eventful_api_master.Repository
{
    public class EventRepository: IEventRepository
    {
        readonly DatabaseContext _dbContext = new();

        public EventRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Event> GetEvents()
        {
            try
            {
                return _dbContext.Events.Where(x => x.Active).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Event?> GetEvent(int id)
        {
            try
            {
                Event? eventData = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                return eventData;
            }
            catch (Exception)
            {        
                
                throw;
            }
        }
        public async Task UpdateEvent(Event eventData)
        {
            try
            {
                _dbContext.Entry(eventData).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Event> AddEvent(Event eventData)
        {
            try
            {
                _dbContext.Events.Add(eventData);
                await _dbContext.SaveChangesAsync();

                _dbContext.Entry(eventData).State = EntityState.Modified;
                _dbContext.SaveChangesAsync();
                return eventData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Event> DeleteEvent(Event eventData)
        {
            try
            {
                _dbContext.Entry(eventData).State = EntityState.Modified;
                _dbContext.SaveChangesAsync();
                return eventData;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckEvent(int id) => _dbContext.Events.Any(x => x.Id == id && x.Active);

    }
}
