using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementerController : MonoBehaviour
{

    private const float SLEEP_TIME = 50;
    private const float REPEAT_RATE = 50;

    public Application App { get; set; }

    // Use this for initialization
    void Start()
    {
        gameObject.name = GameObjectNames.SNAKE_INCREMENTER_NAME;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void Awake()
    {
        InvokeRepeating("UpdatePosition", SLEEP_TIME,REPEAT_RATE);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == GameObjectNames.SNAKE_WALL_NAME)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        gameObject.transform.position = App.snakeCtrl.snakeV.GetRandomMapPosition();
    }

}
