﻿using System.Reflection;
using EGID.Application.Common.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EGID.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}