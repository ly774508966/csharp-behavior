using System;
using JetBrains.Annotations;

namespace BehaviorTree.Synchronous
{
    public static class Do
    {
        public static Do<TTarget, TWorld> Create<TTarget, TWorld>([NotNull] Func<TTarget, TWorld, BehaviorStatus> onExecute)
        {
            if (onExecute == null) throw new ArgumentNullException("onExecute");
            return new Do<TTarget, TWorld>(onExecute);
        }
    }

    public class Do<TTarget, TWorld> : IBehavior<TTarget, TWorld>
    {
        private readonly Func<TTarget, TWorld, BehaviorStatus> _onExecute;

        public Do([NotNull] Func<TTarget, TWorld, BehaviorStatus> onExecute)
        {
            if (onExecute == null) throw new ArgumentNullException("onExecute");
            _onExecute = onExecute;
        }

        public BehaviorStatus Execute(TTarget target, TWorld world)
        {
            return _onExecute(target, world);
        }
    }
}
