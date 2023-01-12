﻿using eventful_api_master.Context;
using eventful_api_master.Interface;
using eventful_api_master.Models;
using Microsoft.EntityFrameworkCore;

namespace eventful_api_master.Repository
{
    public class UserRepository: IUserRepository
    {
        readonly DatabaseContext _dbContext = new();

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetUsers()
        {
            try
            {
                return _dbContext.Users.Where(x => x.Active).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<User?> GetUser(int id)
        {
            try
            {
                User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                return user;
            }
            catch (Exception)
            {        
                
                throw;
            }
        }
        public async Task UpdateUser(User user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                //_dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User> AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                user.CreationUser = user.Id;
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> DeleteUser(int id)
        {
            try
            {
                User? user = await _dbContext.Users.FindAsync(id);
                if (user != null)
                {
                    user.Active = false;
                    user.ChangeDate = DateTime.Now;
                    user.ChangeUser = user.Id;

                    _dbContext.Entry(user).State = EntityState.Modified;
                    _dbContext.SaveChangesAsync();
                    return user;
                }

                throw new ArgumentNullException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckUser(int id) => _dbContext.Users.Any(x => x.Id == id && x.Active);

    }
}
