using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArachnophobiaMode : MonoBehaviour
{
    public void ArachnophobiaOn()
    {
        PlayerPrefs.SetInt("Arachnophobia", 1);
        Debug.Log("Arachnophobia On");
    }

    public void ArachnophobiaOff()
    {
        PlayerPrefs.SetInt("Arachnophobia", 0);
        Debug.Log("Arachnophobia Off");
    }
}
