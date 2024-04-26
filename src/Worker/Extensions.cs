using System.Reflection;
using MassTransit;

namespace Worker;

internal static class Extensions
{
    internal static class AddModuleToBus
    {
        public static void Add(IBusRegistrationConfigurator bus, IEnumerable<Assembly?> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                bus.AddConsumers(assembly);
                bus.AddSagaStateMachines(assembly);
                bus.AddSagas(assembly);
                bus.AddActivities(assembly);
            }
        }
    }
}