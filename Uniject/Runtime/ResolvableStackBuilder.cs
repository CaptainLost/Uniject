using UnityEngine;

namespace Uniject
{
    public static class ResolvableStackBuilder
    {
        public static ResolvableStack BuildResolvableStackForGameObject(GameObject gameObject, ResolvableStack resolvableStack = null)
        {
            if (resolvableStack == null)
                resolvableStack = new ResolvableStack();

            if (gameObject != null)
            {
                if (gameObject.TryGetComponent(out GameObjectContainer gameObjectContainer))
                {
                    resolvableStack.PushBack(gameObjectContainer);
                }

                if (gameObject.transform.parent != null)
                {
                    return BuildResolvableStackForGameObject(gameObject.transform.parent.gameObject, resolvableStack);
                }
            }

            SceneContainer sceneContainer = Utilities.GetSceneContainer(gameObject);
            if (sceneContainer != null)
                resolvableStack.PushBack(sceneContainer);

            ProjectContainer projectContainer = Utilities.GetProjectContainer();
            if (projectContainer != null)
                resolvableStack.PushBack(projectContainer);

            return resolvableStack;
        }
    }
}
