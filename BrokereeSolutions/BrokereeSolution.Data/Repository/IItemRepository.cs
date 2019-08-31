using System.Collections.Generic;
using BrokereeSolution.Data.Model;
using BrokereeSolution.Data.ViewModel;

namespace BrokereeSolution.Data.Repository
{
    public interface IItemRepository
    {
        int Create(Item item);
        int Delete(int itemId);
        Item GetItem(int itemId);
        List<Item> GetItems();
        string GetSubstringItem(int itemId, int start, int length);
        int Update(ItemView itemView);
    }
}