using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.EnumsResult;
using Microsoft.EntityFrameworkCore;
using Services.DapperContext;
using Services.Repositories.Interfaces;

namespace Prueba_Tecnica_Net.Repositories
{
    public class RetailerRepository : IRetailerRepository
    {
        private readonly AppDapperContext AppDbContext;

        public RetailerRepository(AppDapperContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public async Task<Retailer?> GetRetailerById(int id)
        {
            try
            {
                var query = "SELECT * FROM Retailer WHERE Id = @Id";

                using (var connection = AppDbContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id);
                    var retailer = await connection.QuerySingleOrDefaultAsync<Retailer>(query, parameters);
                    return retailer;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los datos");
            }
        }

        public async Task<IEnumerable<Retailer>> GetRetailersAsync(string? code, string? country, string? name)
        {
            try
            {
                var query = "SELECT * FROM Retailer WHERE (@Code is null OR Recode like CONCAT('%', @Code, '%')) AND (@Country is null OR UPPER(Country) = UPPER(@Country))" +
                    "AND (@Name is null OR ReName like CONCAT('%', @Name, '%'))";

                using (var connection = AppDbContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Code", code);
                    parameters.Add("Country", country);
                    parameters.Add("Name", name);
                    var retailers = await connection.QueryAsync<Retailer>(query, parameters);
                    return retailers;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los datos");
            }
        }

        public async Task AddRetailers(IEnumerable<Retailer> retailers)
        {
            try
            {
                var query = "INSERT INTO Retailer (CodingScheme, Country, ReName, ReCode) VALUES (@CodingScheme, @Country, @ReName, @ReCode)";
                using (var connection = AppDbContext.CreateConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {

                        await connection.ExecuteAsync(query, retailers);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving retailers");
            }
        }

        public async Task DeleteAllAsync()
        {
            try
            {
                var query = "DELETE FROM Retailer";
                using (var connection = AppDbContext.CreateConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {

                        await connection.ExecuteAsync(query);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
