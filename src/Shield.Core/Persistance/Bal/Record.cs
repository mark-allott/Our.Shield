﻿using System;
using System.Collections.Generic;

namespace Shield.Core.Persistance.Bal
{
    public abstract class Record
    {
        public abstract string Id { get; }

        protected virtual T Read<T>() where T : IJsonValues, new()
        {
            throw new NotImplementedException();
        }

        protected virtual IEnumerable<T> Read<T>(int page, int itemsPerPage) where T : IJsonValues, new()
        {
            throw new NotImplementedException();
        }

        protected virtual bool Write<T>(T values) where T : IJsonValues
        {
            throw new NotImplementedException();
        }

    }
}
