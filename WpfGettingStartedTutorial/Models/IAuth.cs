using System.Threading.Tasks;

namespace WpfGettingStartedTutorial.Models
{
    public interface IAuth
    {
        Task AuthorizeAsync();
    }
}