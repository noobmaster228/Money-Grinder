using UnityEngine;

public class SkyboxRandomizer : MonoBehaviour
{
    public Material[] skyboxes; // Присвой сюда все скайбоксы в инспекторе

    void Start()
    {
        if (skyboxes.Length == 0) return;

        Material chosenSkybox = skyboxes[Random.Range(0, skyboxes.Length)];
        RenderSettings.skybox = chosenSkybox;

        // Чтобы эффект применился сразу (если нужен), можно обновить шейдер
        DynamicGI.UpdateEnvironment();
    }
}
