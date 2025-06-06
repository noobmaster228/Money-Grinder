using System.Collections.Generic;

public static class BonusHelper
{
    static List<string> positiveTags = new List<string> { "+Speed", "+Money", "+Points", "+Multi" }; //������ ������������� �����
    static List<string> negativeTags = new List<string> { "-Speed", "-Money", "-Points", "-Multi" }; //������ ������������� �����
    public static string GetRandomPositiveBonusTag() //��������� ���������� ����������� ����
    {
        return positiveTags[UnityEngine.Random.Range(0, positiveTags.Count)];
    }
    public static string GetRandomNegativeBonusTag() //��������� ���������� �������������� ����
    {
        return negativeTags[UnityEngine.Random.Range(0, negativeTags.Count)];
    }
    public static string GetTextFromTag(string tag) //��������� ������ � ����������� �� ����
    {
        switch (tag)
        {
            case "+Speed": return "+1 � ��������";
            case "+Money": return "+100 � ������y";
            case "+Points": return "+20 ����� � ���y��y";
            case "+Multi": return "+0.5 � �y�������������";
            case "-Speed": return "-1 � ��������";
            case "-Money": return "-100 � ������y";
            case "-Points": return "-20 ����� � ���y��y";
            case "-Multi": return "-0.5 � �y�������������";
            default: return "";
        }
    }
}