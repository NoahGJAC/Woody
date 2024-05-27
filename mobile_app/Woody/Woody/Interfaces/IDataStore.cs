using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.Interfaces
{
    /// <summary>
    /// Represents a generic interface for a data store that can add, update, delete, and retrieve items asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the items stored in the data store.</typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Asynchronously adds an item to the data store.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was added successfully, otherwise false.</returns>
        Task<bool> AddItemsAsync(T item);

        /// <summary>
        /// Asynchronously updates an item in the data store.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was updated successfully, otherwise false.</returns>
        Task<bool> UpdateItemsAsync(T item);

        /// <summary>
        /// Asynchronously deletes an item from the data store.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteItemsAsync(T item);

        /// <summary>
        /// Asynchronously retrieves items from the data store.
        /// </summary>
        /// <param name="forceRefresh">A boolean indicating whether to force a refresh of the data store.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning an <see cref="IEnumerable{T}"/> of items.</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        /// <summary>
        /// Gets an observable collection of items managed by the data store.
        /// </summary>
        public ObservableCollection<T> Items { get; }

    }
}
