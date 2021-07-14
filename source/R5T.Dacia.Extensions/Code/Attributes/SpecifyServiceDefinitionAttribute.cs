using System;


namespace R5T.Dacia
{
    /// <summary>
    /// Specifies the service definition interface for a service implementation class.
    /// Useful in situations where a service implementation implements multiple interfaces, only one of which should be considered its service definition. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SpecifyServiceDefinitionAttribute : Attribute
    {
        private readonly Type zType;
        public Type Type
        {
            get
            {
                return this.zType;
            }
        }


        public SpecifyServiceDefinitionAttribute(Type type)
        {
            this.zType = type;
        }
    }
}
