# **Uniject: Dependency Injection Library for Unity**
Uniject is a dependency injection library created specifically for the Unity Engine. Unity is built on an entity component system, where for such a system it is often easy to get into chaos with dependencies. API like this allows for clear dependency management while increasing the modularity of the code.

Be aware that this is a fresh plugin and may contain bugs.

## **Features**

### **Injection support**
- Fields
- Properties
- Methods
- Constructors

### **Containers**
Uniject organises dependencies through a system of containers:
- **Project Container**: For dependencies shared by the entire project (located at ‘Resources/Project Container’)
- **Scene Containers**: For scene-specific dependencies
- **GameObject Containers**: Attached to individual gameobjects, allows the creation of modular and reusable setups

A single container can only have one dependency of one type. If an injection target has several options for resolving a single dependency, the priory gets the dependency that is lower in this list.

### **Bindings**
At this time, bindings can be passed through mono behaviours installers and scriptable objects installers.

Bind types:
- **Instances**: Passing single instance of a given object for injection
- **Dynamic**: Dynamically created instance of a given non-mono behaviour class, constructor arguments populate based on dependencies
- **Transient**: Same as for the dynamic binder, with the difference of creating a new instance for each injection
- **Factory**: Dynamic creation of game objects, allowing dependencies to be injected before unity callbacks (e.g. Awake)

## **Hooking into Player Loop**

W.I.P

## **Installation**

W.I.P

## **Getting Started**

Start by creating a scene container on the scene. Right click on the scene and select _’Uniject/Scene Container’_ or create an empty object and add the _'SceneContainer'_ script manually.

Then make the first installer, create a new mono script inheriting from MonoInstaller:
```cs
using Uniject;

public class FirstMonoInstaller : MonoInstaller
{
    public override void Install(IDependencyContextBuilder contextBuilder)
    {
        
    }
}
```
Let's add our newly created installer, to a container on the scene.

Define the class of the object we want to inject:
```cs
public class GameManager
{
    private readonly string m_importantString;

    public GameManager(string importantString)
    {
        m_importantString = importantString;
    }
}
```

Bind instance of an object, and provide instances manually:
```cs
public override void Install(IDependencyContextBuilder contextBuilder)
{
    GameManager gameManager = new GameManager("Hello World");
    contextBuilder.BindInstance<GameManager>(gameManager);
}
```
This bound instance will be the equivalent of the GameManager type for the scene container.

A second option for such binding, may be to create the object dynamically:
```cs
public override void Install(IDependencyContextBuilder contextBuilder)
{
    contextBuilder.BindDynamic<GameManager>();
    contextBuilder.BindInstance<string>("Dynamic Hello World");
}
```
In order to populate the constructor argument, we must also bind this argument.

Next we need to have somewhere to inject our dependency, let's create a class _‘Service1’_:
```cs
using Uniject;
using UnityEngine;

public class Service1 : MonoBehaviour
{
    [Inject]
    private GameManager m_gameManagerField;

    [Inject]
    private GameManager m_gameManagerProperty { get; set; }

    [Inject]
    private void Construct(GameManager gameManager)
    {
        // Do whatever you want with this
    }
}
```
I have given some possible options, we can choose which method we want to inject our dependency with.

The third option here, is transient binding. Each time this type is injected, a new instance of the object will be created:
```cs
public override void Install(IDependencyContextBuilder contextBuilder)
{
    contextBuilder.BindTransient<GameManager>();
    contextBuilder.BindInstance<string>("Dynamic bind");
}
```

## **Binder Attributes**

### **Every Binder**
<details>
  <summary>SetTarget: Allows you to select the types for which injection will take place</summary>

```cs
public interface IGameManager
{

}

public class GameManager : IGameManager
{

}

public class GameInstaller : MonoInstaller
{
    public override void Install(IDependencyContextBuilder contextBuilder)
    {
        contextBuilder.BindDynamic<GameManager>()
            .SetTarget<IGameManager, GameManager>();
    }
}
```

GameManager object, will be injected into IGameManager and GameManager.
</details>

> [!IMPORTANT]
> The type to be injected must be assignable from the target type

### **Dynamic Binder**
<details>
  <summary>NonLazy: Allows an object to be created when it is binded and not when it is injected as is normally the case</summary>

```cs
public override void Install(IDependencyContextBuilder contextBuilder)
{
    contextBuilder.BindDynamic<GameManager>()
        .NonLazy();
}
```

GameManager object, will be injected into IGameManager and GameManager.
</details>

### **Transient Binder**
W.I.P

### **Factory Binder**
W.I.P

## **Credits**
Inspired by [Zenject](https://github.com/modesttree/Zenject)
