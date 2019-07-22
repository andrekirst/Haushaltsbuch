namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface IApplyEvent<TEvent>
    {
        void ApplyEvent(TEvent @event);
    }
}