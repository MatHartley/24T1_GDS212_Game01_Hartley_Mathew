using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [Header("Silk Management")]
    private SilkManager silkManager;
    private ScoreSaver scoreSaver;
    [SerializeField] private int silkReturn;

    [Header("SFX")]
    private AudioSource flyCrunch;

    // Start is called before the first frame update
    void Start()
    {
        silkManager = GameObject.FindObjectOfType<SilkManager>();
        scoreSaver = GameObject.FindObjectOfType<ScoreSaver>();
        flyCrunch = GameObject.Find("FlyCrunchSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            if (silkManager.silkCurrent + silkReturn <= 100)
            {
                silkManager.silkCurrent += silkReturn;
            }
            else
            {
                silkManager.silkCurrent = 100;
            }

            flyCrunch.Play();
            scoreSaver.flyCount++;
            Destroy(gameObject);
        }
    }
}
