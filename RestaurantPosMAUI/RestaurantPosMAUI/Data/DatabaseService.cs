using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPosMAUI.Data
{
    public class DatabaseService : IAsyncDisposable
    {
        private readonly SQLiteAsyncConnection _connection;
        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "restpos.db3");
            _connection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }

        public async Task InitializeDatabaseAsync()
        {
            await _connection.CreateTableAsync<MenuCategory>();
            await _connection.CreateTableAsync<MenuItem>();
            await _connection.CreateTableAsync<MenuItemCategoryMapping>();
            await _connection.CreateTableAsync<Order>();
            await _connection.CreateTableAsync<OrderItem>();

            await SeedDataAsync();
        }

        private async Task SeedDataAsync()
        {
            var firstCategory = await _connection.Table<MenuCategory>().FirstOrDefaultAsync();
            if (firstCategory is not null)
            {
                return;
            }

            var categories = SeedData.GetMenuCategories();
            var menuItems = SeedData.GetMenuItems();
            var mappings = SeedData.GetMenuItemCategoryMappings();
            await _connection.InsertAllAsync(categories);
            await _connection.InsertAllAsync(menuItems);
            await _connection.InsertAllAsync(mappings);
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection is not null)
            {
                await _connection.CloseAsync();
            }
        }
    }
}
