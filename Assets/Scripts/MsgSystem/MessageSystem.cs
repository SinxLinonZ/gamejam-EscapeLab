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

        Instance.msg.gameObject.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        // no hit
        if (!Physics.Raycast(ray, out hit, distance))
        {
            HideMessage();
            return;
        }
        
        // get object message
        var msgObj = hit.transform.GetComponent<MsgObj>();
        // not a message object
        if (msgObj == null)
        {
            HideMessage();
            return;
        }

        ShowMessage(msgObj.msg);
    }


    public static void ShowMessage(string message)
    {
        Instance.msg.text = message;
        Instance.msg.gameObject.SetActive(true);
    }

    public static void HideMessage()
    {
        Instance.msg.gameObject.SetActive(false);
    }
}