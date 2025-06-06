using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GrayscaleRenderFeature : ScriptableRendererFeature
{
    [Tooltip("Материал с шейдером Grayscale")]
    public Material grayscaleMaterial;

    public static bool IsActive = false; // глобальный флаг для активации

    GrayscalePass pass;

    public override void Create()
    {
        pass = new GrayscalePass(grayscaleMaterial);
        pass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }

    class GrayscalePass : ScriptableRenderPass
    {
        Material material;

        public GrayscalePass(Material mat)
        {
            material = mat;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (!IsActive || material == null)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("GrayscaleEffect");
            var src = renderingData.cameraData.renderer.cameraColorTarget;

            int tempRT = Shader.PropertyToID("_TempRT");
            cmd.GetTemporaryRT(tempRT, renderingData.cameraData.camera.pixelWidth, renderingData.cameraData.camera.pixelHeight, 0, FilterMode.Bilinear, RenderTextureFormat.Default);
            Blit(cmd, src, tempRT, material, 0);
            Blit(cmd, tempRT, src);
            cmd.ReleaseTemporaryRT(tempRT);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
