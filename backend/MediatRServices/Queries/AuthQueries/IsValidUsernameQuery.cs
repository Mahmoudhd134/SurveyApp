﻿using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.AuthQueries;

public record IsValidUsernameQuery(string Username):IRequest<Response<bool>>;