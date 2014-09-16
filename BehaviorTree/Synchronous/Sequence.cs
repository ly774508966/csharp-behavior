using System;
using JetBrains.Annotations;

namespace BehaviorTree.Synchronous
{
    public static class Sequence
    {
        public static Sequence<TTarget, TWorld> Create<TTarget, TWorld>([NotNull] params IBehavior<TTarget, TWorld>[] behaviors)
        {
            if (behaviors == null) throw new ArgumentNullException("behaviors");
            return new Sequence<TTarget, TWorld>(behaviors);
        }
    }

    public class Sequence<TTarget, TWorld> : Composite<TTarget, TWorld>
    {
        public Sequence([NotNull] params IBehavior<TTarget, TWorld>[] behaviors) : base(behaviors)
        {
        }

        public override BehaviorStatus Execute(TTarget target, TWorld world)
        {
            foreach (var b in Behaviors)
            {
                var status = b.Execute(target, world);
                if (status == BehaviorStatus.Failure) return BehaviorStatus.Failure;
            }
            return BehaviorStatus.Success;
        }
    }
}
