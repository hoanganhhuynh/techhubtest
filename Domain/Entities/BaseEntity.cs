using System;
namespace Domain.Entities
{
	public class BaseEntity
	{
		public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public BaseEntity()
		{
		}
	}
}

