using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class StockHub : Hub
{
    public async Task SendPrice(string currency, decimal price)
    {
        await Clients.All.SendAsync("ReceivePrice", currency, price);
    }
}
