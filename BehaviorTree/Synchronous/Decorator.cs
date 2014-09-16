using System;
using JetBrains.Annotations;

namespace BehaviorTree.Synchronous
{
    public abstract class Decorator<TTarget, TWorld> : IBehavior<TTarget, TWorld>
    {
        protected IBehavior<TTarget, TWorld> Behavior;

        protected Decorator([NotNull] IBehavior<TTarget, TWorld> behavior)
        {
            if (behavior == null) throw new ArgumentNullException("behavior");
            Behavior = behavior;
        }

        public BehaviorStatus Execute(TTarget target, TWorld world)
        {
            return Decorate(Behavior.Execute(target, world));
        }

        protected abstract BehaviorStatus Decorate(BehaviorStatus ex);
    }
}
