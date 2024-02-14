using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpriteManager : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite playerGooball;
    public Sprite playerSpider;
    private SpriteRenderer spriteRenderer;

    [Header("Materials")]
    public Material gooballMaterial;
    public Material spiderMaterial;

    //[Header("Script Ref")]
    //private ArachnophobiaMode arachnophobiaMode;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //arachnophobiaMode = FindObjectOfType<ArachnophobiaMode>();
        //arachnophobiaMode.CheckArachnophobia();
        if (PlayerPrefs.GetInt("Arachnophobia") == 0)
        {
            spriteRenderer.sprite = playerSpider;
            spriteRenderer.material = spiderMaterial;
        }
        else if (PlayerPrefs.GetInt("Arachnophobia") == 1)
        {
            spriteRenderer.sprite = playerGooball;
            spriteRenderer.material = gooballMaterial;
        }
    }
}
