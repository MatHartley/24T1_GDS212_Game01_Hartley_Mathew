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
    [SerializeField] private float speedUpCooldown;
    private float speedUpCount;
    private float respawnCount;

    // Start is called before the first frame update
    private void Start()
    {
        respawnCount = respawnTime;
        speedUpCount = speedUpCooldown;
    }
    // Update is called once per frame
    void Update()
    {
        speedUpCount -= Time.deltaTime;
        Debug.Log("Boulder Speedup: " + speedUpCount);
        respawnCount -= Time.deltaTime;
        Debug.Log("Boulder Spawn: " + respawnCount);

        if (speedUpCount <=0)
        {
            respawnTime = respawnTime * 0.75f;
            fallSpeed = fallSpeed * 1.25f;
            speedUpCount = speedUpCooldown;
        }
        
        if (respawnCount <= 0)
        {
            GameObject spawn = Instantiate(boulder) as GameObject;
            spawn.transform.position = new Vector2(Random.Range(-6.5f, 6.5f), 6.5f);
            spawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallSpeed);
            respawnCount = respawnTime;
        }
    }
}
