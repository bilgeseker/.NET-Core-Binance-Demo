using System;
namespace binance.Models
{
    public class CryptoList
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal PriceChangePercentage { get; set; }
    }
}

