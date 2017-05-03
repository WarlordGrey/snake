using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAllBodyPartController : MonoBehaviour
{
    public int BodyNumber { get; set; }
    public bool HasInitTouch { get; set; }

    // Use this for initialization
    void Start () {
        HasInitTouch = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
