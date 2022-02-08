using UnityEngine;

public class PCEvent : IInteractableObject
{
    public void Interact()
    {
        var a = GameObject.Find("pc");
        a.transform.localPosition += new Vector3(0, 0, -0.1f);
    }
}
