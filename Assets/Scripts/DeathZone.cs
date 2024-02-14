using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private SceneController sceneController;
    private AudioSource deathSFX;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        deathSFX = GameObject.Find("DeathSFX").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        if (collision.tag == "Player")
        {
            deathSFX.Play();
            StartCoroutine(GameEnd());
        }
    }
    private IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(2);
        sceneController.LoadWinScreen();
    }
}

