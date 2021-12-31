﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using efModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace efModels
{
    public partial class utilarsDBContext
    {
        private utilarsDBContextProcedures _procedures;

        public virtual utilarsDBContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new utilarsDBContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public utilarsDBContextProcedures GetProcedures()
        {
            return Procedures;
        }
    }

    public partial class utilarsDBContextProcedures
    {
        private readonly utilarsDBContext _context;

        public utilarsDBContextProcedures(utilarsDBContext context)
        {
            _context = context;
        }

        public virtual async Task<List<sp_coloringPagesIndexResult>> sp_coloringPagesIndexAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<sp_coloringPagesIndexResult>("EXEC @returnValue = [dbo].[sp_coloringPagesIndex]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<sp_getBlogEntryResult>> sp_getBlogEntryAsync(string friendlyId, int? year, int? month, int? day, string subId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "friendlyId",
                    Size = 50,
                    Value = friendlyId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "year",
                    Value = year ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "month",
                    Value = month ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "day",
                    Value = day ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "subId",
                    Size = 50,
                    Value = subId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<sp_getBlogEntryResult>("EXEC @returnValue = [dbo].[sp_getBlogEntry] @friendlyId, @year, @month, @day, @subId", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<sp_getBlogListResult>> sp_getBlogListAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<sp_getBlogListResult>("EXEC @returnValue = [dbo].[sp_getBlogList]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
