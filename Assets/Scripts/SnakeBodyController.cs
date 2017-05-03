using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : SnakeAllBodyPartController
{
    
	// Use this for initialization
	void Start () {
        gameObject.name = GameObjectNames.SNAKE_BODY_NAME;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
