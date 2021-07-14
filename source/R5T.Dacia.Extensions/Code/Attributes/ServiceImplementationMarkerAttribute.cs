using System;


namespace R5T.Dacia
{
    /// <summary>
    /// Marks a class as being a service implementation (in cases where the service implemenation is in a code file outside the usual service implementations directory).
    /// Also allows marking a class as *not* being a service implementation. (Useful for decorating extraneous classes that happen to be in a service implementation code file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceImplementationMarkerAttribute : Attribute
    {
        private bool zIsServiceImplementation;
        public bool IsServiceImplementation
        {
            get
            {
                return this.zIsServiceImplementation;
            }
        }


        public ServiceImplementationMarkerAttribute(
            bool isServiceImplementation = true)
        {
            this.zIsServiceImplementation = isServiceImplementation;
        }
    }
}
