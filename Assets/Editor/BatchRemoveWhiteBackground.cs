using UnityEngine;
using UnityEditor;
using System.IO;

public class BatchRemoveWhiteBackground : EditorWindow
{
    [MenuItem("Tools/Batch Remove White Background")]
    public static void ShowWindow()
    {
        GetWindow<BatchRemoveWhiteBackground>("去除背景图白色背景");
    }

    private string folderPath = "Assets/Textures";

    void OnGUI()
    {
        GUILayout.Label("Batch Remove White Background", EditorStyles.boldLabel);
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Process Textures"))
        {
            ProcessTextures(folderPath);
        }
    }

    void ProcessTextures(string folderPath)
    {
        // 获取文件夹中的所有 PNG 文件
        string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string assetPath = file.Replace(Application.dataPath, "Assets");
            // 加载纹理
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (texture != null)
            {
                Texture2D newTexture = RemoveWhiteBackgroundFromTexture(texture);
                byte[] bytes = newTexture.EncodeToPNG();
                File.WriteAllBytes(file, bytes);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }
        }

        AssetDatabase.Refresh();
    }

    /// <summary> 去除白色背景
    Texture2D RemoveWhiteBackgroundFromTexture(Texture2D texture)
    {
        Texture2D newTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                // 获取原始纹理的像素颜色
                Color color = texture.GetPixel(x, y);
                // 如果像素颜色接近白色（RGB 值接近 1），将其 alpha 值设置为 0（即透明）
                if (color.r > 0.9f && color.g > 0.9f && color.b > 0.9f)
                {
                    color.a = 0;
                }
                newTexture.SetPixel(x, y, color);
            }
        }

        newTexture.Apply();
        return newTexture;
    }
}
