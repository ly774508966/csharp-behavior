namespace BehaviorTree.Synchronous
{
    public enum BehaviorStatus
    {
        Success,
        Failure
    }

    public interface IBehavior<in TTarget, in TWorld>
    {
        BehaviorStatus Execute(TTarget target, TWorld world);
    }
}
