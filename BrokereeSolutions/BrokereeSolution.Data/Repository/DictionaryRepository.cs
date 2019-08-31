using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrokereeSolution.Data.Model;
using BrokereeSolution.Data.ViewModel;

namespace BrokereeSolution.Data.Repository
{
    public class DictionaryRepository : IItemRepository
    {
        private readonly ConcurrentDictionary<int, string> _items;
       
        public DictionaryRepository()
        {
            _items = code.items;       
        }

        public int Create(Item item)
        {
            return _items.TryAdd(item.Id, item.Text) ? 1 : 0;
        }

        public int Delete(int itemId)
        {            
            string str = "";
            if (_items.TryRemove(itemId,out str))
            {
                return 1;
            }
            return 0;
        }

        public Item GetItem(int itemId)
        {
            if (_items.TryGetValue(itemId, out string str))
            {
                return new Item() { Id = itemId, Text = str };
            }
            return null;
        }

        public List<Item> GetItems()
        {
            var list = new List<Item>();
            var keys = _items.Keys.ToList();
            foreach(var key in keys)
            {
                if (_items.TryGetValue(key, out string str))
                {
                    list.Add(new Item() { Id = key, Text = str });
                }
            }
            return list;
        }

        public string GetSubstringItem(int itemId, int start, int length)
        {         
            if (_items.TryGetValue(itemId, out string str))
            {
                return str.Substring(start, length);
            }
            return null;

        }

        public int Update(ItemView itemView)
        {

            var oldText = "";
            var item = GetItem(itemView.Id);
            if (item != null)
            {

                oldText = item.Text;
            }
            else
            {
                return 0;
            }
            switch (itemView.ActionType)
            {
                case ActionType.DefaultUpdate:
                    {                        
                        return Update(item.Id, oldText, itemView.Text);
                    }
                case ActionType.InsertSubBegin:
                    {
                        return Update(item.Id, oldText, oldText.Insert(0, itemView.Text));
                    }
                case ActionType.InsertSubEnd:
                    {
                        return Update(item.Id, oldText, oldText.Insert(oldText.Length, itemView.Text));
                    }
                case ActionType.InsertSubIndex:
                    {                        
                        return Update(item.Id, oldText, oldText.Insert(itemView.Index, itemView.Text));                        
                    }
                case ActionType.DeleteSub:
                    {
                        return Update(item.Id, oldText, oldText.Remove(itemView.Index, itemView.Length));
                    }
                case ActionType.ReplaceSub:
                    {
                       var old = oldText.Remove(itemView.Index, itemView.Text.Length).Insert(itemView.Index, itemView.Text);
                       return Update(item.Id, oldText, old);
                    }
            }
            return 1;
        }


        private int Update(int itemId,string oldValue,string newValue)
        {
            return _items.TryUpdate(itemId,newValue, oldValue) ? 1 : 0;
        }

        //private Item GetItemById(int itemId)
        //{
        //    if (IsContainsId(itemId))
        //    {
        //        var id = _items.Keys.Single(x => x == itemId);

        //        return new Item() { Id = id, Text = _items[id] };
        //    }
        //    return new Item();  
        //}

        //private bool IsContainsId(int Id)
        //{
        //    return _items.Keys.Contains(Id);
        //}
    }


}
