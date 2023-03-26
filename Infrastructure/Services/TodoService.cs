using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Services;

namespace Infrastructure.Services
{
	public class TodoService : ITodoService
	{
		private readonly ITodoRepository _todoRepository;
		public TodoService(ITodoRepository todoRepository)
		{
			_todoRepository = todoRepository;
		}

        public async Task Create(TodoEntity entity)
        {
            await _todoRepository.Create(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await Fetch(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Todo with id {id} is not found");
            }
            await _todoRepository.Create(entity);
        }

        public async Task<TodoEntity> Fetch(Guid id)
        {
            return await _todoRepository.Fetch(id);
        }

        public async Task Update(TodoEntity entity)
        {
            var isExist = await _todoRepository.IsExist(entity.Id);
            if (!isExist)
            {
                throw new KeyNotFoundException($"Todo with id {entity.Id} is not found");
            }
            await _todoRepository.Update(entity);
        }
    }
}

