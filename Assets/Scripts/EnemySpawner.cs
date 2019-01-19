using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesList; // List of Enemy Instances
    public Transform EnemySpawnPoint; // The Spawn point, located in the center vertically, right edge of screen.

    public GameObject[] EnemyTypes = new GameObject[4]; // an array of the different enemy types that can be spawned
    public int EnemyChoice; // The choice of which enemy will spawn

    public float upperSpawnHeightLimit; // So enemy doesn't spawn outside of bounds Vertically
    public float lowerSpawnHeightLimit;

    public float spawnTime;            // How long between each spawn in seconds.
    public float levelDuration; // in seconds
    float LevelEndTime; // The time for the level to end (Stop spawning enemies)

    int tempInt;    // used to temp hold an int value, will later be converted to float.
    double tempDouble; // used to temp hold a double value, will later be converted to float.
    System.Random rnd; // Used to randomly determinde height
    float determinedSpawnHeight; // The determined spawn height, added from the tempInt and tempDouble

    Vector3 generatedSpawn; // This is the coordinate of the generated spawn, contains the determinedSpawnHeight

    public float sceneTimeLeft;

    void Awake ()
    {
        EnemyChoice = 2;
        upperSpawnHeightLimit = 4.85f;
        lowerSpawnHeightLimit = -4.85f;
        rnd = new System.Random();
    }

    private void Start()
    {
        LevelEndTime = Time.time + levelDuration;
        InvokeRepeating("Spawn", 0, spawnTime);
    }

    // Update is called once per frame
    void Update ()
    {
        sceneTimeLeft = LevelEndTime - Time.time;
		if(Time.time >= LevelEndTime)
        {
            CancelInvoke();
        }
	}

    void Spawn()
    {
        RandomlyGenerateSpawnHeight();

        SetEnemySpawnLocation();

        SpawnCorrectEnemy();
    }

    private void SpawnCorrectEnemy()
    {
        switch (EnemyChoice)
        {
            case 2:
                {
                    var temp = Instantiate(EnemyTypes[2], generatedSpawn, EnemySpawnPoint.rotation, this.transform);
                    enemiesList.Add(temp);
                    //temp.GetComponent<EnemyBehavior>().
                    break;
                }
        } // switch() end
    }

    private void SetEnemySpawnLocation()
    {
        generatedSpawn = EnemySpawnPoint.position;
        generatedSpawn.Set(EnemySpawnPoint.position.x, (EnemySpawnPoint.position.y + determinedSpawnHeight), EnemySpawnPoint.position.z);
    }

    private void RandomlyGenerateSpawnHeight()
    {
        determinedSpawnHeight = 0;

        tempInt = rnd.Next(-4, 4);
        tempDouble = rnd.NextDouble();
        determinedSpawnHeight += (float)tempInt;
        determinedSpawnHeight += (float)tempDouble;
    }
}
