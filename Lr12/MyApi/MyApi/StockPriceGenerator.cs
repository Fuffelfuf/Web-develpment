using Microsoft.AspNetCore.SignalR;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Globalization;

public class StockPriceGenerator
{
    private readonly IHubContext<StockHub> _stockHub;
    private Timer _timer;
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string ApiUrl = "https://api.coincap.io/v2/assets";
    private readonly string[] _targetIds = new[] { "bitcoin", "ethereum", "tether", "binance-coin", "solana", "usd-coin", "xrp" };

    public StockPriceGenerator(IHubContext<StockHub> stockHub)
    {
        _stockHub = stockHub;
    }

    public void Start()
    {
        _timer = new Timer(async _ => await UpdateStockPrices(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
    }

    private async Task UpdateStockPrices()
    {
        try
        {
            Console.WriteLine("Fetching data from API...");
            var response = await _httpClient.GetStringAsync(ApiUrl);
            Console.WriteLine("Data fetched successfully.");

            var jsonDoc = JsonDocument.Parse(response);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("data", out var dataArray))
            {
                foreach (var asset in dataArray.EnumerateArray())
                {
                    var id = asset.GetProperty("id").GetString();
                    if (Array.Exists(_targetIds, targetId => targetId.Equals(id, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (asset.TryGetProperty("priceUsd", out var priceElement))
                        {
                            var priceString = priceElement.GetString()?.Trim();
                            if (!string.IsNullOrEmpty(priceString))
                            {
                                if (decimal.TryParse(priceString, NumberStyles.Any, CultureInfo.InvariantCulture, out var priceUsd))
                                {
                                    Console.WriteLine($"Sending price for {id}: ${priceUsd}");
                                    await _stockHub.Clients.All.SendAsync("ReceivePrice", id, priceUsd);
                                }
                                else
                                {
                                    Console.WriteLine($"Failed to parse price for {id}: {priceString}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Price for {id} is empty or null.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Price for {id} not found.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No 'data' property found in the response.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stock prices: {ex.Message}");
        }
    }
}
