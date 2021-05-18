using System;


namespace R5T.Dacia
{
    public static class ServiceHelper
    {
        public static T GetNullService<T>()
            where T: class
        {
            return null;
        }

        public static bool IsNullService<T>(T service)
            where T: class
        {
            var nullService = ServiceHelper.GetNullService<T>();

            var output = service == nullService;
            return output;
        }
    }
}
