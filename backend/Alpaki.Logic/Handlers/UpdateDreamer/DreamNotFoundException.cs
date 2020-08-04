namespace Alpaki.Logic.Handlers.UpdateDreamer
{
    public class DreamNotFoundException : LogicException
    {
        public override string Code => "DREAM_NOT_FOUND";

        public DreamNotFoundException(long dreamId) : base($"Dream with given DreamId does not exists [DreamId={dreamId}]")
        {
        }
    }
}