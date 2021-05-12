using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circular : MonoBehaviour
{
    public Image image;
    private float currentValue = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentValue += 0.1f * 0.1f;
        image.fillAmount = currentValue % 1.0f;
    }
}
