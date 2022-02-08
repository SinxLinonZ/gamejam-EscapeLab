using System;
using UnityEngine;

public class MsgObj : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private bool isInteractable;

    [SerializeField] private string interactiveEvent;
    private IInteractableObject _interactiveEvent;

    private void Awake()
    {
        if (!isInteractable) return;

        try
        {
            _interactiveEvent = (IInteractableObject)Activator.CreateInstance(Type.GetType(interactiveEvent));
        }
        catch (Exception)
        {
            throw new Exception("Unknown event name: " + interactiveEvent);
        }
    }

    public string Message
    {
        get => message;
        set => message = value;
    }

    public bool IsInteractable
    {
        get => isInteractable;
        set => isInteractable = value;
    }
    public IInteractableObject InteractiveEvent
    {
        get => _interactiveEvent;
        set => _interactiveEvent = value;
    }
}