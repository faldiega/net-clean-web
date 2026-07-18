using Dapper;
using Microsoft.Data.SqlClient;
using NetApp.Application.DTOs;
using NetApp.Application.Interfaces;
using NetApp.Domain.Entities;
using NetApp.Domain.Interfaces;
using NetApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Infrastructure.Repositories
{
    public class CategoryQueryRepository : ICategoryQueryService
    {
        private readonly DapperContext _dapperContext;

        public CategoryQueryRepository(DapperContext context)
        {
            _dapperContext = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            string query = @"
                SELECT
	                c.CategoryID,
	                c.Name, 
	                COUNT(p.CategoryID) AS ProductCount,
	                c.CreatedDate
                FROM Category c
                LEFT JOIN Product p ON p.CategoryID = c.CategoryID
                GROUP BY c.CategoryID, c.Name, c.CreatedDate
            ";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<CategoryDto>(query);
        }
    }
}
