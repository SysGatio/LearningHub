namespace SF.PPA.Console;

public class App(IMessages messages)
{
    public void Run(IEnumerable<string> args)
	{
		var lang = "es";

		foreach (var t in args)
        {
            if (!t.StartsWith("lang=", StringComparison.CurrentCultureIgnoreCase))
            {
                continue;
            }

            if (t.Length >= 5)
            {
                lang = t[5..];
            }
            break;
        }

		var message = messages.Greeting(lang);

		System.Console.WriteLine(message);
	}
}
