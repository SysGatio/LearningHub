namespace SF.PPA.Console.Dominio.LogicaNegocio;

public class Messages(ILogger<Messages> log) : IMessages
{
    private static readonly JsonSerializerOptions SOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public string Greeting(string language)
	{
		var output = LookUpCustomText("Greeting", language);

		return output;
	}

    private string LookUpCustomText(string key, string language)
    {
        try
        {
            var messageSets = JsonSerializer.Deserialize<List<CustomText>>(
                File.ReadAllText("CustomText.json"), SOptions);

            var messages = messageSets?.FirstOrDefault(x => x.Language == language);

            return messages is null
                ? throw new NullReferenceException("The specified language was not found in the json file.")
                : messages.Translations[key];
        }
        catch (Exception ex)
        {
            log.LogError(message: "Error looking up the custom text {ex}", ex);
            throw;
        }
    }

}
