using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); ;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection=new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                (
                    "SELECT * FROM Coupon WHERE ProductName=@ProductName",
                    new { ProductName=productName }
                );
            if (coupon == null)
                return new Coupon() { 
                    ProductName="No Discount",Amount=0,Description="No Discount Desc"
                };

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var effected = await connection.ExecuteAsync(
                    @"INSERT INTO Coupon(ProductName,Amount,Description)
                    VALUES(@ProductName,@Amount,@Description);",
                    new {ProductName=coupon.ProductName,Amount=coupon.Amount,Description=coupon.Description }
                );
            if (effected == 0) return false;

            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var effected = await connection.ExecuteAsync(
                    @"UPDATE Coupon SET 
                        ProductName=@ProductName,
                        Amount=@Amount,
                        Description=@Description
                    WHERE Id=@Id",
                    new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description,Id=coupon.Id }
                );
            if (effected == 0) return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var effected = await connection.ExecuteAsync(
                    @"DELETE FROM Coupon
                        WHERE ProductName=@ProductName",
                    new { ProductName = productName }
                );
            if (effected == 0) return false;

            return true;
        }

        

        
    }
}
