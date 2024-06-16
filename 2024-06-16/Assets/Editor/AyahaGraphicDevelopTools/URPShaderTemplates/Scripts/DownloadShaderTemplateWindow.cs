using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;
using Unity.EditorCoroutines.Editor;

public class DownloadShaderTemplateWindow : EditorWindow
{
    private const string LINK_PREFIX = "https://github.com/ayaha401";
    
    private string _outputPath;

    [MenuItem("AyahaGraphicDevelopTools/ShaderTemplate")]
    public static void ShowWindow()
    {
        var window = GetWindow<DownloadShaderTemplateWindow>("DownloadShaderTemplateWindow");
        window.titleContent = new GUIContent("DownloadShaderTemplateWindow");
    }

    private void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope())
        {
            GUILayout.Label("OutputPath");
            _outputPath = GUILayout.TextField(_outputPath);

            DownloadCell("Noise", "Shader-Noise", "Noise.hlsl");
            DownloadCell("Wave", "Shader-Wave", "Wave.hlsl");
            DownloadCell("Curve", "Shader-Curve", "Curve.hlsl");
            DownloadCell("Macro", "Shader-Macro", "Macro.hlsl");
            DownloadCell("MathFunction", "Shader-MathFunction", "MathFunction.hlsl");
            DownloadCell("ColorFunction", "Shader-ColorFunction", "ColorFunction.hlsl");
            DownloadCell("SDF", "Shader-SDF", "SDF.hlsl");
        }
    }

    /// <summary>
    /// ダウンロードする項目
    /// </summary>
    /// <param name="cellLabel">項目名</param>
    /// <param name="repositoryName">リポジトリ名</param>
    /// <param name="fileName">ファイル名</param>
    private void DownloadCell(string cellLabel, string repositoryName, string fileName)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label(cellLabel);
            if (GUILayout.Button("Download"))
            {
                EditorCoroutineUtility.StartCoroutine(DownloadFile(LINK_PREFIX, repositoryName, fileName, _outputPath), this);
                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("Open Link"))
            {
                Application.OpenURL(GetRepositoryLink(LINK_PREFIX, repositoryName));
            }
        }
    }

    /// <summary>
    /// ファイルをダウンロードする
    /// </summary>
    /// <param name="prefix">URLの先頭部分</param>
    /// <param name="repositoryName">リポジトリ名</param>
    /// <param name="fileName">ファイル名</param>
    /// <param name="outputPath">出力先</param>
    private IEnumerator DownloadFile(string prefix, string repositoryName, string fileName, string outputPath)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetFilePath(prefix, repositoryName, fileName)))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download file: " + request.error);
            }
            else
            {
                string directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                outputPath = Path.Combine(outputPath, fileName);
                File.WriteAllBytes(outputPath, request.downloadHandler.data);
                Debug.Log("File downloaded to: " + outputPath);
            }
        }
    }

    /// <summary>
    /// ダウンロードするファイルパスを作成
    /// </summary>
    /// <param name="prefix">URLの先頭部分</param>
    /// <param name="repositoryName">リポジトリ名</param>
    /// <param name="fileName">ファイル名</param>
    private string GetFilePath(string prefix, string repositoryName, string fileName)
    {
        return Path.Combine(prefix, repositoryName, "raw/main", fileName);
    }

    /// <summary>
    /// リポジトリのあるリンクを作成
    /// </summary>
    /// <param name="prefix">URLの先頭部分</param>
    /// <param name="repositoryName">リポジトリ名</param>
    private string GetRepositoryLink(string prefix, string repositoryName)
    {
        return Path.Combine(prefix, repositoryName);
    }
}
