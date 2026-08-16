namespace FM.Pitchside.Core;

public class FMProcessNotFoundException : Exception
{
    public FMProcessNotFoundException(string message) : base(message)
    {
    }
}