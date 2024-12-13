using UnityEngine;

public static class RemoveBackgroundProcessor
{
    public static Texture2D RemoveBackground(Texture2D sourceTexture, Color backgroundColor, float tolerance)
    {
        if (sourceTexture == null)
        {
            Debug.LogError("Source texture is null.");
            return null;
        }

        // 创建一个新的纹理，用于存储处理后的图像
        Texture2D newTexture = new Texture2D(sourceTexture.width, sourceTexture.height, TextureFormat.RGBA32, false);

        // 获取原始纹理的像素数据
        Color[] pixels = sourceTexture.GetPixels();

        // 遍历所有像素并处理背景色
        for (int i = 0; i < pixels.Length; i++)
        {
            if (IsColorClose(pixels[i], backgroundColor, tolerance))
            {
                // 如果像素颜色接近背景色，将其设置为透明
                pixels[i] = new Color(0, 0, 0, 0);
            }
        }

        // 将处理后的像素数据应用到新的纹理
        newTexture.SetPixels(pixels);
        newTexture.Apply();

        return newTexture;
    }

    // 判断两个颜色是否接近
    private static bool IsColorClose(Color a, Color b, float tolerance)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
