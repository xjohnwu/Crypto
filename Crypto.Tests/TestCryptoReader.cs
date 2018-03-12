using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Crypto.Lib;
using Deedle;
using NUnit.Framework;

namespace Crypto.Tests
{
    [TestFixture]
    public class TestCryptoReader
    {
        [Test]
        public void Get_CoinList()
        {
            var reader = new CryptoReader();
            var coins = reader.GetCoinList().ToList();

            Assert.IsNotEmpty(coins);

            Console.WriteLine("==================ProofType===============");
            Console.WriteLine(string.Join(Environment.NewLine, coins.Select(_=>_.ProofType.Trim()).Distinct()));
            Console.WriteLine("==================Algorithm===============");
            Console.WriteLine(string.Join(Environment.NewLine, coins.Select(_=>_.Algorithm.Trim()).Distinct()));
        }

        [Test]
        public void Get_All_HistoricalData()
        {
            var reader = new CryptoReader();
            var df = Frame.FromColumns(CoinPrices(reader, reader.GetCoinList().Select(c=>c.Symbol), new DateTime(2017, 10, 10)));
            df.SaveCsv(Path.Combine(@"G:\temp\Crypto", DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff")+ ".csv"), true);
        }

        [TestCase("BTC", "ETH", "IOT", "XRP", "EOS")]
        public void Get_HistoricalData(params string[] symbols)
        {
            var reader = new CryptoReader();
            var df = Frame.FromColumns(CoinPrices(reader, symbols, new DateTime(2017, 10, 10)));
            var filePath = Path.Combine(@"G:\temp\Crypto", DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff") + ".csv");
            df.SaveCsv(filePath, true);
            Process.Start(filePath);
        }

        private IEnumerable<KeyValuePair<string, Series<DateTime, decimal>>> CoinPrices(CryptoReader reader, IEnumerable<string> symbols, DateTime fromDate)
        {
            foreach (var symbol in symbols)
            {
                var data = reader.GetHistoricalData(symbol, "USD", fromDate, DateTime.UtcNow.Date).ToArray();
                if (!data.Any())
                    continue;
                yield return KeyValue.Create(symbol, new Series<DateTime, decimal>(data.Select(p => KeyValue.Create(DateHelper.FromEpochTime(p.Time), p.Close))));
            }
        }
    }
}
