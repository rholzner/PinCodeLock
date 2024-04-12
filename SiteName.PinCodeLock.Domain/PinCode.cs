namespace SiteName.PinCodeLock.Domain;

public class PinCode
{
    public PinCode(string kode)
    {
        Kode = kode;
    }

    public string Kode { get; }

    public bool IsValid(string kode)
    {
        return Kode == kode;
    }
}

