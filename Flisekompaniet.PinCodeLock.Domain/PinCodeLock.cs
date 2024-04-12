namespace Flisekompaniet.PinCodeLock.Domain;

public class PinCodeLock
{
    private readonly IEnumerable<PinCode> _pinCode;

    public PinCodeLock(params string[] correctKodes)
    {
        _pinCode = correctKodes.Select(x => new PinCode(x));
    }

    public bool IsUnlocked(string kode)
    {
        return _pinCode.Any(x => x.IsValid(kode));
    }
}

public interface IPinCodeRepository
{
    PinCodeLock GetPinCodeLock();
}
