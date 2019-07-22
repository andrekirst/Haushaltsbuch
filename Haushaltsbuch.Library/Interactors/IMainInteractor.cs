using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Interactors
{
    public interface IMainInteractor
    {
        Task ExecuteAsync(string[] args);
    }
}