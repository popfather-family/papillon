﻿using Papillon.Mediator;

namespace Papillon.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}