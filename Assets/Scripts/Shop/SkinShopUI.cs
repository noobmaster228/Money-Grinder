using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopUI : MonoBehaviour
{
    public SkinCatalog skinCatalog;
    public Transform contentRoot;
    public GameObject skinShopItemPrefab;
    public Text balanceText;

    private HashSet<string> purchasedSkins = new HashSet<string>();
    private string activeSkinId;
    private float balance;

    // Храним ссылки на UI элементов магазина для быстрого обновления
    private List<SkinShopItemUI> spawnedItems = new List<SkinShopItemUI>();

    void Start()
    {
        var save = SaveManager.LoadProgress();
        purchasedSkins = new HashSet<string>(save.PurchasedSkins ?? new string[0]);
        activeSkinId = save.ActiveSkinId;
        balance = save.Balance;
        UpdateBalanceUI(); // отображаем баланс на старте
        DrawShop();
    }

    public void DrawShop()
    {
        // Очищаем старое
        foreach (Transform child in contentRoot)
            Destroy(child.gameObject);
        spawnedItems.Clear();

        // Создаём новые элементы
        foreach (var skin in skinCatalog.skins)
        {
            var go = Instantiate(skinShopItemPrefab, contentRoot);
            var item = go.GetComponent<SkinShopItemUI>();
            item.SetData(
                skin,
                purchasedSkins,
                activeSkinId,
                OnBuyClicked,
                OnSelectClicked,
                balance
            );
            spawnedItems.Add(item);
        }
    }
    public void ResetShop()
    {
        // ОБНОВЛЯЕМ ПЕРЕМЕННЫЕ из save!
        var save = SaveManager.LoadProgress();
        purchasedSkins = new HashSet<string>(save.PurchasedSkins ?? new string[0]);
        activeSkinId = save.ActiveSkinId;
        balance = save.Balance;

        // Очищаем старое
        foreach (Transform child in contentRoot)
            Destroy(child.gameObject);
        spawnedItems.Clear();

        // Создаём новые элементы
        foreach (var skin in skinCatalog.skins)
        {
            var go = Instantiate(skinShopItemPrefab, contentRoot);
            var item = go.GetComponent<SkinShopItemUI>();
            item.SetData(
                skin,
                purchasedSkins,
                activeSkinId,
                OnBuyClicked,
                OnSelectClicked,
                balance
            );
            spawnedItems.Add(item);
        }
    }


    void OnBuyClicked(SkinData skin)
    {
        if (balance < skin.price || purchasedSkins.Contains(skin.skinId))
            return;

        balance -= skin.price;
        purchasedSkins.Add(skin.skinId);

        SaveManager.SaveProgressOnlyStats(SaveManager.LoadProgress().Record, balance);
        SaveManager.SaveProgressOnlySkins(new List<string>(purchasedSkins).ToArray(), activeSkinId);
        UpdateBalanceUI(); // обновляем текст баланса
        // Обновляем только текущий элемент магазина
        var item = spawnedItems.Find(i => i.skin.skinId == skin.skinId);
        if (item != null)
            item.UpdateState(purchasedSkins, activeSkinId, balance);
    }

    void OnSelectClicked(SkinData skin)
    {
        if (!purchasedSkins.Contains(skin.skinId))
            return;

        activeSkinId = skin.skinId;
        SaveManager.SaveProgressOnlySkins(new List<string>(purchasedSkins).ToArray(), activeSkinId);

        // Обновляем состояние у всех элементов (сброс у предыдущего, включение у нового)
        foreach (var item in spawnedItems)
            item.UpdateState(purchasedSkins, activeSkinId, balance);
    }
    void UpdateBalanceUI()
    {
        if (balanceText != null)
            balanceText.text = balance.ToString("#,0").Replace(',', '\''); // формат с апострофами, если надо
    }
}
