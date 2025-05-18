using UnityEngine;

public class Grayscale : MonoBehaviour
{
    private Material material;
    public Shader grShader;
    void Start()
    {
        material = new Material(grShader); //создаётся новый шейдер
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination) //преобразовывает изображение с использованием материала с шейдером
    {
        Graphics.Blit(source, destination, material);
    }
}