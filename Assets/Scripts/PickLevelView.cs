using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickLevelView : MonoBehaviour {

    private const float BTN_SPACE_X = 20;
    private const float BTN_SPACE_Y = 20;

    private const float BTN_DELTA_X = 50;
    private const float BTN_DELTA_Y = 75;

    public PickLevelModel pickLevelM;

    public bool IsLevelsInited { get; set; }

    // Use this for initialization
    void Start() {
        IsLevelsInited = false;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void RenderLvlButtons(int page)
    {
        int curLevel = (page - 1) * pickLevelM.GetMaxLevelsPerPage() + 1;
        foreach (Button cur in pickLevelM.LevelsButtons)
        {
            string levelDesc = pickLevelM.GetLevelDescription(curLevel);
            cur.GetComponentInChildren<Text>().text = levelDesc;
            cur.enabled = pickLevelM.IsLevelUnlocked(curLevel);
            curLevel++;
        }
    }

    public void InitLevels()
    {
        //while (pickLevelM.LevelsButtons == null) ;
        //while (pickLevelM.LevelsButtons.Capacity == 0) ;
        //while (pickLevelM.GetLevelsData() == null) ;
        //while (pickLevelM.GetLevelsData().AllLevels == null) ;
        int row = 0;
        int col = 0;
        foreach (Button cur in pickLevelM.LevelsButtons)
        {
            cur.transform.position = GetButtonPosition(
                row,
                col,
                cur.GetComponent<RectTransform>().rect.width,
                cur.GetComponent<RectTransform>().rect.height
            );
            col++;
            if (col == PickLevelModel.LEVELS_PER_COLUMN )
            {
                row++;
                if (row == PickLevelModel.LEVELS_PER_ROW)
                {
                    break;
                }
                col = 0;
            }
        }
        RenderLvlButtons(1);
    }

    private Vector3 GetButtonPosition(int row, int col, float btnW, float btnH)
    {
        float x = pickLevelM.pickLevelCanvas.transform.position.x - (pickLevelM.pickLevelCanvas.GetComponent<RectTransform>().rect.width / 3);
        float y = pickLevelM.pickLevelCanvas.transform.position.y;
        float z = pickLevelM.pickLevelCanvas.transform.position.z;
        x += col * (BTN_SPACE_X + btnW) + BTN_DELTA_X;
        y += row * (BTN_SPACE_Y + btnH) - BTN_DELTA_Y;
        return new Vector3(x, y, z);
    }

}
