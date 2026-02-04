using AlfabankMerchant.Security.SignatureVerifier;

namespace AlfabankMerchant.Test.Security;

public class HmacVerifierTest
{
    public const string TestSecretToken = "12345678";
    public const string TestCallbackData = "\"amount;1500;mdOrder;ed6f3abf-cea0-427e-\nafdf-0ba43ead124f;operation;deposited;orderNumber;89312;status;1;";
    public const string TestSignature = "38D69820F981235DE4858A89A9EF3E5999E28EEF9E472AADBA77413BC94C935C";

    [Fact]
    public void HmacVerifier_CalculateSignature()
    {
        var verifier = new HmacVerifier();
        var signature = verifier.CalculateHash(TestSecretToken, TestCallbackData);
        Assert.NotNull(signature);
        Assert.Equal(TestSignature, signature);
    }
}