# Gizmos Service

## Overview
A service for registering the playbacks of gizmos-callbacks. 

The callback is registered using the service and must return a `bool` value, where `true` means to continue executing the callback in the future, and `false` means to stop and remove the registered callback.

When registering a callback, you also need to provide the name of the category it is registered in. You can make public constants and insert them instead of literals.

Provides camera occlusion for gizmos, but disabled by default.

## Install
You can download a unity package from [the latest release](../../releases).

## Usage
Samples are provided in the package. 

`GizmosSystem : IGizmosSystem` is a simple class without inheritance from `MonoBehaviour`. The task of introducing this dependency into your project (by using *Singleton*, *DI*, etc.) is your concern; the samples provide an example of creating and using a service instance.

When drawing with gizmos - use `CustomGizmos` class.

To register new gizmo-callback, use method `RegisterGizmo`:
```cs
private IGizmosSystem system;

private void Awake() {
    system = new GizmosSystem();
}

private void Start() {
    system.RegisterGizmo("Templates", () => {
        if (counter > 1_000) {
            return false;
        }
        
        CustomGizmos.Color = Color.red;
        CustomGizmos.DrawLine(Vector3.zero, Vector3.one);
        counter++;
        return true;
    });
}
```

The service has a variation of a similar method for setting a timer, called `RegisterGizmoTemp`.

Sample #1 shows the use of templates for slightly more complex shapes that have an outline, background or arrow.

Sample #2 shows a canvas with buttons to enable/disable categories, or enable/disable the rendering of all gizmos.
