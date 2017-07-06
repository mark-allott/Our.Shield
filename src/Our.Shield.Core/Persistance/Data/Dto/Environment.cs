﻿namespace Our.Shield.Core.Persistance.Data.Dto
{
    using System.Collections.Generic;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    /// <summary>
    /// Defines the Configuration table.
    /// </summary>
    [TableName(nameof(Shield) + nameof(Environment))]
    [PrimaryKey("Id", autoIncrement = true)]
    internal class Environment
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = true)]
        [NullSetting(NullSetting = NullSettings.NotNull)]
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.NotNull)]
        [Length(256)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        [Length(256)]
        public string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Ignore]
        public IEnumerable<Domain> Domains { get; set; }
    }
}
