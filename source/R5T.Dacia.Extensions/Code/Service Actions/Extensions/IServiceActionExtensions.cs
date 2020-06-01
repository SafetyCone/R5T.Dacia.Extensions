using System;


namespace R5T.Dacia
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Allow running a service action more than once by creating a new service action.
        /// </summary>
        public static IServiceAction<T> Again<T>(this IServiceAction<T> serviceAction)
        {
            var newServiceAction = ServiceAction<T>.New(serviceAction.Action);
            return newServiceAction;
        }
    }
}
