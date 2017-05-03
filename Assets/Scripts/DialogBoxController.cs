using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxController : MonoBehaviour {

    public ApplicationSnake app;
    public DialogBoxView dialBV;

    public delegate void OnClickActionImpl();

    public OnClickActionImpl okClickImpl;
    public OnClickActionImpl cancelClickImpl;

    void Start () {
        SetVisibility(false);
	}
	
	void Update () {
		
	}

    public void OnButtonOkClick()
    {
        if (okClickImpl != null)
        {
            okClickImpl();
        }
    }

    public void OnButtonCancelClick()
    {
        if (cancelClickImpl != null)
        {
            cancelClickImpl();
        }
    }

    public void ClearButtonsActions()
    {
        okClickImpl = null;
        cancelClickImpl = null;
    }

    public void SetText(string msg)
    {
        dialBV.SetText(msg);
    }

    public void SetVisibilityButtonOk(bool visibility)
    {
        dialBV.SetVisibilityButtonOk(visibility);
    }

    public void SetVisibilityButtonCancel(bool visibility)
    {
        dialBV.SetVisibilityButtonCancel(visibility);
    }

    public void SetVisibility(bool visibility)
    {
        dialBV.SetVisibility(visibility);
    }

}
