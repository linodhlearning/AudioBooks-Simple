using AudioBooks.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioBooks.Api.Repositories
{
    public interface IAudioBookRepository
    {
        Task<IEnumerable<AudioBook>> GetAudioBooks();
        Task<AudioBook> GetAudioBookById(int id);
    }
}
