using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;
using System;
using Dapper;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount);",
                new { ProductName = coupon.ProductName, Description=coupon.Description, Amount=coupon.Amount });
            
            return affected > 0? true:false;
        }
        public async Task<bool> DeleteDiscountByProductName(string productName)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync
                ("Delete from Coupon where ProductName=@ProductName;",
                new { ProductName = productName });

            return affected > 0 ? true : false;
        }

        public async Task<bool> DeleteDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync
                ("Delete from Coupon where ProductName=@ProductName;",
                new { ProductName = coupon.ProductName });

            return affected > 0 ? true : false;
        }


        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM public.coupon where productname=@ProductName;", new { ProductName= productName });
        
            if (coupon == null)
            {
                return new Coupon() { ProductName="No Discount"};
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync
                ("update Coupon set productname=@ProductName, Description=@Description, Amount=@Amount where productname=@ProductName;",
                new { ProductName = coupon.ProductName , Description =coupon.Description, Amount =coupon.Amount});

            return affected > 0 ? true : false;
        }
    }
}
