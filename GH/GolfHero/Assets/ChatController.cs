using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour {

    public static ChatController instance;

    public InputField input;
    public Button sendButton;
    //public Image chatBackground;

    public bool textMode;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

    }

    private void ShowChat()
    {
        // chatBackground.gameObject.SetActive(true);
        input.gameObject.SetActive(true);
        sendButton.gameObject.SetActive(true);
    }

    private void HideChat()
    {
        // chatBackground.gameObject.SetActive(false);
        input.gameObject.SetActive(false);
        sendButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        GameObject inputObj = GameObject.FindGameObjectWithTag("Chat Button");

        if (inputObj)
            sendButton = inputObj.GetComponent<Button>();

        inputObj = GameObject.FindGameObjectWithTag("Chat Input");

        if (inputObj)
            input = inputObj.GetComponent<InputField>();

        /*
        inputObj = GameObject.FindGameObjectWithTag("Chat Background");

        if (inputObj)
            chatBackground = inputObj.GetComponent<Image>();
            */

        if (input == null || sendButton == null)
        {
            textMode = false;
            return;
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (textMode)
            {
                sendButton.onClick.Invoke();
                input.DeactivateInputField();
                HideChat();
                textMode = false;
            } else
            {
                ShowChat();
                input.ActivateInputField();
                textMode = true;
            }
        }

        if (!textMode)
        {
            HideChat();
        }
    }
}
