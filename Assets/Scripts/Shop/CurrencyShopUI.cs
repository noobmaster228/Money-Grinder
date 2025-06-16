using UnityEngine;
using UnityEngine.UI;

public class CurrencyShopUI : MonoBehaviour
{
    public Text balanceText;
    public Text premBalanceText;

    public Button buyPremiumButton;

    public int addMoneyAmount = 1000;
    public int addMoneyPrice = 10; // ������� ������� ������ �������� �� ������� �����

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
        // ����� ����� ����� API � ���� ��������
        StartCoroutine(SimulateApiPurchase());
    }

    System.Collections.IEnumerator SimulateApiPurchase()
    {
        buyPremiumButton.interactable = false;
        // �������� �������� "�������"
        yield return new WaitForSeconds(2f);

        premBalance += buyPremiumAmount;

        SaveManager.SaveProgress(SaveManager.LoadProgress().Record, balance, SaveManager.LoadProgress().PurchasedSkins, SaveManager.LoadProgress().ActiveSkinId, premBalance);
        UpdateUI();
        buyPremiumButton.interactable = true;
    }
}
