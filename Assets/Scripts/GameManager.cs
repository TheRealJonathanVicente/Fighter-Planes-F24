using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateEnemy2", 2f, 4f);
        InvokeRepeating("CreateEnemy3", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-9f, 9f), 8f, 0), Quaternion.identity);
    }

    void CreateEnemy2()
    {
        Instantiate(enemy2, new Vector3(-12, Random.Range(1f, 5.5f), 0), Quaternion.identity);
    }

    void CreateEnemy3()
    {
        Instantiate(enemy3, new Vector3(Random.Range(-10f, -5f), Random.Range(4f, 15f), 0), Quaternion.identity);
    }
}
