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

### **Bindings**
- **Instances**: Passing one instance of a given object for injection
- **Dynamic**: Dynamically created instance of a given non-mono behaviour class, constructor arguments populate based on dependencies
- **Transient**: Same as for the dynamic binder, with the difference of creating a new instance for each injection
- **Factory**: Dynamic creation of game objects, allowing dependencies to be injected before unity callbacks (e.g. Awake)

## **Getting Started**

## **Credits**
Inspired by [Zenject](https://github.com/modesttree/Zenject)
