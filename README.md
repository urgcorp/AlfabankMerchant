# AlfabankMerchant
REST and Web-Service (WS / WSDL) C# Client and API for Alfabank

Target Framework - net6.0

Support DI

## Code example

    var cfg = new AlfaBankConfiguration("https://tws.egopay.ru/api/ab/")
    {
        Login = "userName",
        Password = "password",
        Token = "merchant_token"
    };
    var client = new AlfabankRestClient(cfg);
    
    var req = new RegisterOrderAction()
    {
        OrderNumber = "0-0001",
        Amount = 100,
        ReturnUrl = "https://example.com/pay/0-0001",
        Email = "client@example.com",
        Description = "API Test Order",
        DynamicCallbackUrl = "https://example.com/pay_callback"
    };
        
    if (req.ValidateActionParams(out var errors))
    {
        try
        {
            var res = await client.CallActionAsync(req);           
            Console.WriteLine($"{res.OrderId} - {res.FormUrl}");
            return res;
        }
        catch (AlfabankException abEx)
        {
            Console.WriteLine($"Error: {abEx.Message}");
            throw;
        }
    }

[link-author]: https://github.com/CaCTuCaTu4ECKuu
