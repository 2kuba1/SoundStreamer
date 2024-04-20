using System.Reflection;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Configuration;

public class ConsumersRegistrator
{
    public static void Register(Assembly assembly, IServiceCollection services)
    {
        var consumerTypes = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IConsumer)));

        var registerConsumer = typeof(DependencyInjectionConsumerRegistrationExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(m =>
                m.Name == "RegisterConsumer" &&
                m.GetGenericArguments().Length == 1 &&
                m.GetParameters().Length == 1);

        foreach (var consumerType in consumerTypes)
        {
            registerConsumer.MakeGenericMethod(consumerType).Invoke(services, [services]);
        }
    }
}