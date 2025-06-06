using UnityEngine;

public class PlayerSkinApplier : MonoBehaviour
{
    public SkinCatalog skinCatalog;
    public Transform playerParent; // объект "Player" (родитель nogi)
    public string nogiObjectName = "nogi"; // имя заменяемого объекта
    public PlayerControls playerControls;   // ссылка на PlayerControls (или найди автоматически)
    [SerializeField] UIManager uiManager;

    void Awake()
    {
        // Если не назначено в инспекторе — ищем на родителе
        if (playerControls == null)
            playerControls = playerParent.GetComponent<PlayerControls>();

        var save = SaveManager.LoadProgress();
        string activeSkinId = save.ActiveSkinId;

        // Находим старый nogi
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
            uiManager.PlayerModel = skinModel;
            playerControls.PlayerModel = skinModel;
            foreach (var ps in skinModel.GetComponentsInChildren<ParticleSystem>(true))
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            playerControls.ShieldEffect = skinModel.transform.Find("Shield effect")?.GetComponent<ParticleSystem>();
            playerControls.SpeedEffect = skinModel.transform.Find("Speed effect")?.GetComponent<ParticleSystem>();
            playerControls.walkEffect = skinModel.transform.Find("walkEffect")?.GetComponent<ParticleSystem>();
            playerControls.PEffect = skinModel.transform.Find("+Effect")?.GetComponent<ParticleSystem>();
            playerControls.NEffect = skinModel.transform.Find("-Effect")?.GetComponent<ParticleSystem>();
            playerControls.ATMEffect = skinModel.transform.Find("ATMEffect")?.GetComponent<ParticleSystem>();
            playerControls.SawHitEffect = skinModel.transform.Find("SawHitEffect")?.GetComponent<ParticleSystem>();
            playerControls.NoColorEffect = skinModel.transform.Find("NoColorEffect")?.GetComponent<ParticleSystem>();
        }
        else if (skin != null && oldNogi != null && skin.skinMaterial != null)
        {
            var rend = oldNogi.GetComponentInChildren<Renderer>();
            if (rend != null)
                rend.material = skin.skinMaterial;

            // Обновляем ссылку
            if (playerControls != null)
                playerControls.PlayerModel = oldNogi.gameObject;
        }

    }
}
