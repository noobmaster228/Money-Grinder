using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grayscale : MonoBehaviour
{
    private Material material;
    public Shader grShader;
    void Start()
    {
        material = new Material(grShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
