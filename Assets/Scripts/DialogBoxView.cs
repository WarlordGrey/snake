using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxView : MonoBehaviour {

    private Button btnOk;
    private Button btnCancel;
    private Canvas canvas;
    private Text message;
    
    void Start() {
        InitButtons();
        canvas = gameObject.GetComponentInChildren<Canvas>();
        message = gameObject.GetComponentInChildren<Text>();
    }

    private void InitButtons()
    {
        foreach(Button cur in gameObject.GetComponentsInChildren<Button>())
        {
            switch (cur.gameObject.name)
            {
                case "btnOk":
                    btnOk = cur;
                    break;
                case "btnCancel":
                    btnCancel = cur;
                    break;
                default:
                    break;
            }
        }
    }
    
    void Update() {

    }

    public void SetVisibilityButtonOk(bool visibility)
    {
         SetButtonVisibility(btnOk,visibility);
    }

    public void SetVisibilityButtonCancel(bool visibility)
    {
        SetButtonVisibility(btnCancel, visibility);
    }

    private void SetButtonVisibility(Button btn, bool visibility)
    {
        btn.gameObject.SetActive(visibility);
    }

    public void SetText(string msg)
    {
        message.text = msg;
    }

    public void SetVisibility(bool visibility)
    {
        canvas.gameObject.SetActive(visibility);
        if (visibility)
        {
            canvas.transform.SetAsLastSibling();
        }
        else
        {
            canvas.transform.SetAsFirstSibling();
        }
    }
}
