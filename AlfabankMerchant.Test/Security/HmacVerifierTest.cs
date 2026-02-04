using AlfabankMerchant.Security.SignatureVerifier;

namespace AlfabankMerchant.Test.Security;

public class HmacVerifierTest
{
    public const string SecretToken = "123";
    public const string message = "\"amount;1500;mdOrder;ed6f3abf-cea0-427e-\nafdf-0ba43ead124f;operation;deposited;orderNumber;89312;status;1;";

    [Fact]
    public void HmacVerifier_CalculateSignature()
    {
        var verifier = new HmacVerifier();
        var signature = verifier.CalculateHash(SecretToken, message);
        Assert.NotNull(signature);
    }
}