﻿using System;
using System.Collections.Generic;

namespace ApiShort.DAL.Repository
{
    public interface IRepository<T> : IDisposable
    where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
