namespace LearningHub.Console.YieldExemple;

public static class YieldExemple
{
    public static void RunExample01()
    {
        foreach (var number in GenerateNumbers())
        {
            System.Console.WriteLine(number);
        }
    }

    private static IEnumerable<int> GenerateNumbers()
    {
        for (var i = 0; i < 10; i++)
        {
            if (i % 2 == 0) 
            {
                yield return i; 
            }
        }
    }
}
