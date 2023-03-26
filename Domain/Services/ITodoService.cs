using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
	public interface ITodoService
	{
		Task<TodoEntity> Fetch(Guid id);
		Task Update(TodoEntity entity);
		Task Delete(Guid id);
		Task Create(TodoEntity entity);
	}
}

