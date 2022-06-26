using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PossessionRadialProgress : MonoBehaviour
{
    public Text timer;
    public Image circlegauge;
    public bool timeover = false;
    float time = 5;

    void Start()
    {
        //timer.text = "6";
    }

    void Update()
    {
        time -= Time.deltaTime;
        //timer.text = Mathf.Floor(time).ToString();
        circlegauge.fillAmount = time / 5;
        if (time <= 0.000000f) timeover = true;
    }

}
