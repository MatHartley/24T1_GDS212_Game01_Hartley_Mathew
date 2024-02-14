using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private GameObject fly;

    [Header("Spawning Options")]
    [SerializeField] private float respawnTime;
    private float respawnCount;

    [Header("SFX")]
    private AudioSource flyBuzz;

    // Start is called before the first frame update
    void Start()
    {
        respawnCount = respawnTime;
        flyBuzz = GameObject.Find("FlyBuzzSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        respawnCount -= Time.deltaTime;
        Debug.Log("Fly Spawn: " + respawnCount);
        if (respawnCount <= 0)
        {
            GameObject spawn = Instantiate(fly) as GameObject;
            flyBuzz.Play();
            spawn.transform.position = new Vector2(Random.Range(-6.5f, 6.5f), Random.Range(-4f, 4f));
            respawnCount = respawnTime;
        }
    }
}