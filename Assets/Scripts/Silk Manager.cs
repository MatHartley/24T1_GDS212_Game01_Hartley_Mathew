using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SilkManager : MonoBehaviour
{
    [Header("Components")]
    public Slider silkSlider;
    public TextMeshProUGUI silkText;

    [Header("Silk Values")]
    public int silkCurrent;
    [SerializeField] private int silkMax;

    // Start is called before the first frame update
    void Start()
    {
        silkCurrent = silkMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (silkCurrent > silkMax)
        {
            silkCurrent = silkMax;
        }
        else if (silkCurrent < 0)
        {
            silkCurrent = 0;
        }

        silkSlider.value = silkCurrent;
        silkText.text = silkCurrent.ToString("F0");
    }

    public void ReduceSilk(int silkCost)
    {
        silkCurrent -= silkCost;
    }
}
