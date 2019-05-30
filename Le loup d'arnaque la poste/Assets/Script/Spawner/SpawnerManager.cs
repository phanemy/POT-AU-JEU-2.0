using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<SpawnerAbstract> itemsSpawner;
    public int nbToEnable;
    private int nbEnable;

    public void Awake()
    {
        nbEnable = 0;
        int idxToEnable;
        foreach (SpawnerAbstract spawner in itemsSpawner)
        {
            spawner.enabled = false;
        }
        do
        {
            idxToEnable = Random.Range(0, itemsSpawner.Count);
            if (!itemsSpawner[idxToEnable].enabled)
            {
                itemsSpawner[idxToEnable].enabled = true;
                nbEnable++;
            }
        } while (nbEnable < nbToEnable) ;
    }
}
