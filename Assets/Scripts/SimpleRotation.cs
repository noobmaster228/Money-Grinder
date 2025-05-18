using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 125f, 0f);
    public bool enableFloating;
    public bool enableRotation;
    public float floatAmplitude = 0.5f; // ��������� ��������
    public float floatFrequency = 1f;   // ������� (���-�� ��������� � �������)
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // ��������
        if (enableRotation)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
        // ������������ �������� (���� ��������)
        if (enableFloating)
        {
            float offset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
        }
    }
}
