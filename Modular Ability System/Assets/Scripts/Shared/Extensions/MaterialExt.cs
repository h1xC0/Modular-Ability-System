using UnityEngine;

namespace Shared.Extensions
{
    public static class MaterialExt
    {
        public static void ToOpaqueMode(this Material material)
        {
            material.SetOverrideTag("RenderType", "");
            material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }
   
        public static void ToFadeMode(this Material material)
        {
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
        }
        
        // public static void ToOpaqueMode(this MaterialPropertyBlock propertyBlock)
        // {
        //     propertyBlock.SetOverrideTag("RenderType", "");
        //     propertyBlock.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
        //     propertyBlock.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
        //     propertyBlock.SetInt("_ZWrite", 1);
        //     propertyBlock.DisableKeyword("_ALPHATEST_ON");
        //     propertyBlock.DisableKeyword("_ALPHABLEND_ON");
        //     propertyBlock.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //     propertyBlock.renderQueue = -1;
        // }
        //
        // public static void ToFadeMode(this MaterialPropertyBlock propertyBlock)
        // {
        //     propertyBlock.SetOverrideTag("RenderType", "Transparent");
        //     propertyBlock.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
        //     propertyBlock.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //     propertyBlock.SetInt("_ZWrite", 0);
        //     propertyBlock.DisableKeyword("_ALPHATEST_ON");
        //     propertyBlock.EnableKeyword("_ALPHABLEND_ON");
        //     propertyBlock.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //     propertyBlock.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
        // }

    }
}