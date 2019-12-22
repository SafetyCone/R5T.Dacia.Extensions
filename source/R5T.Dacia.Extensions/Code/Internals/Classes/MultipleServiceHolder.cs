using System;


namespace R5T.Dacia.Internals
{
    public class MultipleServiceHolder<TService> : IMultipleServiceHolder<TService>
    {
        public TService Value { get; }


        public MultipleServiceHolder(TService value)
        {
            this.Value = value;
        }
    }
}
