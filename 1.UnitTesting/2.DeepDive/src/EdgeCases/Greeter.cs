namespace EdgeCases;

internal class Greeter
{
    private readonly IClock _clock;

    public Greeter(IClock clock)
    {
        _clock = clock;
    }

    public string GenerateGreetText()
    {
        var dateTimeNow = _clock.Now;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
