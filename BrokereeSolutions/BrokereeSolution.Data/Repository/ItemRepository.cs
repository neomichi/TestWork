using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BrokereeSolution.Data.Model;
using BrokereeSolution.Data.ViewModel;
using Dapper;

namespace BrokereeSolution.Data.Repository
{
    public class ItemRepository : IItemRepository
    {
        string connectionString = "";
        public ItemRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        /// <summary>
        /// получить все
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems()
        {
            using (IDbConnection db = new Npgsql.NpgsqlConnection(connectionString))
            {
                return db.Query<Item>("SELECT * FROM \"public\".\"Items\"").ToList();
            }
        }
        /// <summary>
        /// получить по itemId
        /// </summary>
        /// <param name="itemId">id</param>
        /// <returns></returns>
        public Item GetItem(int itemId)
        {
            using (IDbConnection db = new Npgsql.NpgsqlConnection(connectionString))
            {
                return db.Query<Item>("SELECT * FROM \"public\".\"Items\" Where \"Id\"=@itemId", new { itemId })
                    .FirstOrDefault();
            }
        }
        /// <summary>
        /// получить подсроку
        /// </summary>
        /// <param name="itemId">id</param>
        /// <param name="start">начало</param>
        /// <param name="length">длина</param>
        /// <returns></returns>
        public string GetSubstringItem(int itemId, int start, int length)
        {
            using (IDbConnection db = new Npgsql.NpgsqlConnection(connectionString))
            {
                return db.Query<string>("SELECT substr(\"Text\", @start, @length) FROM \"public\".\"Items\" Where \"Id\"=@itemId", new { itemId, start, length }).FirstOrDefault();

            }
        }
        /// <summary>
        /// создать
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(Item item)
        {
            var result = 0;

            using (IDbConnection db = new Npgsql.NpgsqlConnection(connectionString))
            {
                try
                {

                    var sqlQuery = "INSERT INTO public.\"Items\"(\"Id\", \"Text\") VALUES (@id,@text)";
                    result = db.Execute(sqlQuery, new { item.Id, item.Text });

                }
                catch
                {


                }
                return result;
            }

        }

        ///// <summary>
        ///// обновить ресурс 
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //public int Update(Item item)
        //{
        //    using (IDbConnection db = new Npgsql.NpgsqlConnection(connectionString))
        //    {
        //        var sqlQuery = "UPDATE public.\"Items\" SET \"Text\"=@text WHERE \"Id\"=@id";
        //        return db.Execute(sqlQuery, new { item.Text, item.Id, });
        //    }
        //}
        /// <summary>
        /// Удалить ресурс по id
        /// </summary>
        /// <param name="itemId">id</param>
        /// <returns></returns>
        public int Delete(int itemId)
        {
            using (IDbConnection db = new Npgsql.NpgsqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM public.\"Items\" WHERE \"Id\"=@itemId";
                return db.Execute(sqlQuery, new { itemId });
            }
        }
        /// <summary>
        /// - Вставка подстроки
        /// - в начало
        ///  -в конец
        /// - в любое место по индексу
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="index"></param>
        /// <param name="text">подстрока</param>
        /// <param name="TaskType">тип запроса</param>
        /// <param name="length">длина </param>
        /// <returns></returns>
        public int Update(ItemView itemView)
        {
            var result = 0;

            using (IDbConnection db = new Npgsql.NpgsqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    var newText = "";
                    var oldText = GetItem(itemView.Id).Text;
                    try
                    {   switch (itemView.ActionType)
                        {
                            case ActionType.DefaultUpdate:
                                {
                                    newText = itemView.Text;
                                    break;
                                }
                            case ActionType.InsertSubBegin:
                            case ActionType.InsertSubEnd:
                            case ActionType.InsertSubIndex:
                                {
                                    newText = oldText.Insert(itemView.Index, itemView.Text);
                                    break;
                                }
                            case ActionType.DeleteSub:
                                {
                                    newText = oldText.Remove(itemView.Index, itemView.Length);
                                    break;
                                }
                            case ActionType.ReplaceSub:
                                {
                                    newText = oldText.Remove(itemView.Index, itemView.Text.Length).Insert(itemView.Index, itemView.Text);
                                    break;
                                }
                        }
                   
                        var sql = "UPDATE public.\"Items\" SET \"Text\"=@text WHERE \"Id\"=@itemId";
                        result = db.Execute(sql, new { itemView.Id, newText }, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }             
            }
            return result;
        }      
            
    }


}

