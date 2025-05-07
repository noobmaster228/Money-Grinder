using UnityEngine;

public class SkyboxRandomizer : MonoBehaviour
{
    public Material[] skyboxes; // ������� ���� ��� ��������� � ����������

    void Start()
    {
        if (skyboxes.Length == 0) return;

        Material chosenSkybox = skyboxes[Random.Range(0, skyboxes.Length)];
        RenderSettings.skybox = chosenSkybox;

        // ����� ������ ���������� ����� (���� �����), ����� �������� ������
        DynamicGI.UpdateEnvironment();
    }
}
