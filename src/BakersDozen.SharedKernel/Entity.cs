using System;

namespace BakersDozen.SharedKernel
{
    public abstract class Entity
	{
		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}
