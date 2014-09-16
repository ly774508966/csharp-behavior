using System;
using JetBrains.Annotations;

namespace BehaviorTree.Synchronous
{
    public static class Selector
    {
        public static Selector<TTarget, TWorld> Create<TTarget, TWorld>([NotNull] params IBehavior<TTarget, TWorld>[] behaviors)
        {
            if (behaviors == null) throw new ArgumentNullException("behaviors");
            return new Selector<TTarget, TWorld>(behaviors);
        }
    }

    public class Selector<TTarget, TWorld> : Composite<TTarget, TWorld>
    {
        public Selector([NotNull] IBehavior<TTarget, TWorld>[] behaviors) : base(behaviors)
        {
        }

        public override BehaviorStatus Execute(TTarget target, TWorld world)
        {
            foreach (var b in Behaviors)
            {
                var status = b.Execute(target, world);
                if (status == BehaviorStatus.Success) return BehaviorStatus.Success;
            }
            return BehaviorStatus.Failure;
        }
    }
}
