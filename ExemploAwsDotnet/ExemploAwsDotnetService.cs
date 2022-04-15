using System;
using System.Collections.Generic;
using System.Text;
using ExemploAwsDotnet.ServiceModel;
using ServiceStack;

namespace ExemploAwsDotnet
{
    public class MyService : Service
    {
        public object Any(ProjetoStart request) => new ProjetoStartResponse
        {
            Result = $"ProjetoStart, {request.Name}!"
        };
    }
}
