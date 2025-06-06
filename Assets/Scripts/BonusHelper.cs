using System.Collections.Generic;

public static class BonusHelper
{
    static List<string> positiveTags = new List<string> { "+Speed", "+Money", "+Points", "+Multi" }; //список положительных тегов
    static List<string> negativeTags = new List<string> { "-Speed", "-Money", "-Points", "-Multi" }; //список отрицательных тегов
    public static string GetRandomPositiveBonusTag() //установка случайного позитивного тега
    {
        return positiveTags[UnityEngine.Random.Range(0, positiveTags.Count)];
    }
    public static string GetRandomNegativeBonusTag() //установка случайного отрицательного тега
    {
        return negativeTags[UnityEngine.Random.Range(0, negativeTags.Count)];
    }
    public static string GetTextFromTag(string tag) //установка текста в зависимости от тега
    {
        switch (tag)
        {
            case "+Speed": return "+1 к скорости";
            case "+Money": return "+100 к балансy";
            case "+Points": return "+20 очков в секyндy";
            case "+Multi": return "+0.5 к мyльтипликатору";
            case "-Speed": return "-1 к скорости";
            case "-Money": return "-100 к балансy";
            case "-Points": return "-20 очков в секyндy";
            case "-Multi": return "-0.5 к мyльтипликатору";
            default: return "";
        }
    }
}