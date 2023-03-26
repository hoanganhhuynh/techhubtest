using System;
using FluentValidation;

namespace Domain.Entities
{
	public class TodoEntity: BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime DueDate { get; set; }
		public int ProcessDate { get; set; }
		public TodoEntity()
		{
		}
	}

	public class ToDoValidator : AbstractValidator<TodoEntity>
	{
		public ToDoValidator()
		{
			RuleFor(x => x.Title).NotEmpty();
			RuleFor(x => x.Description).NotEmpty();
			RuleFor(x => x.DueDate).GreaterThan(DateTime.Now);
			RuleFor(x => x.ProcessDate).GreaterThan(0);
		}
	}
}

