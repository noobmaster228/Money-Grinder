using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SkinShopItemUI : MonoBehaviour
{
    public Image icon;
    public Text skinName;
    public Text priceText;
    public Button buyButton;
    public Button selectButton;
    public Text statusText;

    [HideInInspector] public SkinData skin;

    private Action<SkinData> onBuyClicked;
    private Action<SkinData> onSelectClicked;

    // Этот метод вызывается только один раз для первичной настройки
    public void SetData(
        SkinData skin,
        HashSet<string> purchasedSkins,
        string activeSkinId,
        Action<SkinData> onBuyClicked,
        Action<SkinData> onSelectClicked,
        float balance)
    {
        this.skin = skin;
        this.onBuyClicked = onBuyClicked;
        this.onSelectClicked = onSelectClicked;

        icon.sprite = skin.icon;
        skinName.text = skin.skinName;
        priceText.text = skin.price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => this.onBuyClicked?.Invoke(skin));

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => this.onSelectClicked?.Invoke(skin));

        UpdateState(purchasedSkins, activeSkinId, balance);
    }

    // Этот метод можно вызывать для динамического обновления UI
    public void UpdateState(
        HashSet<string> purchasedSkins,
        string activeSkinId,
        float balance)
    {
        bool bought = purchasedSkins.Contains(skin.skinId);
        bool chosen = activeSkinId == skin.skinId;

        buyButton.gameObject.SetActive(!bought);
        buyButton.interactable = (balance >= skin.price);
        selectButton.gameObject.SetActive(bought && !chosen);
        statusText.gameObject.SetActive(bought && chosen);
        statusText.text = bought && chosen ? "Выбран" : "";
        priceText.gameObject.SetActive(!bought);
    }
}
