using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickLevelView : MonoBehaviour {

    private const float BTN_SPACE_X = 20;
    private const float BTN_SPACE_Y = 20;
    
    private const float BTN_DELTA_X = 240;
    private const float BTN_DELTA_Y = 80;

    public PickLevelModel pickLevelM;

    public bool IsLevelsInited { get; set; }
    
    void Start() {
        IsLevelsInited = false;
    }

    void Update() {
        
    }

    public void RenderLvlButtons(int page)
    {
        int curLevel = (page - 1) * pickLevelM.GetMaxLevelsPerPage() + 1;
        foreach (Button cur in pickLevelM.LevelsButtons)
        {
            string levelDesc = pickLevelM.GetLevelDescription(curLevel);
            cur.GetComponentInChildren<Text>().text = levelDesc;
            if (pickLevelM.IsLevelUnlocked(curLevel))
            {
                ColorBlock cb = cur.colors;
                cb.normalColor = new Color(0, 0.38f, 0.11f);
                cb.highlightedColor = new Color(0, 0.4f, 0.2f);
                cur.colors = cb;
                cur.GetComponentInChildren<Text>().color = new Color(1, 1, 1);
            }
            else
            {
                ColorBlock cb = cur.colors;
                cb.normalColor = Color.grey;
                cur.colors = cb;
                cur.GetComponentInChildren<Text>().color = new Color(0, 0, 0);
            }
            cur.enabled = pickLevelM.IsLevelUnlocked(curLevel);
            curLevel++;
        }
    }

    public void InitLevelsButtons()
    {
        pickLevelM.LevelsButtons = new List<Button>();
        int btnCnt = pickLevelM.GetMaxLevelsPerPage();
        for (int i = 0; i < btnCnt; i++)
        {
            Button newLvl = Button.Instantiate(pickLevelM.typicalPickLevelButton, gameObject.GetComponentInChildren<Canvas>().gameObject.transform);
            newLvl.gameObject.AddComponent<ButtonLevelData>();
            newLvl.gameObject.GetComponent<ButtonLevelData>().Lvl = i + 1;
            pickLevelM.LevelsButtons.Add(newLvl);
        }
    }

    public void InitLevels()
    {
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
        float x = Screen.width / 2;
        float y = Screen.height / 2;
        float z = 0;
        x += col * (BTN_SPACE_X + btnW) - BTN_DELTA_X;
        y += row * (BTN_SPACE_Y + btnH) - BTN_DELTA_Y;
        return new Vector3(x, y, z);
    }

}
