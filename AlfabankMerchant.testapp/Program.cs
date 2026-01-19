using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AlfabankMerchant;
using AlfabankMerchant.Actions;
using AlfabankMerchant.Common;
using AlfabankMerchant.Exceptions;
using AlfabankMerchant.Models;
using AlfabankMerchant.Models.Response;
using AlfabankMerchant.RestClient;
using AlfabankMerchant.testapp;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

IConfiguration appConfig = builder.Build();

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();  // Добавляем логирование в консоль
    builder.SetMinimumLevel(LogLevel.Trace);
});
var logger = loggerFactory.CreateLogger<AlfabankMerchantRestClient<AlfabankConfiguration>>();

string env = appConfig["ENVIRONMENT"]!;
AlfabankConfiguration cfg = env switch
{
    "TEST" => new AlfabankConfiguration(appConfig["AB_SERVER"]!)
    {
        Login = appConfig["AB_LOGIN"] ?? appConfig[$"{env}_AB_LOGIN"]!,
        Password = appConfig["AB_PASS"] ?? appConfig[$"{env}_AB_PASS"]!,
        Token = appConfig["AB_TOKEN"] ?? appConfig[$"{env}_AB_TOKEN"]!
    },
    "PROD" => new AlfabankConfiguration("https://ecom.alfabank.ru/api/")
    {
        Login = appConfig["AB_LOGIN"] ?? appConfig[$"{env}_AB_LOGIN"]!,
        Password = appConfig["AB_PASS"] ?? appConfig[$"{env}_AB_PASS"]!,
        Token = appConfig["AB_TOKEN"] ?? appConfig[$"{env}_AB_TOKEN"]!
    },
    _ => throw new NotImplementedException()
};

var client = new AlfabankMerchantRestClient(logger);
Services.Init(logger, loggerFactory, cfg, client);

var newOrder = await CreateOrder(client, cfg, 5000);

var orders = await GetOrders(client, cfg);

//var order = await GetOrderExtended(client, cfg, null, "");

//await RefundOrder(client, cfg, "");

Console.WriteLine("Exit");

static async Task<RegisterOrderResponse> CreateOrder(AlfabankMerchantRestClient client, AlfabankConfiguration config, int amount)
{
    //int orderId = 1;
    var req = new RegisterOrderAction()
    {
        OrderNumber = $"TEST-1-0003",
        Amount = amount,
        // Currency = Currency.RUB,
        ReturnUrl = "https://urg.su/pay",
        Email = "test@example.com",
        Description = "Тестовый заказ созданный через API с DynamicCallback",
        DynamicCallbackUrl = "https://orderman.urg.su/providers/alfabank/callback"
    };

    if (req.ValidateActionParams(out var errors))
    {
        try
        {
            var res = await client.CallActionAsync(req, config);
            Console.WriteLine($"{res.OrderId} - {res.FormUrl}");
            return res;
        }
        catch (AlfabankException abEx)
        {
            Console.WriteLine($"Error: {abEx.Message}");
            throw;
        }
    }

    throw new NotImplementedException();
}

static async Task<LastOrdersForMerchants> GetOrders(AlfabankMerchantRestClient client, AlfabankConfiguration config)
{
    var req = new GetLastOrdersForMerchantsAction()
    {
        Size = 100,
        FromDate = DateTime.Today.AddDays(-30),
        ToDate = DateTime.Today.AddDays(1),
        TransactionStates = TransactionState.ALL,
        SearchByCreatedDate = true
    };

    if (req.ValidateActionParams(out var errors))
    {
        try
        {
            var res = await client.CallActionAsync(req, config);
            return res;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    throw new NotImplementedException();
};

static async Task<Order> GetOrderExtended(AlfabankMerchantRestClient client, AlfabankConfiguration config, string? orderId, string? orderNumber)
{
    var req = new GetOrderStatusExtendedAction()
    {
        OrderId = orderId,
        OrderNumber = orderNumber,
    };

    try
    {
        var res = await client.CallActionAsync(req, config);
        return res;
    }
    catch (Exception ex)
    {
        throw;
    }
}

static async Task RefundOrder(AlfabankMerchantRestClient client, AlfabankConfiguration config, string orderId)
{
    var order = await GetOrderExtended(client, config, orderId, null);

    var req = new RefundAction()
    {
        OrderId = orderId,
        Amount = 5000
    };

    try
    {
        await client.CallActionAsync(req, config);
    }
    catch (Exception ex)
    {
        throw;
    }
}