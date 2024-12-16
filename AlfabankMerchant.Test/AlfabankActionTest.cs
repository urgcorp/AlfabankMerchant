using AlfabankMerchant.Test.Common;

namespace AlfabankMerchant.Test;

public class AlfabankActionTest
{
    public readonly AlfabankActionBlank Act = new("login", "password", "token")
    {
        StringAny = "test string"
    };

    [Fact]
    public void AlfabankAction_GetActionParams()
    {
        var param = Act.GetActionParams();
        Assert.Equal(4, param.Count);
        Assert.Contains(param, x => x.Key == "userName");
        Assert.Contains(param, x => x.Key == "password");
        Assert.Contains(param, x => x.Key == "token");
        Assert.Contains(param, x => x.Key == "asn512");
    }
    
    [Fact]
    public void AlfabankAction_FindActionUrl_NullThrowsNotOverriden()
    {
        Assert.Throws<ArgumentException>(() => Act.FindActionUrl(null!));
    }

    [Fact]
    public void AlfabankAction_FindActionUrl_NullWhenOverriden()
    {
        var overrideUrl = "https://override.com";
        var act = new AlfabankActionBlank()
        {
            ActionUrl = overrideUrl
        };
        var url = act.FindActionUrl(null!);
        Assert.Equal(overrideUrl, url);
    }
    
    [Fact]
    public void AlfabankAction_FindActionUrl_RestNotOverriden()
    {
        var url = Act.FindActionUrl("REST");
        Assert.Equal("https://example.com", url);
    }

    [Fact]
    public void AlfabankAction_FindActionUrl_RestWhenOverriden()
    {
        var overrideUrl = "https://override.com";
        var act = new AlfabankActionBlank()
        {
            ActionUrl = overrideUrl
        };
        var url = act.FindActionUrl("REST");
        Assert.Equal(overrideUrl, url);
    }
}