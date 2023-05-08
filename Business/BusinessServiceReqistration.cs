using Business.Abstracts;
using Business.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business;

public static class BusinessServiceReqistration
{

    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentManager>();
        return services;
    }
}
