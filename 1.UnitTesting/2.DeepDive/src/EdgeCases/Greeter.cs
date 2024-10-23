namespace EdgeCases;

public class Greeter
{
    public string GenerateGreetText()
    {
        var dateTimeNow = DateTime.Now;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
