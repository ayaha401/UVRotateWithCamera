using UnityEditor;

public class CreateURPShaderTemplates : EditorWindow
{
    private const string UNLIT_TEMPLATE_PATH = "Assets/Editor/AyahaGraphicDevelopTools/URPShaderTemplates/Template/TemplateURPUnlitShader.txt";
    [MenuItem("Assets/Create/Shader/Unlit Shader(URP)")]
    private static void CreateURPUnlitTemp()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(UNLIT_TEMPLATE_PATH, "URPUnlit.shader");
    }

    private const string UI_DEFAULT_TEMPLATE_PATH = "Assets/Editor/AyahaGraphicDevelopTools/URPShaderTemplates/Template/TemplateURPUIDefaultShader.txt";
    [MenuItem("Assets/Create/Shader/UI Default Shader(URP)")]
    private static void CreateURPUITemp()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(UI_DEFAULT_TEMPLATE_PATH, "URPUI.shader");
    }

    private const string HLSL_TEMPLATE_PATH = "Assets/Editor/AyahaGraphicDevelopTools/URPShaderTemplates/Template/TemplateHLSL.txt";
    [MenuItem("Assets/Create/Shader/HLSL Template")]
    private static void CreateHLSLTemp()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(HLSL_TEMPLATE_PATH, "Temp.hlsl");
    }
}
