using UnityEngine;

public class PlayerSkinApplier : MonoBehaviour
{
    public SkinCatalog skinCatalog;
    public Transform playerParent; // ������ "Player" (�������� nogi)
    public string nogiObjectName = "nogi"; // ��� ����������� �������
    public PlayerControls playerControls;   // ������ �� PlayerControls (��� ����� �������������)

    void Awake()
    {
        // ���� �� ��������� � ���������� � ���� �� ��������
        if (playerControls == null)
            playerControls = playerParent.GetComponent<PlayerControls>();

        var save = SaveManager.LoadProgress();
        string activeSkinId = save.ActiveSkinId;

        // ������� ������ nogi
        Transform oldNogi = null;
        foreach (Transform child in playerParent)
            if (child.name == nogiObjectName)
                oldNogi = child;

        SkinData skin = null;
        foreach (var s in skinCatalog.skins)
            if (s.skinId == activeSkinId)
                skin = s;

        if (skin != null && skin.skinPrefab != null)
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            Vector3 scale = Vector3.one;

            if (oldNogi != null)
            {
                pos = oldNogi.localPosition;
                rot = oldNogi.localRotation;
                scale = oldNogi.localScale;
                Destroy(oldNogi.gameObject);
            }

            var skinModel = Instantiate(skin.skinPrefab, playerParent);
            skinModel.name = nogiObjectName;
            skinModel.transform.localPosition = pos;
            skinModel.transform.localRotation = rot;


            // ��������� ������ �� ����� ������ � PlayerControls
            if (playerControls != null)
                playerControls.PlayerModel = skinModel;
        }
        else if (skin != null && oldNogi != null && skin.skinMaterial != null)
        {
            var rend = oldNogi.GetComponentInChildren<Renderer>();
            if (rend != null)
                rend.material = skin.skinMaterial;

            // ��������� ������
            if (playerControls != null)
                playerControls.PlayerModel = oldNogi.gameObject;
        }
    }
}
