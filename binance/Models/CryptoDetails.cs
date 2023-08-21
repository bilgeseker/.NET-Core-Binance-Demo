using System;
namespace binance.Models
{
	public class CryptoDetails
	{
		public int Id{ get; set; }
		public string Symbol{ get; set; }
		public decimal Price{ get; set; }
		public int IsFavourite{ get; set; }
        //sspublic decimal PriceChangePercentage { get; set; }
    }


}

