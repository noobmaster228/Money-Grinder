using System.Collections.Generic;

public static class BonusHelper
{
    static List<string> positiveTags = new List<string> { "+Speed", "+Money", "+Points", "+Multi" };
    static List<string> negativeTags = new List<string> { "-Speed", "-Money", "-Points", "-Multi" };

    public static string GetRandomPositiveBonusTag()
    {
        return positiveTags[UnityEngine.Random.Range(0, positiveTags.Count)];
    }

    public static string GetRandomNegativeBonusTag()
    {
        return negativeTags[UnityEngine.Random.Range(0, negativeTags.Count)];
    }

    public static string GetTextFromTag(string tag)
    {
        switch (tag)
        {
            case "+Speed": return "+1 Speed";
            case "+Money": return "+100 Money";
            case "+Points": return "+20 Points per second";
            case "+Multi": return "+0.5 Money Multiplier";
            case "-Speed": return "-1 Speed";
            case "-Money": return "-100 Money";
            case "-Points": return "-20 Points per second";
            case "-Multi": return "-0.5 Money Multiplier";
            default: return "";
        }
    }
}

