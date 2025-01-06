using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Uniject
{
    public static class Utilities
    {
        public static readonly BindingFlags s_BindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private static ProjectContainer m_cachedProjectContainer;

        public static bool HasSceneSceneContext(ref Scene scene)
        {
            GameObject[] sceneRootObjects = scene.GetRootGameObjects();

            IEnumerable<GameObject> sceneContainerRoots = sceneRootObjects
                .Where((x) => x.GetComponentsInChildren<SceneContainer>().Any());

            return sceneContainerRoots.Any();
        }

        // TODO: Cache this
        public static SceneContainer GetSceneContainer(GameObject referenceGameObject)
        {
            Scene owningScene = referenceGameObject.scene;

            GameObject[] sceneRootGameObjects = owningScene.GetRootGameObjects();

            SceneContainer[] sceneContainers = sceneRootGameObjects
                .SelectMany(root => root.GetComponentsInChildren<SceneContainer>()).ToArray();

            if (sceneContainers.Length > 1)
            {
                Logging.Warn("Multiplie scene containers, choosing instance with lower InstanceID");
            }

            return sceneContainers.FirstOrDefault();
        }

        public static ProjectContainer GetProjectContainer()
        {
            if (m_cachedProjectContainer != null)
                return m_cachedProjectContainer;

            ProjectContainer[] projectContainers = UnityEngine.Object.FindObjectsByType<ProjectContainer>(FindObjectsSortMode.InstanceID);

            if (projectContainers.Length > 1)
            {
                Logging.Warn("Multiplie project containers, choosing instance with lower InstanceID");
            }

            m_cachedProjectContainer = projectContainers.FirstOrDefault();

            return m_cachedProjectContainer;
        }

        public static bool IsMonoBehaviourInjectable(MonoBehaviour monoBehaviour)
        {
            MemberInfo[] members = monoBehaviour.GetType().GetMembers(s_BindingFlags);

            return members.Any(IsMemberInfoInjectable);
        }

        public static bool IsMemberInfoInjectable(MemberInfo memberInfo)
        {
            return Attribute.IsDefined(memberInfo, typeof(InjectAttribute));
        }
    }
}