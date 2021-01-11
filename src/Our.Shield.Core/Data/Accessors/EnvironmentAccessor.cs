﻿using NPoco;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Core.Logging;
using Umbraco.Core.Scoping;

namespace Our.Shield.Core.Data.Accessors
{
    /// <summary>
    /// Implements <see cref="IEnvironmentAccessor"/>
    /// </summary>
    public class EnvironmentAccessor : Accessor, IEnvironmentAccessor
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EnvironmentAccessor"/> class
        /// </summary>
        /// <param name="scopeProvider"></param>
        /// <param name="logger"></param>
        public EnvironmentAccessor(
            IScopeProvider scopeProvider,
            ILogger logger)
            : base(scopeProvider, logger)
        {
        }

        /// <inheritdoc />
        public async Task<bool> Create(Dtos.IEnvironment environment)
        {
            using (var scope = ScopeProvider.CreateScope())
            {
                try
                {
                    var result = await scope.Database.InsertAsync(environment);

                    return result != null;
                }
                catch(Exception ex)
                {
                    Logger.Error<EnvironmentAccessor>(ex);
                }
                finally
                {
                    scope.Complete();
                }
            }

            return false;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Dtos.IEnvironment>> Read()
        {
            using (var scope = ScopeProvider.CreateScope(autoComplete: true))
            {
                try
                {
                    var result = await scope.Database.FetchAsync<Dtos.Environment>();

                    return result.AsReadOnly();
                }
                catch (Exception ex)
                {
                    Logger.Error<EnvironmentAccessor>(ex);
                }
                finally
                {
                    scope.Complete();
                }
            }

            return new List<Dtos.Environment>();
        }

        /// <inheritdoc />
        public async Task<Dtos.IEnvironment> Read(Guid key)
        {
            using (var scope = ScopeProvider.CreateScope(autoComplete: true))
            {
                try
                {
                    return await scope.Database.SingleAsync<Dtos.Environment>(new Sql().Where("[Key] = '{0}'", key));
                }
                catch(Exception ex)
                {
                    Logger.Error<EnvironmentAccessor>(ex);
                }
                finally
                {
                    scope.Complete();
                }
            }

            return null;
        }

        /// <inheritdoc />
        public Task<bool> Update(Dtos.IEnvironment environment)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid key)
        {
            var env = await Read(key);

            if (env != null)
            {
                return await Delete(env);
            }

            return false;
        }

        /// <inheritdoc />
        public Task<bool> Delete(Dtos.IEnvironment environment)
        {
            throw new NotImplementedException();
        }
    }
}
