﻿using CleanArchitecture.Application.Common.Interfaces;
using EasyCache.MemCache.Concrete;
using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Caching
{
    public class ApplicationEasyMemCacheManager : EasyCacheMemCacheManager, IApplicationCacheService
    {
        public ApplicationEasyMemCacheManager(IMemcachedClient memcachedClient) : base(memcachedClient)
        {
        }

        public override T Get<T>(string key)
        {
            return base.Get<T>(key);
        }

        public override Task<T> GetAsync<T>(string key)
        {
            return base.GetAsync<T>(key);
        }

        public override void Remove<T>(string key)
        {
            base.Remove<T>(key);
        }

        public override Task RemoveAsync<T>(string key)
        {
            return base.RemoveAsync<T>(key);
        }

        public override void Set<T>(string key, T value, TimeSpan expireTime)
        {
            base.Set(key, value, expireTime);
        }

        public override Task SetAsync<T>(string key, T value, TimeSpan expireTime)
        {
            return base.SetAsync(key, value, expireTime);
        }
    }
}
