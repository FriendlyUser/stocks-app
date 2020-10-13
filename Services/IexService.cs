using System;
using System.Threading.Tasks;
using IEXSharp;
using IEXSharp.Model.CoreData.CorporateActions.Request;
using IEXSharp.Helper;
using IEXSharp.Model.CoreData.StockPrices.Request;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using IEXSharp.Helper;
using IEXSharp.Model.CoreData.StockPrices.Request;
using IEXSharp.Model.CoreData.StockPrices.Response;
using System.Collections.Generic;
namespace stock_notifications.Services
{

  public class IexService
  {
    private IEXCloudClient client;
    // Constructor that takes no arguments:
    public IexService()
    {
      // read publishableToken and secretToken
      string IEX_SECRET_KEY = Environment.GetEnvironmentVariable("IEX_SECRET_KEY");
      string IEX_PUBLISHABLE_KEY = Environment.GetEnvironmentVariable("IEX_PUBLISHABLE_KEY");
      if (IEX_SECRET_KEY == null) {
        Console.WriteLine("MISSING IEX_SECRET_KEY");
        Environment.Exit(1);
      }
      if (IEX_PUBLISHABLE_KEY == null) {
        Console.WriteLine("MISSING IEX_PUBLISHABLE_KEY");
        Environment.Exit(1);
      }
      client = new IEXCloudClient(
        IEX_PUBLISHABLE_KEY,
        IEX_SECRET_KEY,
        signRequest: false,
        useSandBox: false
      ); 
    }

    public IEXCloudClient getClient() {
      return client;
    }

    public async Task<List<HistoricalPriceResponse>> StockPrices(string stock)
    {
      var response = await client.StockPrices.HistoricalPriceAsync(stock, ChartRange.OneMonth);
      if (response.ErrorMessage != null)
      { 
        var emptyResponse = new List<HistoricalPriceResponse>();
        return emptyResponse;
      }
      else
      {
        return (List<HistoricalPriceResponse>)response.Data;
      }
    }
  }
}