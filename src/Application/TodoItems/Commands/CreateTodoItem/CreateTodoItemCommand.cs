﻿using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoLists.Queries.ExportTodos;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;
using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public int ListId { get; set; }
        public string Title { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Title = request.Title,
                Done = false
            };

            entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

            _context.TodoItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

    public class CreateTodoItemPostProcessor : IRequestPostProcessor<CreateTodoItemCommand, int>, ICacheInvalidatorPostProcessor
    {
        public InvalidateCacheForQueries QueriesList { get; set; }

        public CreateTodoItemPostProcessor(InvalidateCacheForQueries pairs)
        {
            QueriesList = pairs;
        }

        public Task Process(CreateTodoItemCommand request, int response, CancellationToken cancellationToken)
        {
            QueriesList.Add(typeof(ExportTodosQuery), new ExportTodosQuery { ListId = request.ListId });
            QueriesList.Add(typeof(GetTodosQuery), new GetTodosQuery { });

            return Task.CompletedTask;
        }
    }
}
