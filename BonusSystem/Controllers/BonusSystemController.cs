using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonusSystem.DataProviders;
using BonusSystem.DataProviders.Interfaces;
using BonusSystem.EnterpriseDB.DataTransferObjects;
using ElasticSearch.Interfaces;
using EmailService.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BonusSystem.Controllers
{
    [Route("bonus-system")]
    [ApiController]
    public class BonusSystemController : ControllerBase
    {
        
        IDataProvider Data;
        public BonusSystemController(IDataProvider _Data)
        {
            Data = _Data;
        }
        [EnableCors("BonusSystemPolicy")]
        [Route("get-cards")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetCards(QueryParamDTO request)
        {
            try
            {
                var result = await Data.GetCards(request);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableCors("BonusSystemPolicy")]
        [Route("get-card-by-phone-number")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetCardByNumberPhone(IEnumerable<QueryParamDTO> request)
        {
            try
            {
                var result = await Data.GetCardByNumberPhone(request);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableCors("BonusSystemPolicy")]
        [Route("get-card-by-number")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetCardBalanseByCardNumber(IEnumerable<QueryParamDTO> request)
        {
            try
            {
                var result = await Data.GetCardBalanseByCardNumber(request);
                if(result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableCors("BonusSystemPolicy")]
        [Route("get-card-by-user")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetCardBalanseByUser(IEnumerable<QueryParamDTO> request)
        {
            try
            {
                var result = await Data.GetCardBalanseByUser(request);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableCors("BonusSystemPolicy")]
        [Route("money-from-card")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> MoneyFromBonusCard(QueryParamDTO request)
        {
            try
            {
                await Data.MoneyFromBonusCard(request);
                return Ok("Списание успешно завершено");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableCors("BonusSystemPolicy")]
        [Route("money-to-card")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> MoneyToBonusCard(QueryParamDTO request)
        {
            try
            {
                await Data.MoneyToBonusCard(request);
                return Ok("Зачисление успешно завершено");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableCors("BonusSystemPolicy")]
        [Route("create-card")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateBonusCard()
        {
            try
            {
                await Data.CreateBonusCard();
                return Ok("Новая бонусная карта успешно добавлена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
