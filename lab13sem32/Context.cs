using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLite;

namespace lab13sem32
{
    public class LocalDbService 
    {
        private const string DB_NAME = "laboratory_db.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Research>();
            _connection.CreateTableAsync<Equipment>();
            _connection.CreateTableAsync<Scientists>();
        }
        //Researches
        public async Task<List<Research>> GetResearches() 
        {
            return await _connection.Table<Research>().ToListAsync();
        }
        public async Task<Research> GetById(int id)
        {
            return await _connection.Table<Research>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task Create(Research research)
        {
            await _connection.InsertAsync(research);
        }
        public async Task Update(Research research)
        {
            await _connection.UpdateAsync(research);
        }
        public async Task Delete(Research research)
        {
            await _connection.DeleteAsync(research);
        }

        //Equipment
        public async Task<List<Equipment>> GetEquipment()
        {
            return await _connection.Table<Equipment>().ToListAsync();
        }
        public async Task<Equipment> GetByIdE(int id)
        {
            return await _connection.Table<Equipment>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateE(Equipment equipment)
        {
            await _connection.InsertAsync(equipment);
        }
        public async Task UpdateE(Equipment equipment)
        {
            await _connection.UpdateAsync(equipment);
        }
        public async Task DeleteE(Equipment equipment)
        {
            await _connection.DeleteAsync(equipment);
        }
        //Scientists
        public async Task<List<Scientists>> GetScientists()
        {
            return await _connection.Table<Scientists>().ToListAsync();
        }
        public async Task<Scientists> GetByIdS(int id)
        {
            return await _connection.Table<Scientists>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateS(Scientists scientist)
        {
            await _connection.InsertAsync(scientist);
        }
        public async Task UpdateS(Scientists scientist)
        {
            await _connection.UpdateAsync(scientist);
        }
        public async Task DeleteSc(Scientists scientist)
        {
            await _connection.DeleteAsync(scientist);
        }
    }
}
