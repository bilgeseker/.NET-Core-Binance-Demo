using binance.Models;
using Binance.Net.Clients;
using Microsoft.AspNetCore.Mvc;
using Binance.Spot.Models;
using CryptoExchange.Net.CommonObjects;
//using Newtonsoft.Json;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace binance.Controllers;

public class HomeController : Controller
{


    public async Task<ActionResult> Index(bool? showFavorites)
    {
        var veriler = new Data();
        var veriListesi = await veriler.GetData();
        List<CryptoDetails> favorites = new List<CryptoDetails>();
        CryptoContext context = new CryptoContext();

        favorites = context.Cryptolar.ToList();
        foreach (var item in favorites)
        {
            var crypto = veriListesi.FirstOrDefault(x => x.Symbol == item.Symbol.Trim());
            if (crypto != null)
            {
                crypto.IsFavourite = 1;
            }
        }
        if (showFavorites == true)
        {
            var favoriList = veriListesi.Where(x => x.IsFavourite == 1).ToList();
            return View(favoriList);
        }

        return View(veriListesi);
    }


    [HttpPost]
    public ActionResult ManageFavList(string id, string symbol, bool isFavorite)
    {

        using (CryptoContext context = new CryptoContext())
        {
            var cryptoList = context.Cryptolar.ToList();

            CryptoDetails favorite = new CryptoDetails();
            favorite.Id = Int32.Parse(id);
            favorite.Symbol = symbol;

            if (isFavorite && !cryptoList.Any(i => i.Symbol == favorite.Symbol))
            {
                context.Cryptolar.Add(favorite);
            }
            else if (!isFavorite && cryptoList.Any(i => i.Symbol == favorite.Symbol))
            {
                context.ChangeTracker.Clear();
                context.Cryptolar.Remove(favorite);
            }
            context.SaveChanges();
        }
        return Json(true);
    }


    [HttpGet]
    public async Task<ActionResult> GetUpdatedCryptoPrices()
    {
        //Binance.Net API'ye istek gönderip yeni fiyatları al
        var veriler = new Data();
        var veriListesi = await veriler.GetData();

        // Dönen veriyi JSON formatında döndür
        var updatedPrices = veriListesi.Select(crypto => new
        {
            Id = crypto.Id,
            Price = crypto.Price
        });

        return Json(updatedPrices, new System.Text.Json.JsonSerializerOptions());
    }

    [HttpGet]
    public async Task<ActionResult> GetUpdatedCryptoDepth(string symbol)
    {
        var client = new BinanceRestClient();
        var coinFuturesOrderBookData = await client.SpotApi.ExchangeData.GetOrderBookAsync(symbol);

        return Json(coinFuturesOrderBookData, new System.Text.Json.JsonSerializerOptions());

    }

    [HttpGet]
    public async Task<ActionResult> GetUpdatedCryptoPricesAndChanges()
    {
        var client = new BinanceRestClient();
        var veriler = new Data();
        var veriListesi = await veriler.GetData();

        List<CryptoList> cryptoDataList = new List<CryptoList>();

        var orderBookList = await client.SpotApi.ExchangeData.GetTickersAsync();
        
        foreach (var crypto in veriListesi)
        {
            var cryptoData = new CryptoList
            {
                Id = crypto.Id,
                Symbol = crypto.Symbol,
                Price = crypto.Price,
                PriceChangePercentage = orderBookList.Data.FirstOrDefault(p=>p.Symbol == crypto.Symbol).PriceChangePercent                
            };
            cryptoDataList.Add(cryptoData);
        }
        return Json(cryptoDataList, new System.Text.Json.JsonSerializerOptions());
    }


    public async Task<IActionResult> ChartAsync()
    {
        var veriler = new Data();
        var veriListesi = await veriler.GetData();
        var sortedCryptoDataList = veriListesi.OrderBy(crypto => crypto.Symbol).ToList();
        return View(sortedCryptoDataList);
    }
}



