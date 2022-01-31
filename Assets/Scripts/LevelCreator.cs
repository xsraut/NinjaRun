using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Transform transformPos;
    public Transform nextSpawnPos;
    Transform player;

    public int tileLength = 30;
    public Vector3 TileSpaawnPos;
    public int GapBetweenTiles = 2;
    public GameObject startTile;
    public GameObject[] tiles;

    private Vector3 TileSpeed;

    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Controller>().transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(!spawned && (Vector3.Distance(transformPos.position, player.position) < 8 ))
        {
            spawnTile();
            spawned = true;
        }

        if (spawned && (Vector3.Distance(transformPos.position, player.position) > 42))
        {
            Destroy(gameObject);
        }
    }

    public void spawnTile()
    {

        int randNum = Random.Range(0, tiles.Length - 1);
        GameObject tile = Instantiate(tiles[randNum], nextSpawnPos.position, player.rotation);

    }
}
