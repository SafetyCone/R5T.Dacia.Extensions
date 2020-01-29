using System;


namespace R5T.Dacia
{
    public static class ServiceAction
    {
        public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();
        public static DoNothingServiceAction AlreadyAdded { get; } = new DoNothingServiceAction();
        //public static DoNothingServiceAction AddedElsewhere { get; } = new DoNothingServiceAction();
    }


    public class ServiceAction<T>
    {
        private Action Action { get; }


        public static implicit operator ServiceAction<T>(DoNothingServiceAction doNothingServiceAction)
        {
            var serviceAction = new ServiceAction<T>(DoNothingServiceAction.DoNothingAction);
            return serviceAction;
        }


        public ServiceAction(Action action)
        {
            this.Action = action;
        }

        public void Run()
        {
            this.Action();
        }
    }
}
