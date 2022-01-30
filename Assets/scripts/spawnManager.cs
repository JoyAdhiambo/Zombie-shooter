using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public int totalNumberOfZombiesSpown;
    public float maxtimeBetweenSpawn;
    public float mintimeBetweenSpawn;
    public GameObject[] zombies;
    public Slider wave;
    public GameObject wonPanel;
    public GameObject lostPanel;

    void Start()
    {
        StartCoroutine(SpawnZombies());
        wave.maxValue = totalNumberOfZombiesSpown;
    }

    // Update is called once per frame
    void Update()
    {
        wave.value = totalNumberOfZombiesSpown;
        if (totalNumberOfZombiesSpown <= 0 && GameObject.FindGameObjectsWithTag("Player") != null && GameObject.FindGameObjectsWithTag("zombies").Length <= 0)
            wonPanel.SetActive(true);


    }
    void Spawn()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            Instantiate(zombies[Random.Range(0, zombies.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            totalNumberOfZombiesSpown--;
        }
    }
    IEnumerator SpawnZombies()
    {
        yield return new WaitForSeconds(Random.Range(mintimeBetweenSpawn, maxtimeBetweenSpawn));
        if (totalNumberOfZombiesSpown > 0)
        {
            Spawn();
            StartCoroutine(SpawnZombies());
        }


    }

}
