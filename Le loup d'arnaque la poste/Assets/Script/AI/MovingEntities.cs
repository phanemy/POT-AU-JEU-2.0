﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEnties : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 Position2D
    {
        get { return transform.position; }
        private set { }
    }

    public DirectionEnum Direction{get; private set;}

    protected Pathfinder pathFinder;

    protected Vector2 targetPos;
    protected Vector2[] path;
    protected int indexDest;
    public bool isMoving;
    protected Vector2 movement;
    protected Rigidbody2D rb;
    
    // Start is called before the first frame update

    protected void Awake()
    {
        indexDest = -1;
        isMoving = false;
        AINavMeshGenerator gen = GameObject.FindWithTag("navMeshGenerator").GetComponent<AINavMeshGenerator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (gen == null)
            Debug.LogError("missing grid Generator");
        pathFinder = new Pathfinder(gen);
    }

    protected abstract bool choseDestination();
    protected abstract void move();
}
