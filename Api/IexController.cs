using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stock_notifications.Services;
using IEXSharp.Helper;
using IEXSharp.Model.CoreData.StockPrices.Request;
using IEXSharp.Model.CoreData.StockPrices.Response;
namespace stock_notifications.Controllers
{

  public class IexController : ControllerBase
  {
    private IexService iexService = new IexService();

    // GET: api/iex/stock/appl
    [Route("/api/iex/stock/{stock}")]
    [HttpGet("{stock}", Name="GetStock")]
    public async Task<List<HistoricalPriceResponse>> GetStock(string stock)
    {
        var data = await iexService.StockPrices(stock);
        return data;
    }

    // POST: api/Test
    [HttpPost]
    public string Post([FromBody] string value)
    {
      return "Post";
    }

    // PUT: api/Test/5
    [HttpPut("api/Test2/{id}")]
    public string Put(int id, [FromBody] string value)
    {
      return "Nothing";
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public string Delete(int id)
    {
      return "More of Nothing";
    }
  }
}