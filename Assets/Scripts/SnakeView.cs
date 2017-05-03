using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeView : MonoBehaviour
{

    private const float Y_BODY_OFFSET = 1;
    private const float DELAY = 0;
    private const float STEP_TIME = 1;
    private const float DISTANCE = 0.2f;
    private const float GROUND_Y = -0.6f;

    public int MaxTicks { get { return 20; } } 
    public int CurTick { get; set; }

    public SnakeModel snakeM;

    private Vector3 startPosition = new Vector3(0, 1f, 0);
    private Vector3 startBodyPosition = new Vector3(-1000, 1f, -1000);
    private Vector3 xBodyOffset = new Vector3(7f, 0, 0);
    private Vector3 zBodyOffset = new Vector3(0, 0, 7f);
    private int elementsLeft = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveSnake();
        gameObject.transform.position = snakeM.transform.position;
    }
    public void CreateSnake()
    {
        Init();
    }

    private void Init()
    {
        DestroySnake();
        snakeM.SnakeBody = new LinkedList<GameObject>();
        AddPartToSnake(snakeM.snakeHeadConstructor);
        snakeM.InitIncrementor();
    }

    private void MoveSnake()
    {
        if (snakeM.IsSnakeEmpty())
        {
            return;
        }
        CurTick++;
        if(CurTick >= MaxTicks)
        {
            LinkedList<GameObject> body = snakeM.SnakeBody;
            var cur = body.Last;
            while (cur != body.First)
            {
                var prev = cur.Previous;
                cur.Value.transform.position = prev.Value.transform.position;
                cur = prev;
            }
            body.First.Value.transform.position = snakeM.transform.position;
            switch (snakeM.Direction)
            {
                case SnakeDirection.RIGHT:
                    snakeM.tmpPos.x++;
                    break;
                case SnakeDirection.LEFT:
                    snakeM.tmpPos.x--;
                    break;
                case SnakeDirection.DOWN:
                    snakeM.tmpPos.z--;
                    break;
                case SnakeDirection.UP:
                    snakeM.tmpPos.z++;
                    break;
                default:
                    break;
            }
            CurTick = 0;
        }
        snakeM.transform.position = snakeM.tmpPos;
    }

    public void AddSnakeLength(int length)
    {
        for(int i = 0; i < length; i++)
        {
            AddPartToSnake();
        }
    }

    private void AddPartToSnake()
    {
        AddPartToSnake(snakeM.snakeBodyConstructor);
    }

    private void AddPartToSnake(GameObject parent)
    {
        GameObject part = GameObject.Instantiate(parent);
        if (!snakeM.SnakeBody.Contains(part))
        {
            part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            part.GetComponent<SnakeAllBodyPartController>().BodyNumber = snakeM.SnakeBody.Count;
            snakeM.SnakeBody.AddLast(part);
        }
    }

    internal void SetSnakeDirection(SnakeDirection direction)
    {
        snakeM.Direction = direction;
    }

    public void GenerateGround(float x, float z, int width, int length)
    {
        TerrainData terData = new TerrainData();
        terData.size = new Vector3(width, 1, length);
        if (snakeM.Ground == null)
        {
            snakeM.Ground = Terrain.CreateTerrainGameObject(terData);
            snakeM.Ground.GetComponent<Terrain>().gameObject.GetComponent<TerrainCollider>().material
                = snakeM.groundPhysMaterial;
        }
        else
        {
            snakeM.Ground.GetComponent<Terrain>().terrainData = terData;
        }
        snakeM.Ground.transform.position = new Vector3(x, GROUND_Y, z);
        Terrain ter = snakeM.Ground.GetComponent<Terrain>();
        RenderGroundTexture(ter.terrainData);
    }

    private void RenderGroundTexture(TerrainData terrainData)
    {
        SplatPrototype[] tex = new SplatPrototype[snakeM.groundTexture.Length];
        for (int i = 0; i < snakeM.groundTexture.Length; i++)
        {
            tex[i] = new SplatPrototype();
            tex[i].texture = snakeM.groundTexture[i];
            tex[i].tileSize = new Vector2(32, 32);
        }
        terrainData.splatPrototypes = tex;
    }

    public void GenerateIncrementer()
    {
        if (snakeM.IsSnakeEmpty())
        {
            return;
        }
        snakeM.Incrementer++;
        if(snakeM.CurrentIncrementerGameObject != null)
        {
            Destroy(snakeM.CurrentIncrementerGameObject);
        }
        snakeM.CurrentIncrementerGameObject = GameObject.Instantiate(snakeM.incrementerBodyConstructor);
        snakeM.CurrentIncrementerGameObject.transform.position = GetRandomIncrementerPosition();
    }

    public void DestroySnake()
    {
        if (snakeM.SnakeBody != null)
        {
            foreach (GameObject cur in snakeM.SnakeBody)
            {
                Destroy(cur);
            }
        }
        snakeM.SnakeBody = null;
    }

    private Vector3 GetRandomIncrementerPosition()
    {
        //TODO
        float randomX = Random.value * 10;
        float randomZ = Random.value * 10;
        return new Vector3(randomX, 0, randomZ); ;
    }
    
}
