using UnityEngine;

[CreateAssetMenu(menuName = "MoneyGrinder/Skin Data")]
public class SkinData : ScriptableObject
{
    public string skinId;           // уникальный идентификатор
    public string skinName;         // название для UI
    public int price;               // цена
    public int premPrice;           // цена премиумной валютой
    public Sprite icon;             // иконка для магазина
    public GameObject skinPrefab;   // префаб модели игрока (или null, если только материал)
    public Material skinMaterial;   // материал (если вариант с разными материалами)
}