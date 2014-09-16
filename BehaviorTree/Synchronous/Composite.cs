using System;
using JetBrains.Annotations;

namespace BehaviorTree.Synchronous
{
    public abstract class Composite<TTarget, TWorld> : IBehavior<TTarget, TWorld>
    {
        [NotNull] protected IBehavior<TTarget, TWorld>[] Behaviors;

        protected Composite([NotNull] IBehavior<TTarget, TWorld>[] behaviors)
        {
            if (behaviors == null) throw new ArgumentNullException("behaviors");
            Behaviors = behaviors;
        }

        public abstract BehaviorStatus Execute(TTarget target, TWorld world);
    }
}
