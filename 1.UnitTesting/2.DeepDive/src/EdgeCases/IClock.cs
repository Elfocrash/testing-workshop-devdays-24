namespace EdgeCases;

internal interface IClock
{
    public DateTime Now { get; }
}

public class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}
