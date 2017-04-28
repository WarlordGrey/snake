using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickLevelController : MonoBehaviour {

    public Application app;
    public PickLevelView pickLevelV;

    private int curPage;

    // Use this for initialization
    void Start () {
        curPage = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (pickLevelV.IsLevelsCountChanged)
        {
            InitLevels();
            pickLevelV.IsLevelsCountChanged = false;
        }
	}

    public void setVisibility(bool visibility)
    {
        pickLevelV.pickLevelM.pickLevelCanvas.gameObject.SetActive(visibility);
    }

    private void OnBtnPickLevelClick()
    {

    }

    public void OnReturnToMenuClick()
    {
        setVisibility(false);
        app.mainMenuCtrl.setVisibility(true);
    }

    public void OnNextPageClick()
    {
        PickLevelModel plm = pickLevelV.pickLevelM;
        if(curPage < (plm.LevelsCount / plm.GetMaxLevelsPerPage()))
        {
            curPage++;
            pickLevelV.RenderLvlButtons(curPage);
        }
    }

    public void OnPrevPageClick()
    {
        if (curPage > 1)
        {
            curPage--;
            pickLevelV.RenderLvlButtons(curPage);
        }
    }

    private void InitLevels()
    {
        while (pickLevelV.pickLevelM.LevelsButtons == null);
        while (pickLevelV.pickLevelM.LevelsButtons.Capacity == 0);
        foreach (Button cur in pickLevelV.pickLevelM.LevelsButtons)
        {
            cur.onClick.AddListener(() => OnBtnPickLevelClick());
        }
        pickLevelV.RenderLvlButtons(curPage);
    }

}
