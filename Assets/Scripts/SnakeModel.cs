﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeModel : MonoBehaviour
{

    public const int WIN_MUL = 10;
    public const int GROUND_X = 0;
    public const int GROUND_Z = 0;
    public const char EMPTY_SPACE = ' ';

    private const float POSITION_THRESHOLD = 4.5f;
    private const int START_INCREMENTER = 0;

    public Camera camera;
    public Light light;
    public GameObject snakeHeadConstructor;
    public GameObject snakeBodyConstructor;
    public GameObject wallBodyConstructor;
    public GameObject incrementerBodyConstructor;
    public PhysicMaterial groundPhysMaterial;
    public Texture2D[] groundTexture;
    public float speed;
    private Vector3 startPosition = new Vector3(1, 5f, 1);

    public Vector3 tmpPos;

    public List<GameObject> AllWalls { get; set; }
    
    public int Incrementer { get; set; }

    public GameObject Ground { get; set; }

    public int CurrentScore { get; set; }

    public int LevelWinScore { get; set; }
    public int CurrentLevel { get; set; }

    public List<string> LevelMap { get; set; }

    public GameObject CurrentIncrementerGameObject { get; set; }

    // Use this for initialization
    void Start () {
        SnakeBody = null;
        gameObject.transform.position = startPosition;
        CanAddNewElement = false;
        InitIncrementor();
        CurrentScore = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(SnakeBody != null)
        {
            if(SnakeBody.Count > 0)
            {
                //gameObject.transform.position = SnakeBody.First.Value.transform.position;
            }
        }
    }

    public LinkedList<GameObject> SnakeBody { get; set; }

    internal SnakeDirection Direction { get; set; }

    public bool HasSamePosition(Vector3 pos1, Vector3 pos2)
    {
        bool samePosX = Mathf.Abs(pos1.x - pos2.x) < POSITION_THRESHOLD;
        bool samePosY = Mathf.Abs(pos1.y - pos2.y) < POSITION_THRESHOLD;
        bool samePosZ = Mathf.Abs(pos1.z - pos2.z) < POSITION_THRESHOLD;
        return samePosX && samePosY && samePosZ;
    }

    public bool CanAddNewElement { get; set; }

    public Vector3 GetCameraPosition()
    {
        /*
        if((SnakeBody == null) || (SnakeBody.Count == 0))
        {
            return transform.position;
        }
        return SnakeBody.First.Value.transform.position;
        */
        return transform.position;
    }

    public void InitIncrementor()
    {
        Incrementer = START_INCREMENTER;
    }


    public bool IsSnakeEmpty()
    {
        return ((SnakeBody == null) || (SnakeBody.Count == 0));
    }

    public int GetLevelWidth()
    {
        if(LevelMap == null)
        {
            return 0;
        }
        return LevelMap[0].Length;
    }


    public int GetLevelLength()
    {
        if (LevelMap == null)
        {
            return 0;
        }
        return LevelMap.Count;
    }

}
