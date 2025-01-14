using System.IO;
using UnityEditor;
using UnityEngine;

namespace Uniject
{
    public static class GameObjectMenu
    {
        [MenuItem("GameObject/Uniject/Scene Container")]
        static private void CreateSceneContainer(MenuCommand menuCommand)
        {
            GameObject sceneContext = new GameObject("Scene Container");
            sceneContext.AddComponent<SceneContainer>();

            Undo.RegisterCreatedObjectUndo(sceneContext, "Create " + sceneContext.name);

            Selection.activeObject = sceneContext;
        }

        [MenuItem("GameObject/Uniject/Game Object Container")]
        static private void CreateGameObjectContainer(MenuCommand menuCommand)
        {
            GameObject sceneContext = new GameObject("Game Object Container");
            sceneContext.AddComponent<GameObjectContainer>();

            GameObjectUtility.SetParentAndAlign(sceneContext, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(sceneContext, "Create " + sceneContext.name);

            Selection.activeObject = sceneContext;
        }
    }

    public static class AssetsMenu
    {
        [MenuItem("Assets/Create/Uniject/Project Container")]
        static private void CreateProjectContainer(MenuCommand menuCommand)
        {
            string folderPath = "Assets";

            Object selectedObject = Selection.activeObject;
            if (selectedObject != null)
            {
                string selectedPath = AssetDatabase.GetAssetPath(selectedObject);

                if (AssetDatabase.IsValidFolder(selectedPath))
                {
                    folderPath = selectedPath;
                }
                else
                {
                    folderPath = Path.GetDirectoryName(selectedPath);
                }
            }

            string parentFolderName = Path.GetFileName(folderPath);

            folderPath += "/Project Container.prefab";

            if (parentFolderName != "Resources")
            {
                Logging.Warn("Project Container must be created in a 'Resources' folder.");

                return;
            }

            GameObject projectContainer = new GameObject();
            projectContainer.AddComponent<ProjectContainer>();

            bool wasPrefabSuccess;

            GameObject savedPrefab = PrefabUtility.SaveAsPrefabAsset(projectContainer, folderPath, out wasPrefabSuccess);
            Selection.activeObject = savedPrefab;

            GameObject.DestroyImmediate(projectContainer);

            if (!wasPrefabSuccess)
            {
                Logging.Warn($"Failed creating Project Container in {folderPath}");
            }
        }
    }
}
