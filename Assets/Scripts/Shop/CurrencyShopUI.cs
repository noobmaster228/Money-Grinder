using UnityEngine;
using UnityEngine.UI;

public class CurrencyShopUI : MonoBehaviour
{
    public Text balanceText;
    public Text premBalanceText;

    public Button buyPremiumButton;

    public int addMoneyAmount = 1000;
    public int addMoneyPrice = 10; // сколько премиум валюты тратитс€ за покупку денег

    public int buyPremiumAmount = 100;

    private float balance;
    private float premBalance;

    void Start()
    {
        LoadAndShow();
    }

    void LoadAndShow()
    {
        var save = SaveManager.LoadProgress();
        balance = save.Balance;
        premBalance = save.PremiumBalance;
        UpdateUI();
    }

    void UpdateUI()
    {
        balanceText.text = balance.ToString("#,0").Replace(',', '\'');
        premBalanceText.text = premBalance.ToString("#,0").Replace(',', '\'');
    }

    public void OnAddMoneyClicked()
    {
        if (premBalance < addMoneyPrice)
            return;

        premBalance -= addMoneyPrice;
        balance += addMoneyAmount;

        SaveManager.SaveProgress(SaveManager.LoadProgress().Record, balance, SaveManager.LoadProgress().PurchasedSkins, SaveManager.LoadProgress().ActiveSkinId, premBalance);
        UpdateUI();
    }

    public void OnBuyPremiumClicked()
    {
        // «десь будет вызов API Ч пока заглушка
        StartCoroutine(SimulateApiPurchase());
    }

    System.Collections.IEnumerator SimulateApiPurchase()
    {
        buyPremiumButton.interactable = false;
        // имитаци€ задержки "платежа"
        yield return new WaitForSeconds(2f);

        premBalance += buyPremiumAmount;

        SaveManager.SaveProgress(SaveManager.LoadProgress().Record, balance, SaveManager.LoadProgress().PurchasedSkins, SaveManager.LoadProgress().ActiveSkinId, premBalance);
        UpdateUI();
        buyPremiumButton.interactable = true;
    }
}
