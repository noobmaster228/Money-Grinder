using UnityEngine;

public class WaterScroller : MonoBehaviour
{
    public float scrollSpeed; // скорость прокрутки
    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        scrollSpeed = 0.04f;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = new Vector2(Time.time * scrollSpeed, 0);
        rend.material.SetTextureOffset("_MainTex", offset);
    }
}

