# Aether
Unity Game Engine Event Manager

## Installation
Install via _Add package from git URL_ in the Package Manager.

## Basic Usage

Create a separate class for each event:
```
public class ExampleEvent : Aether.Event<ExampleEvent>
{
    public readonly int Variable;

    public ExampleEvent(int variable)
    {
        Variable = variable;
    }
}
```
Add listeners and remove when not needed anymore (they will be automatically removed when the game object has been destroyed):
```
public class ExampleListener : MonoBehaviour
{
    private void Awake()
    {
        ExampleEvent.AddListener(OnEvent);
    }

    private void OnEvent(ExampleEvent eventData)
    {
        Debug.Log(eventData.Variable);
        ExampleEvent.RemoveListener(OnEvent);
    }
}
```
Create a new event instance and invoke the event:
```
public class ExampleInvoker : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        new ExampleEvent(0).Invoke();
    }
}
```

## Good Practices
Use readonly fields or private setter properties as event parameters to prevent listeners from changing them.
Use `Awake` to subscribe to events whenever possible and don't invoke any events until `Start` to be sure that all listeners have already subscribed.

## Order of Execution
If you invoke a new event from an event listener, the original event will execute on all of the remaining listeners first.

## Last Event of Type
Use Last property to access the last invoked event of a given type (can be null if the event hasn't been invoked yet).

## Context Events
Inherit from `ContextEvent` instead and pass an additional context parameter when adding a listener and invoking the event. Only listeners with the same context as the event will be executed.

## Priority
Pass an additional priority argument when adding a listener to invoke that listener pre or post other listeners for that event.

## Predicate
Use predicate field when adding a listener to only invoke the listener if a condition is met.

## Event Processed
Subscribe to EventProcessed event to handle all events of any type.
