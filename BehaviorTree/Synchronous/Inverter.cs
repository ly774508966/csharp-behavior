namespace BehaviorTree.Synchronous
{
    public static class Inverter
    {
        public static Not<TTarget, TWorld> Create<TTarget, TWorld>(IBehavior<TTarget, TWorld> behavior)
        {
            return new Not<TTarget, TWorld>(behavior);
        }
    }

    public class Not<TTarget, TWorld> : Decorator<TTarget, TWorld>
    {
        public Not(IBehavior<TTarget, TWorld> behavior) : base(behavior) { }


        protected override BehaviorStatus Decorate(BehaviorStatus status)
        {
            return status == BehaviorStatus.Success ? BehaviorStatus.Failure : BehaviorStatus.Success;
        }
    }
}
