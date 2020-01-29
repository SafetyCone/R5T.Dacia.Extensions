using System;


namespace R5T.Dacia
{
    public class DoNothingServiceAction
    {
        public static Action DoNothingAction { get; } = () => { };


        // Does not work: "The type or namespace 'T' could not be found ...".
        // See: https://social.msdn.microsoft.com/Forums/en-US/e591d550-4df9-49e9-a5a1-3e28d602e263/introduce-generic-parameter-in-explicit-type-conversion?forum=csharplanguage
        // Instead, put operator on ServiceAction<T>!
        //public static implicit operator ServiceAction<T>(DoNothingServiceAction doNothingServiceAction)
        //{
        //    var serviceAction = new ServiceAction<T>(DoNothingServiceAction.DoNothingAction);
        //    return serviceAction;
        //}
    }
}
