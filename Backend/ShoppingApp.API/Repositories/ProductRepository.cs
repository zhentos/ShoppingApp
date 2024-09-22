using Dapper;
using Microsoft.Data.Sqlite;
using ShoppingApp.API.Helpers;
using ShoppingApp.API.Models;

namespace ShoppingApp.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private ILogger<ProductRepository> _logger;

        public ProductRepository(ILogger<ProductRepository> logger)
        {
            _connectionString = $"Data Source={DatabaseHelper.GetDatabasePath()}";
            _logger = logger;
        }

        public async Task<int> Add(Product product)
        {
            try
            {
                using (var dbConnection = new SqliteConnection(_connectionString))
                {
                    await dbConnection.OpenAsync();
                    var sql = @"INSERT INTO Products (Title, Description, Price, Thumbnail, CreatedAt, UpdatedAt) 
                        VALUES (@Title, @Description, @Price, @Thumbnail, @CreatedAt, @UpdatedAt);
                        SELECT last_insert_rowid();"; // Get the ID of the newly inserted row

                    // Execute the query and return the new product ID
                    return await dbConnection.ExecuteScalarAsync<int>(sql, new
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Price = product.Price,
                        Thumbnail = product.Thumbnail,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return 0; // object wasn't created
        }

        public async Task<bool> Delete(int id)
        {
            var rowsAffected = 0;
            try
            {
                using (var dbConnection = new SqliteConnection(_connectionString))
                {
                    await dbConnection.OpenAsync();
                    var sql = @"DELETE FROM Products WHERE Id = @Id";

                    // Execute the delete command and return true if any rows were affected
                    rowsAffected = await dbConnection.ExecuteAsync(sql, new { Id = id });

                    return rowsAffected > 0; // Return true if the deletion was successful
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                using (var dbConnection = new SqliteConnection(_connectionString))
                {
                    await dbConnection.OpenAsync();
                    var products = await dbConnection.QueryAsync<Product>("SELECT * FROM Products");
                    return products;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return [];
        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                using (var dbConnection = new SqliteConnection(_connectionString))
                {
                    await dbConnection.OpenAsync();
                    var product = await dbConnection.QuerySingleOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
                    return product ?? new Product();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return new Product();
        }

        public async Task<bool> Update(Product product)
        {
            var rowsAffected = 0;
            try
            {
                using (var dbConnection = new SqliteConnection(_connectionString))
                {
                    await dbConnection.OpenAsync();
                    var sql = @"UPDATE Products 
                        SET Title = @Title, 
                            Description = @Description, 
                            Price = @Price,
                            Thumbnail = @Thumbnail,
                            UpdatedAt = @UpdatedAt 
                        WHERE Id = @Id";

                    // Execute the update command and return true if any rows were affected
                    rowsAffected = await dbConnection.ExecuteAsync(sql, new
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Price = product.Price,
                        Thumbnail = product.Thumbnail,
                        UpdatedAt = DateTime.UtcNow,
                        Id = product.Id
                    });

                    return rowsAffected > 0; // Return true if the update was successful
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return false;
        }
    }
}
