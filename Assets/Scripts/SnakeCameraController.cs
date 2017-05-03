using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeCameraController : MonoBehaviour {

    public SnakeModel player;

    private const string SCORE_TEXT = "Score: ";
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
        
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.GetCameraPosition() + offset;
        gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Text>().text = SCORE_TEXT + player.CurrentScore;
    }
}
