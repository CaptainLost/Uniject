using System.Linq;
using UnityEngine;

namespace Uniject
{

    public static class Bootstrap
    {
        private static UnijectBrain s_brainObject;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            CreateBrain();
            LoadProjectContainer();
        }

        private static void CreateBrain()
        {
            GameObject brainObject = new GameObject();
            s_brainObject = brainObject.AddComponent<UnijectBrain>();

            brainObject.name = "Uniject Brain";

            GameObject.DontDestroyOnLoad(brainObject);
        }

        private static void LoadProjectContainer()
        {
            Object[] projectContainerPrefabs = Resources.LoadAll("Project Container", typeof(GameObject));

            if (projectContainerPrefabs.Length > 1)
            {
                Logging.Warn("Multiplie project containers in resource files, choosing only one prefab");
            }

            GameObject selectedPrefab = projectContainerPrefabs.FirstOrDefault() as GameObject;

            if (selectedPrefab == null)
                return;

            GameObject projectContainer = GameObject.Instantiate(selectedPrefab);
            projectContainer.transform.SetParent(s_brainObject.transform);

            projectContainer.name = selectedPrefab.name;
        }
    }
}