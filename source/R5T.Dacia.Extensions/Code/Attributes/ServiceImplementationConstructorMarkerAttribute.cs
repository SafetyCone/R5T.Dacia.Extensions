using System;


namespace R5T.Dacia
{
    /// <summary>
    /// If a service implementation has multiple constructors (which for Automatic Construction, they shouldn't), allow marking one of the 
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public sealed class ServiceImplementationConstructorMarkerAttribute : Attribute
    {
        private readonly bool zIsServiceImplementationConstructor;
        public bool IsServiceImplementationConstructor
        {
            get
            {
                return this.zIsServiceImplementationConstructor;
            }
        }


        public ServiceImplementationConstructorMarkerAttribute(
            bool isServiceImplementationConstructor = true)
        {
            this.zIsServiceImplementationConstructor = isServiceImplementationConstructor;
        }
    }
}
