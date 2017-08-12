﻿using Our.Shield.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;

namespace Our.Shield.Core.Persistance.Business
{
    /// <summary>
    /// The Journal context
    /// </summary>
    public class DomainContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="environmentId"></param>
        /// <returns></returns>
        public IEnumerable<Data.Dto.Domain> List(int? environmentId = null)
        {
            try
            {
                if (environmentId == null)
                {
                    return MapUmbracoDomains(Database.FetchAll<Data.Dto.Domain>());
                }

                return MapUmbracoDomains(Database.Fetch<Data.Dto.Domain>("WHERE " + nameof(Data.Dto.Domain.EnvironmentId) + " = @0", environmentId));
            }
            catch(Exception ex)
            {
                LogHelper.Error(typeof(DomainContext), $"Error listing domains", ex);
            }

            return Enumerable.Empty<Data.Dto.Domain>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Dto.Domain Read(int id)
        {
            try
            {
                return MapUmbracoDomain(Database.SingleOrDefault<Data.Dto.Domain>((object)id));
            }
            catch(Exception ex)
            {
                LogHelper.Error(typeof(DomainContext), $"Error reading domain with id: {id}", ex);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool Write(IDomain domain)
        {
            try
            {
                var dto = new Data.Dto.Domain
                {
                    Id = domain.Id,
                    EnvironmentId = domain.EnvironmentId,
                    Name = domain.Name,
                    UmbracoDomainId = domain.UmbracoDomainId
                };

                if (domain.Id != 0 && Database.Exists<Data.Dto.Domain>(domain.Id))
                {
                    Database.Update(dto);
                }
                else
                {
                    ((Domain)domain).Id = (int)((decimal)Database.Insert(dto));
                }

                return true;
            }
            catch(Exception ex)
            {
                LogHelper.Error(typeof(DomainContext), $"Error writing domain with id: {domain.Id}", ex);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool Delete(int environmentId, IEnumerable<int> domainIds)
        {
            try
            {
                var sql = new Sql();
                sql.Where(nameof(Data.Dto.Domain.EnvironmentId) + " = @0", environmentId);
                
                foreach(var id in domainIds)
                {
                    sql.Where(nameof(Data.Dto.Domain.Id) + " != @0", id);
                }

                Database.Delete<Data.Dto.Domain>(sql);

                return true;
            }
            catch(Exception ex)
            {
                LogHelper.Error(typeof(DomainContext), $"Error deleting domains for environment with id: {environmentId}", ex);
            }

            return false;
        }
    }
}
