﻿using AudioBooks.Domain;
using AudioBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBooks.Api.Repositories.Contracts
{
    public interface ILookupDataRepository
    {
        Task<IEnumerable<LookupItemModel>> GetAuthors();
        Task<IEnumerable<LookupItemModel>> GetCategories();
        Task<IEnumerable<LookupItemModel>> GetPublishers();

        Task<int> CreateAuthor(LookupItemModel author);
        Task<bool> UpdateAuthor(LookupItemModel author);
        
        Task<bool> DeleteAuthor(int id);

    }
}
