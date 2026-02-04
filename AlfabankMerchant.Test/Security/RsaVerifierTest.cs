using AlfabankMerchant.Models;
using AlfabankMerchant.Security.SignatureVerifier;

namespace AlfabankMerchant.Test.Security;

public class RsaVerifierTest
{
    public static readonly string TestPublicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwtuGKbQ4WmfdV1gjWWys
                                5jyHKTWXnxX3zVa5/Cx5aKwJpOsjrXnHh6l8bOPQ6Sgj3iSeKJ9plZ3i7rPjkfmw
                                qUOJ1eLU5NvGkVjOgyi11aUKgEKwS5Iq5HZvXmPLzu+U22EUCTQwjBqnE/Wf0hnI
                                wYABDgc0fJeJJAHYHMBcJXTuxF8DmDf4DpbLrQ2bpGaCPKcX+04POS4zVLVCHF6N
                                6gYtM7U2QXYcTMTGsAvmIqSj1vddGwvNGeeUVoPbo6enMBbvZgjN5p6j3ItTziMb
                                Vba3m/u7bU1dOG2/79UpGAGR10qEFHiOqS6WpO7CuIR2tL9EznXRc7D9JZKwGfoY
                                /QIDAQAB";

    public static readonly AlfabankOperationCallback TestCallback = new AlfabankOperationCallback()
    {
        OrderId = "12b59da8-f68f-7c8d-12b5-9da8000826ea",
        Operation = CallbackOperationType.Deposited,
        Status = true,
        Amount = 35000099,
        Checksum = "9524FD765FB1BABFB1F42E4BC6EF5A4B07BAA3F9C809098ACBB462618A9327539F975FEDB4CF6EC1556FF88BA74774342AF4F5B51BA63903BE9647C670EBD962467282955BD1D57B16935C956864526810870CD32967845EBABE1C6565C03F94FF66907CEDB54669A1C74AC1AD6E39B67FA7EF6D305A007A474F03B80FD6C965656BEAA74E09BB1189F4B32E622C903DC52843C454B7ACF76D6F76324C27767DE2FF6E7217716C19C530CA7551DB58268CC815638C30F3BCA3270E1FD44F63C14974B108E65C20638ECE2F2D752F32742FFC5077415102706FA5235D310D4948A780B08D1B75C8983F22F211DFCBF14435F262ADDA6A97BFEB6D332C3D51010B"
    };

    [Fact]
    public void RsaVerifier_Verify()
    {
        var data = TestCallback.ToParametersString();
        var validator = new RsaVerifier();
        var res = validator.Verify(data, TestCallback.Checksum!, TestPublicKey);
        Assert.True(res);
    }
}