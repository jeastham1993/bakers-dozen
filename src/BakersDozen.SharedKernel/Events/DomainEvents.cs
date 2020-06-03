using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace BakersDozen.SharedKernel.Events
{
	public static class DomainEvents
	{
		[ThreadStatic] //so that each thread has its own callbacks
		private static List<Delegate> actions;

		public static IServiceProvider ServiceProvider { get; set; }

		/// <summary>
		/// Registers a callback to a domain event.
		/// </summary>
		/// <typeparam name="T">The domain event to register to.</typeparam>
		/// <param name="callback">The callback to execute when the event is raised.</param>
		public static void Register<T>(
			Action<T> callback) where T : IDomainEvent
		{
			if (actions == null)
			{
				actions = new List<Delegate>();
			}

			actions.Add(callback);
		}

		/// <summary>
		/// Clear all registered callbacks.
		/// </summary>
		public static void ClearCallbacks()
		{
			actions = null;
		}

		/// <summary>
		/// Raise a domain event.
		/// </summary>
		/// <typeparam name="T">The type of event to raise.</typeparam>
		/// <param name="args">The event data.</param>
		public static Task Raise<T>(
			T args) where T : IDomainEvent
		{
			if (ServiceProvider != null)
			{
				var tasks = new List<Task>();

				foreach (var handler in ServiceProvider.GetServices<Handles<T>>())
				{
					tasks.Add(handler.Handle(args));
				}

				Task.WaitAll(tasks.ToArray());
			}

			if (actions == null)
			{
				return Task.CompletedTask;
			}

			foreach (var action in actions)
			{
				if (action is Action<T>)
				{
					((Action<T>) action)(args);
				}
			}

			return Task.CompletedTask;
		}
	}
}