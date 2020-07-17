using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Dacia
{
    public class CompositeServiceAction<TService> : SingleRunServiceAction<TService>, IServiceAction<IEnumerable<TService>>
    {
        #region Static

        private static Action<IServiceCollection> FromEnumerable(IEnumerable<Action<IServiceCollection>> actions)
        {
            void Foreach(IServiceCollection services)
            {
                foreach (var action in actions)
                {
                    action(services);
                }
            }
            return Foreach;
        }

        #endregion


        public CompositeServiceAction(IEnumerable<Action<IServiceCollection>> actions)
            : base(CompositeServiceAction<TService>.FromEnumerable(actions))
        {
        }

        public CompositeServiceAction(params Action<IServiceCollection>[] actions)
            : base(CompositeServiceAction<TService>.FromEnumerable(actions))
        {
        }
    }
}
