using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;


namespace MassTransitShared
{
    public class MediatorAdapterBase : ControllerBase
    {
        public readonly MediatorAdapter MediatorAdapter;

        public MediatorAdapterBase(MediatorAdapter mediator)
        {
            MediatorAdapter = mediator;
        }
    }
}
