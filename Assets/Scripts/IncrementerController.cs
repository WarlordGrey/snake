using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementerController : MonoBehaviour
{

    private const float SLEEP_TIME = 25;
    private const float REPEAT_RATE = 25;

    public ApplicationSnake App { get; set; }
    
    void Start()
    {
        gameObject.name = GameObjectNames.SNAKE_INCREMENTER_NAME;
    }
    
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
