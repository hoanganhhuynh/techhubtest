using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class TodoRepository : ITodoRepository
	{
        private readonly TodoDbContext _dbContext;
        public TodoRepository(TodoDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public async Task Create(TodoEntity entity)
        {
            _dbContext.Todos.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task Delete(TodoEntity entity)
        {
            _dbContext.Todos.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task<TodoEntity> Fetch(Guid id)
        {
            return await _dbContext.Todos.Where(entity => entity.Id == id).SingleOrDefaultAsync();
        }

        public async Task<bool> IsExist(Guid id)
        {
            return await _dbContext.Todos.AnyAsync(todo => todo.Id == id);
        }

        public async Task Update(TodoEntity entity)
        {
            _dbContext.Todos.Update(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}

