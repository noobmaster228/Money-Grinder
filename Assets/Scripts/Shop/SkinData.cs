using UnityEngine;

[CreateAssetMenu(menuName = "MoneyGrinder/Skin Data")]
public class SkinData : ScriptableObject
{
    public string skinId;           // ���������� �������������
    public string skinName;         // �������� ��� UI
    public int price;               // ����
    public Sprite icon;             // ������ ��� ��������
    public GameObject skinPrefab;   // ������ ������ ������ (��� null, ���� ������ ��������)
    public Material skinMaterial;   // �������� (���� ������� � ������� �����������)
}