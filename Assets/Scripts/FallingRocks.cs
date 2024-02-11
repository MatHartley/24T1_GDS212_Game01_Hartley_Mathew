using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private GameObject boulder;

    [Header("Spawning Options")]
    [SerializeField] private float fallSpeed;
    [SerializeField] private float respawnTime;
    private float respawnCount;
   
    private void Start()
    {
        respawnCount = respawnTime;
    }
    // Update is called once per frame
    void Update()
    {
        respawnCount -= Time.deltaTime;
        Debug.Log(respawnCount);
        if (respawnCount <= 0)
        {
            GameObject spawn = Instantiate(boulder) as GameObject;
            spawn.transform.position = new Vector2(Random.Range(-6.5f, 6.5f), 7);
            spawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallSpeed);
            respawnCount = respawnTime;
        }
    }
}
