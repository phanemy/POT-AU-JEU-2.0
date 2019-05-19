﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : SpawnerAbstract
{


    public MobBehaviour itemToSpawn;

    public override void Spawn()
    {

        float x = Random.Range(transform.position.x - xScale, transform.position.x + xScale);
        float y = Random.Range(transform.position.y - yScale, transform.position.y + yScale);

        Utils.InstantiateMob(new Vector3(x, y, 0), itemToSpawn, this);
        actualNumber++;
    }
}
