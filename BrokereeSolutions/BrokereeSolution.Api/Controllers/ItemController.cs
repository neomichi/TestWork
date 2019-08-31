using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokereeSolution.Data.Model;
using BrokereeSolution.Data.Repository;
using BrokereeSolution.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrokereeSolution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : ControllerBase
    {
        IItemRepository _itemRepository;
        /// <summary>
        ///  конструктор
        /// </summary>
        /// <param name="itemRepository"></param>
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// Получить список всех items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Item>> GetAll()
        {
            return _itemRepository.GetItems();
        }

        /// <summary>
        /// Получить по id 
        /// </summary>
        /// <param name="id">id item</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Item> GetById(int id)
        {
            var item = _itemRepository.GetItem(id);
            if (item != null) return Ok(item);
            return BadRequest("not found");
        }

        /// <summary>
        /// Получить подстроку ресурса по индексу начала и длине подстроки
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="start">индекс начала</param>
        /// <param name="length">длина подстроки</param>
        /// <returns></returns>
        [HttpGet("{id}/{start}/{length}", Name = "GetByMany")]
        public ActionResult<string> GetByMany(int id, int start, int length)
        {
            var str = _itemRepository.GetSubstringItem(id, start, length);
            if (string.IsNullOrEmpty(str))
                return BadRequest("not found");

            return Ok(_itemRepository.GetSubstringItem(id, start, length));
        }


        /// <summary>
        ///  создать item 
        /// </summary>
        /// <param name="value">{id,text}</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Item> Post([FromBody] Item value)
        {
            var result = _itemRepository.Create(value);
            if (result == 1) return Ok(value);
            return BadRequest("error");
        }

        /// <remarks>
        /// Получить подстроку ресурса по индексу начала и длине подстроки<br/>
        /// - Вставка подстроки<br/>
        ///  - в начало<br/>
        ///  - в конец<br/>
        ///  - в любое место по индексу<br/>
        ///     - Удаление подстроки по индексу и длине<br/>
        ///     - Замена подстроки на другую      
        ///
        /// описание ActionType:
        /// 10: Перезаписать ресурс
        /// 20: Вставка подстроки - в начало       
        /// 30: Вставка подстроки - в конец        
        /// 40: Вставка подстроки - в любое место по индексу             
        /// 50: Удаление подстроки по индексу и длине           
        /// 60: Замена подстроки на другую     
        /// </remarks>
        ///   <summary>  </summary>
        /// <param name="itemView"> 
        /// <param name="Id">идентификатор</param>
        /// <param name="Text">значение</param>
        /// <param name="Index">индекс</param>
        /// <param name="Length">длина под строки(для метода DeleteSub)</param>
        /// <param name="ActionType">тип действия</param> 
        /// 
        /// </param>
        /// <returns></returns>
        [HttpPut]       
        public ActionResult<Item> Put([FromBody] ItemView itemView)
        {
            var result = _itemRepository.Update(itemView);
            if (result == 1) return Ok("");
            return BadRequest("error");
        }

        /// <summary>
        /// Удалить item
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            var result = _itemRepository.Delete(id);
            if (result == 1) return Ok();
            return BadRequest("error");
        }
    }
}
