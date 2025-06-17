using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyShopUI : MonoBehaviour
{
    [SerializeField] Text balanceText;
    [SerializeField] Text premBalanceText;

    [SerializeField] Button buyPremiumButton;

    [SerializeField] int addMoneyAmount = 1000;
    [SerializeField] int addMoneyPrice = 10; // ������� ������� ������ �������� �� ������� �����

    [SerializeField] int buyPremiumAmount = 100;

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

    IEnumerator SimulateApiPurchase()
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
