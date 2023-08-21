using System;
using binance.Models;
using Binance.Net.Clients;

namespace binance
{
	public class Data
	{
		public async Task<List<CryptoDetails>> GetData()
		{
			var client = new BinanceRestClient();  //Binance ağına bağlanmak için client oluştur
            var tickers = await client.SpotApi.ExchangeData.GetPricesAsync(); //Sembol ve fiyatları getir
            List<CryptoDetails> cryptoList = new List<CryptoDetails>();

			
			foreach (var crypto in tickers.Data)
			{
				CryptoDetails detail = new CryptoDetails()
				{
					Id = cryptoList.Count() + 1,
					Symbol = crypto.Symbol,
					Price = crypto.Price
                };

				cryptoList.Add(detail);
			}

			return cryptoList;
        }
	}
}

