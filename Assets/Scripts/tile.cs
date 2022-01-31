using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public Transform player;
    public int spawnDist = 15;
    public int destroyDist = 25;

    bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {/*
        if ((player.position.z - transform.position.z > spawnDist) && !spawned)
        {
            GetComponent<LevelCreator>().spawnTile(transform.position.z);
            spawned = true;
        }
        if (player.position.z - transform.position.z > destroyDist)
        {
            Destroy(gameObject);
        }*/
    }
}
