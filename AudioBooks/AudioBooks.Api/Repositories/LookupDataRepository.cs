using AudioBooks.Api.Repositories.Contracts;
using AudioBooks.Data;
using AudioBooks.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBooks.Api.Repositories
{
    public class LookupDataRepository : ILookupDataRepository
    {
        private AudioBookContext _context;
        private IMapper _mapper;
        private IMemoryCache _cache;
        private MemoryCacheEntryOptions _cacheEntryOptions;//todo: use redis cache

        public LookupDataRepository(AudioBookContext context, IMapper mapper, IMemoryCache cache, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._cache = cache;

            int durationInSeconds = 300;
            int.TryParse(configuration.GetValue<string>("Cache:DurationInSeconds"), out durationInSeconds);
            this._cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(durationInSeconds)); //todo:use a 
        }

        public struct CacheKeys
        {
            public const string Authors = "CacheKey_Author";
            public const string Categories = "CacheKey_Category";
            public const string Publishers = "CacheKey_Publisher";
        }


        public async Task<IEnumerable<LookupItemModel>> GetAuthors()
        {
            IEnumerable<LookupItemModel> authors;
            if (!_cache.TryGetValue(CacheKeys.Authors, out authors))
            {
                authors = _mapper.Map<IEnumerable<Model.LookupItemModel>>(await _context.Authors.ToListAsync());
                _cache.Set(CacheKeys.Authors, authors, _cacheEntryOptions);
            }
            return authors;
        }

        public async Task<IEnumerable<LookupItemModel>> GetCategories()
        {

            IEnumerable<LookupItemModel> categories;
            if (!_cache.TryGetValue(CacheKeys.Categories, out categories))
            {
                categories = _mapper.Map<IEnumerable<Model.LookupItemModel>>
               (await _context.Categories.ToListAsync());
            }
            return categories;
        }

        public async Task<IEnumerable<LookupItemModel>> GetPublishers()
        {
            IEnumerable<LookupItemModel> publishers;
            if (!_cache.TryGetValue(CacheKeys.Publishers, out publishers))
            {
                publishers = _mapper.Map<IEnumerable<Model.LookupItemModel>>
              (await _context.Publishers.ToListAsync());
            }
            return publishers;
        }

        public async Task<int> CreateAuthor(LookupItemModel model)
        {
            // todo: validate 
            var author = _mapper.Map<Domain.Author>(model);
            _context.Authors.Add(author);
            _cache.Remove(CacheKeys.Authors);

            await _context.SaveChangesAsync();
            return author.Id;
        }

        public async Task<bool> UpdateAuthor(LookupItemModel model)
        {
            var author = _mapper.Map<Domain.Author>(model);
            _context.Authors.Update(author);
            _cache.Remove(CacheKeys.Authors);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author =await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            _cache.Remove(CacheKeys.Authors);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
