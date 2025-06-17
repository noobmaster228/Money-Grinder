using UnityEngine;

public class SkyboxRandomizer : MonoBehaviour
{
    [System.Serializable]
    public struct SkyboxWithFog
    {
        public Material skybox;
        public Color fogColor;
    }

    [SerializeField] SkyboxWithFog[] skyboxesWithFog;

    void Start()
    {
        if (skyboxesWithFog.Length == 0) return;

        var chosen = skyboxesWithFog[Random.Range(0, skyboxesWithFog.Length)];

        RenderSettings.skybox = chosen.skybox;
        RenderSettings.fog = true;
        RenderSettings.fogColor = chosen.fogColor;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.03f;

        DynamicGI.UpdateEnvironment();
    }
}
