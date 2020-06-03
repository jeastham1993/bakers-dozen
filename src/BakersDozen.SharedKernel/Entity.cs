using System;

namespace BakersDozen.SharedKernel
{
    public abstract class Entity<TId>
	{
		public TId Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}
