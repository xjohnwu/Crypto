namespace Crypto.Lib.Model
{
    public class Coin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string CoinName { get; set; }
        public string FullName { get; set; }
        public string Algorithm { get; set; }
        public string ProofType { get; set; }
        public decimal? FullyPremined { get; set; }
        public decimal? TotalCoinSupply { get; set; }
        public decimal? PreMinedValue { get; set; }
        public decimal? TotalCoinsFreeFloat { get; set; }
        public int SortOrder { get; set; }
        public bool Sponsored { get; set; }
    }
}
