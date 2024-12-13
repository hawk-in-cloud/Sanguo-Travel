using UnityEditor;
using UnityEngine;

public class RemoveBackgroundEditor : EditorWindow
{
  private Texture2D sourceTexture;
  private Color backgroundColor = new Color(1.0f, 0.0f, 1.0f, 1.0f); // 默认背景颜色为紫色
  private float tolerance = 0.1f;

  [MenuItem("Tools/Remove Background Color")]
  public static void ShowWindow()
  {
    GetWindow<RemoveBackgroundEditor>("Remove Background Color");
  }

  private void OnGUI()
  {
    GUILayout.Label("Remove Background Color from Texture", EditorStyles.boldLabel);

    sourceTexture = (Texture2D)EditorGUILayout.ObjectField("Source Texture", sourceTexture, typeof(Texture2D), false);
    backgroundColor = EditorGUILayout.ColorField("Background Color", backgroundColor);
    tolerance = EditorGUILayout.Slider("Tolerance", tolerance, 0.0f, 1.0f);

    if (GUILayout.Button("Remove Background"))
    {
      if (sourceTexture != null)
      {
        Texture2D newTexture = RemoveBackgroundProcessor.RemoveBackground(sourceTexture, backgroundColor, tolerance);
        SaveTexture(newTexture);
      }
      else
      {
        EditorUtility.DisplayDialog("Error", "Please select a source texture.", "OK");
      }
    }
  }

  private void SaveTexture(Texture2D texture)
  {
    if (texture == null) return;

    string path = EditorUtility.SaveFilePanel("Save Texture", "", "NewTexture.png", "png");
    if (path.Length != 0)
    {
      byte[] pngData = texture.EncodeToPNG();
      if (pngData != null)
      {
        System.IO.File.WriteAllBytes(path, pngData);
        AssetDatabase.Refresh();
      }
    }
  }
}
