using AudioBooks.Domain; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioBooks.Api.Repositories.Contracts
{
    public interface IAudioBookRepository
    {
        Task<IEnumerable<AudioBook>> GetAudioBooks();
        Task<AudioBook> GetAudioBookById(int id);
        Task<int> CreateAudioBook(AudioBook domainModel);
        Task<bool> UpdateAudioBook(AudioBook domainModel); 
    }
}
