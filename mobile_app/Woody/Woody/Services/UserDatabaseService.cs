using Firebase.Database;
using Firebase.Database.Offline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.Services
{

    // UserDatabaseService will use firebase, while ContainerDatabaseService will most likely use CosmosDB
    /// <summary>
    /// Represents a service for managing user data in Firebase.
    /// </summary>
    /// <typeparam name="T">The type of the items stored in the database.</typeparam>
    public class UserDatabaseService<T> : IDataStore<T> where T : class, IHasUKey
    {
        private readonly RealtimeDatabase<T> _realtimeDb;

        private ObservableCollection<T> _items;
        /// <summary>
        /// Gets the observable collection of items managed by the service.
        /// </summary>
        public ObservableCollection<T> Items
        {
            get
            {
                if (_items == null)
                    Task.Run(() => LoadItems()).Wait();
                return _items;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDatabaseService{T}"/> class.
        /// </summary>
        /// <param name="user">The Firebase user.</param>
        /// <param name="path">The path to the Firebase database.</param>
        /// <param name="BaseUrl">The base URL of the Firebase database.</param>
        /// <param name="customKey">An optional custom key for the database.</param>
        public UserDatabaseService(Firebase.Auth.User user, string path, string BaseUrl, string customKey = "")
        {
            FirebaseOptions options = new FirebaseOptions()
            {
                OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s),
                AuthTokenAsyncFactory = async () => await user.GetIdTokenAsync()
            };
            var client = new FirebaseClient(BaseUrl, options);
            _realtimeDb =
                client.Child(path)
                .AsRealtimeDatabase<T>(customKey, "", StreamingOptions.LatestOnly, InitialPullStrategy.MissingOnly, true);
        }

        /// <summary>
        /// Asynchronously adds an item to the database.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was added successfully, otherwise false.</returns>
        public async Task<bool> AddItemsAsync(T item)
        {
            try
            {
                item.Key = _realtimeDb.Post(item); //returns the unique key 

                _realtimeDb.Put(item.Key, item); //Update the entry in the database to maintain the key
                Items.Add(item); //place new item in the observable collection for UI display
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously deletes an item from the database.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was deleted successfully, otherwise false.</returns>
        public Task<bool> DeleteItemsAsync(T item)
        {
            throw new NotImplementedException();
        }

        
        /// <summary>
        /// Asynchronously retrieves items from the database.
        /// </summary>
        /// <param name="forceRefresh">A boolean indicating whether to force a refresh of the database.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning an <see cref="IEnumerable{T}"/> of items.</returns>
        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            
            try
            {
                await _realtimeDb.PullAsync();
            }
            catch (Exception)
            {
                return null;
            }
            
            IEnumerable<T> result = _realtimeDb.Once().Select(x => x.Object);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Asynchronously updates an item in the database.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was updated successfully, otherwise false.</returns>
        public Task<bool> UpdateItemsAsync(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads items from the database asynchronously.
        /// </summary>
        private async Task LoadItems()
        {
            _items = new ObservableCollection<T>(await GetItemsAsync());
        }
    }
}
