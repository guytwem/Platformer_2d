using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGeneration : MonoBehaviour
{
    public List<GameObject> platformPrefabs;
    // when we are to the left of the spawn location spawn the next prefab
    public Transform spawnLocation;

    public GameObject worldParent;

    GameObject lastPlatformGO;
    Platform lastPlatform;

    private void Start()
    {
        lastPlatformGO = Instantiate(platformPrefabs[0],
                                    worldParent.transform.position,
                                    Quaternion.identity,
                                    worldParent.transform);

        lastPlatform = lastPlatformGO.GetComponent<Platform>();
    }

    private void Update()
    {
        // if our last platform is to the left of spawn location, spawn next platform
        if (lastPlatform.rightBounds.transform.position.x <= spawnLocation.position.x)
        {
            int randomIndex = Random.Range(0, platformPrefabs.Count);

            GameObject newSpawn = Instantiate(platformPrefabs[randomIndex],
                                                    worldParent.transform);
            Platform newPlatform = newSpawn.GetComponent<Platform>();

            Vector3 positionDifference = newPlatform.leftBounds.transform.position - newSpawn.transform.position;
            Vector3 spawnLocation = lastPlatform.rightBounds.transform.position - positionDifference;

            newSpawn.transform.position = spawnLocation;

            lastPlatformGO = newSpawn;
            lastPlatform = newPlatform;
        }
    }
}
