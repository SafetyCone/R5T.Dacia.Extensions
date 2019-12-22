using System;


namespace R5T.Dacia.Internals
{
    /// <summary>
    /// A interface for a holder service that helps with multiple services.
    /// </summary>
    public interface IMultipleServiceHolder<out TService>
    {
        TService Value { get; }
    }
}
