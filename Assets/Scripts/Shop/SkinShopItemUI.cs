using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SkinShopItemUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text skinName;
    [SerializeField] Text priceText;
    [SerializeField] Text premPriceText;
    [SerializeField] Button buyButton;
    [SerializeField] Button premBuyButton;
    [SerializeField] Button selectButton;
    [SerializeField] Text statusText;
    [SerializeField] GameObject Images;

    [HideInInspector] public SkinData skin;

    private Action<SkinData> onBuyClicked;
    private Action<SkinData> onSelectClicked;
    private Action<SkinData> onPremBuyClicked;

    // ���� ����� ���������� ������ ���� ��� ��� ��������� ���������
    public void SetData(
        SkinData skin,
        HashSet<string> purchasedSkins,
        string activeSkinId,
        Action<SkinData> onBuyClicked,
        Action<SkinData> onSelectClicked,
        Action<SkinData> OnPremBuyClicked,
        float balance,
        float premBalance)
    {
        this.skin = skin;
        this.onBuyClicked = onBuyClicked;
        this.onSelectClicked = onSelectClicked;
        this.onPremBuyClicked = OnPremBuyClicked;

        icon.sprite = skin.icon;
        skinName.text = skin.skinName;
        priceText.text = skin.price.ToString();
        premPriceText.text = skin.premPrice.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => this.onBuyClicked?.Invoke(skin));

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => this.onSelectClicked?.Invoke(skin));

        premBuyButton.onClick.RemoveAllListeners();
        premBuyButton.onClick.AddListener(() => this.onPremBuyClicked?.Invoke(skin));
        UpdateState(purchasedSkins, activeSkinId, balance, premBalance);
    }

    // ���� ����� ����� �������� ��� ������������� ���������� UI
    public void UpdateState(
        HashSet<string> purchasedSkins,
        string activeSkinId,
        float balance, float premBalance)
    {
        bool bought = purchasedSkins.Contains(skin.skinId);
        bool chosen = activeSkinId == skin.skinId;

        buyButton.gameObject.SetActive(!bought);
        buyButton.interactable = (balance >= skin.price);
        premBuyButton.gameObject.SetActive(!bought);
        premBuyButton.interactable = (premBalance >= skin.premPrice);
        selectButton.gameObject.SetActive(bought && !chosen);
        statusText.gameObject.SetActive(bought && chosen);
        statusText.text = bought && chosen ? "������" : "";
        priceText.gameObject.SetActive(!bought);
        premPriceText.gameObject.SetActive(!bought);
        Images.SetActive(!bought);
    }
}
