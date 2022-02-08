using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private TextMeshProUGUI msg;
    [SerializeField] private TextMeshProUGUI interactiveMsg;
    [Range(0f, 10f)] public float distance = 2f;

    public static MessageSystem Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        HideAll();
    }

    private void Update()
    {
        RaycastHit hit;
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        // no hit
        if (!Physics.Raycast(ray, out hit, distance))
        {
            HideAll();
            return;
        }

        // get object message
        var msgObj = hit.transform.GetComponent<MsgObj>();
        // not a message object
        if (msgObj == null)
        {
            HideAll();
            return;
        }

        ShowMessage(msgObj.Message);

        if (!msgObj.IsInteractable) return;
        ShowInteractiveMessage();
        if (Input.GetKeyDown(KeyCode.F))
        {
            var interactiveEvent = msgObj.InteractiveEvent;
            interactiveEvent?.Interact();
        }
        
    }


    private static void HideAll()
    {
        HideMessage();
        HideInteractiveMessage();
    }

    private static void ShowMessage(string message)
    {
        Instance.msg.text = message;
        var color = Instance.msg.color;
        color.a = 1f;
        Instance.msg.color = color;
    }

    private static void HideMessage()
    {
        var color = Instance.msg.color;
        color.a = 0f;
        Instance.msg.color = color;
    }

    private static void ShowInteractiveMessage()
    {
        var color = Instance.interactiveMsg.color;
        color.a = 1f;
        Instance.interactiveMsg.color = color;
    }
    
    private static void HideInteractiveMessage()
    {
        var color = Instance.interactiveMsg.color;
        color.a = 0f;
        Instance.interactiveMsg.color = color;
    }
}