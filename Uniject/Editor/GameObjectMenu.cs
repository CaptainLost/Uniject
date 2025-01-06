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
}
