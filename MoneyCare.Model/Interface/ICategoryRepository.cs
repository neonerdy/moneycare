using System;
using System.Collections.Generic;


namespace MoneyCare.Model
{
    public interface ICategoryRepository
    {
        Category GetById(Guid id);
        Category GetByName(string name);
        List<Category> GetAll();
        List<Category> GetByType(CategoryType categoryType);
        void Save(Category category);
        void Update(Category category);
        void Delete(Guid id);

    }
}
