using Firebase.Database;
using Firebase.Database.Offline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Services
{

    // UserDatabaseService will use firebase, while ContainerDatabaseService will most likely use CosmosDB
    public class UserDatabaseService<T> : IDataStore<T> where T : class, IHasUKey
    {
        private readonly RealtimeDatabase<T> _realtimeDb;

        private ObservableCollection<T> _items;
        public ObservableCollection<T> Items
        {
            get
            {
                if (_items == null)
                    Task.Run(() => LoadItems()).Wait();
                return _items;
            }
        }
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

        public async Task<bool> AddItemsAsync(T item)
        {
            try
            {
                item.Key = _realtimeDb.Post(item); //returns the unique key 

                _realtimeDb.Put(item.Key, item); //Update the entry in the database to maintain the key
                Items.Add(item); //place new item in the observable collection for UI display
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteItemsAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemsAsync(T item)
        {
            throw new NotImplementedException();
        }
        private async Task LoadItems()
        {
            _items = new ObservableCollection<T>(await GetItemsAsync());
        }
    }
}
