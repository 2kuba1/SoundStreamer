using System.Net;

namespace Shared.Exceptions;

public abstract class SoundStreamerException(string message) : Exception(message)
{
    public abstract HttpStatusCode HttpSatStatusCode { get; }
}