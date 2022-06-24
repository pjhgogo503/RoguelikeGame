using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PossessionRadialProgress : MonoBehaviour
{
    public Text timer;
    public Image circlegauge;
    public bool test = false;
    float time = 5;

    void Start()
    {
        timer.text = "5";
    }

    void Update()
    {
        time -= Time.deltaTime;
        timer.text = Mathf.Floor(time).ToString();
        circlegauge.fillAmount = time / 5;
        if (timer.text == "0") test = true;
    }

}
