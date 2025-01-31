﻿using System.Text.Json;
using MassTransit.PostgresOutbox.Abstractions;
using MassTransit.PostgresOutbox.Entities;
using MassTransit.PostgresOutbox.Enums;

namespace MassTransit.PostgresOutbox.Extensions;

public static class OutboxDbContextExtensions
{
   public static Guid AddToOutbox<T>(this IOutboxDbContext dbContext, T message)
   {
      var entity = new OutboxMessage
      {
         Id = Guid.NewGuid(),
         CreatedAt = DateTime.UtcNow,
         State = MessageState.New,
         UpdatedAt = null,
         Payload = JsonSerializer.Serialize(message),
         Type = typeof(T).AssemblyQualifiedName!
      };

      dbContext.OutboxMessages.Add(entity);

      return entity.Id;
   }
}