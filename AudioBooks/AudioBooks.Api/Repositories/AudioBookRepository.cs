using AudioBooks.Data;
using AudioBooks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AudioBooks.Model;
using AudioBooks.Api.Repositories.Contracts;

namespace AudioBooks.Api.Repositories
{
    /// <summary>
    /// Domain models are returned in this Data Repo to avoid dependency on mapping logics
    /// </summary>
    public class AudioBookRepository : IAudioBookRepository
    {
        private AudioBookContext _context;

        public AudioBookRepository(AudioBookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<AudioBook>> GetAudioBooks()
        {
            return await this._context.AudioBooks.AsNoTracking()
                .Include(a => a.Author)
                .Include(a => a.Audio)
                .Include(a => a.Publisher)
                .OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<AudioBook> GetAudioBookById(int id)
        {
            return await _context.AudioBooks.AsNoTracking()
                .Include(a => a.Author)
                .Include(a => a.Audio)
                .Include(a => a.Publisher)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> CreateAudioBook(AudioBook model)
        {
            // todo: validate
            //add to context
            _context.AudioBooks.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> UpdateAudioBook(AudioBook model)
        {
            // todo: validate
            //add to context
            var audiobook= _context.AudioBooks.FirstOrDefault(a => a.Id == model.Id);
            _context.Entry(model).State = EntityState.Modified;
            return await _context.SaveChangesAsync() >0; 
        }

    }
}
