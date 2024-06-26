﻿using Bulky.Models;

namespace Bulky.DataAccess.Repository.IGenericRepository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    void Update(Category category);
}
