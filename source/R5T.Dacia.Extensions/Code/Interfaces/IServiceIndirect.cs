using System;


namespace R5T.Dacia
{
    /// <summary>
    /// Allows creating indirection for a service.
    /// For example, if an IService implementation wants to depend on IService itself, the AddX() method can look for all IService entries in the service collection and replace them with an IServiceIndirect{IService}.
    /// Then depend on an IServiceIndirect{IService} instead of IService.
    /// Any IService consumers are then left unaware.
    /// </summary>
    public interface IServiceIndirect<TService>
    {
        TService Service { get; }
    }
}
