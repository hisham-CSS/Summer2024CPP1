using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    public GameObject[] spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        float randXPos = Random.Range(1.5f, 234.9f);
        float randYPos = Random.Range(1.2f, 9.41f);

        transform.position = new Vector2(randXPos, randYPos);

        int randNum = Random.Range(0, spawnPrefab.Length);

        if (spawnPrefab[randNum] == null) return;

        Instantiate(spawnPrefab[randNum], transform.position, transform.rotation);
    }
}