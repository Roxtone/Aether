using UnityEditor;

namespace Aether.Editor
{
    public static class AetherTemplateCreator
    {
        [MenuItem("Assets/Create/Aether/Aether Event", false, 80)]
        public static void CreateAetherEvent()
        {
            string packagePath = "Packages/com.github.roxtone.aether/Editor/Templates/AetherEvent.cs.txt";
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(packagePath, "AetherEvent.cs");
        }

        [MenuItem("Assets/Create/Aether/Aether Context Event", false, 80)]
        public static void CreateAetherContextEvent()
        {
            string packagePath = "Packages/com.github.roxtone.aether/Editor/Templates/AetherContextEvent.cs.txt";
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(packagePath, "AetherContextEvent.cs");
        }
    }
}