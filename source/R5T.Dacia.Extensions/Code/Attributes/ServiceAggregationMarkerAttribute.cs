using System;


namespace R5T.Dacia
{
    /// <summary>
    /// Marks an interface as being a service aggregation definition.
    /// Also allows specifying that an interface is *not* a service aggregation. This is useful for decorating extraneous interface declarations that also happen to be in a service aggregation definition file, or in a file in the service aggregation definitions directory.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class ServiceAggregationMarkerAttribute : Attribute
    {
        private readonly bool zIsServiceAggregation;
        public bool IsServiceAggregation
        { 
            get
            {
                return this.zIsServiceAggregation;
            }
        }


        public ServiceAggregationMarkerAttribute(
            bool isServiceAggregation = true)
        {
            this.zIsServiceAggregation = isServiceAggregation;
        }
    }
}
