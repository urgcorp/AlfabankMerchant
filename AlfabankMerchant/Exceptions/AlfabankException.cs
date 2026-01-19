namespace AlfabankMerchant.Exceptions;

public class AlfabankException : Exception
{
    public int ErrorCode { get; set; }

    public AlfabankException(int errorCode, string errorMessage)
        : base(errorMessage)
    {
        this.ErrorCode = errorCode;
    }
}