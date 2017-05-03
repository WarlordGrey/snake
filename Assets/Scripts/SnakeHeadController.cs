using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : SnakeAllBodyPartController {

    public ApplicationSnake App { get; set; }

    void Start()
    {
        gameObject.name = GameObjectNames.SNAKE_HEAD_NAME;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == GameObjectNames.SNAKE_INCREMENTER_NAME)
        {
            Destroy(col.gameObject);
            App.snakeCtrl.IncreaseSnakeSize();
        }
        if(col.gameObject.name == GameObjectNames.SNAKE_BODY_NAME)
        {
            
            SnakeAllBodyPartController sabc = col.gameObject.GetComponent<SnakeAllBodyPartController>();
            if ((sabc.BodyNumber - this.BodyNumber) > 4)
            {
                if (!sabc.HasInitTouch)
                {
                    App.snakeCtrl.Lose();
                }
                sabc.HasInitTouch = false;
            }
        }
        if (col.gameObject.name == GameObjectNames.SNAKE_WALL_NAME)
        {
            App.snakeCtrl.Lose();
        }
    }

}
