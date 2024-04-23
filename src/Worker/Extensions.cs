using System.Reflection;
using MassTransit;

namespace Worker;

internal static class Extensions
{
    internal static class AddModuleToBus
    {
        public static void Add(IBusRegistrationConfigurator bus, Assembly? type)
        {
            bus.AddConsumers(type);
            bus.AddSagaStateMachines(type);
            bus.AddSagas(type);
            bus.AddActivities(type);
        }
    }
}