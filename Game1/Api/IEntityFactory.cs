﻿using Game1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Api
{
    public interface IEntityFactory
    {
        TEntity Create<TEntity>(params object[] args) where TEntity : Entity;
    }
}