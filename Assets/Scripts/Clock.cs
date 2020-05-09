using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public double Hours = 0;
    public double Minutes = 0;
    public double Seconds = 0;
    public double Milliseconds = 0;

    public double TotalTimeLeft = 0;
    public double TotalTimeFinal = 0;

    public Image ClockRingFill;
    public Text CountDownText;
    // Start is called before the first frame update
    void Start()
    {
        TotalTimeLeft += Hours;
        TotalTimeLeft += Minutes / 60;
        TotalTimeLeft += Seconds / (60*60);
        TotalTimeLeft += Milliseconds / 3600000;

        TotalTimeFinal = TotalTimeLeft;
        ClockRingFill = transform.Find("Ring").transform.Find("Fill").GetComponent<Image>();
        CountDownText = transform.Find("CountDownText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((float)TotalTimeLeft / (float)TotalTimeFinal > 0)
        {
            TotalTimeLeft -= Time.deltaTime / 60 / 60;

            double mmRaw = ((TotalTimeLeft % 1) * 60);
            double ssRaw = ((mmRaw % 1) * 60);
            double msRaw = ((ssRaw % 1) * 1000);

            int hh = Convert.ToInt32(TotalTimeLeft - (TotalTimeLeft % 1));
            int mm = Convert.ToInt32(mmRaw - (mmRaw % 1));
            int ss = Convert.ToInt32(ssRaw - (ssRaw % 1));
            int ms = Convert.ToInt32(msRaw - (msRaw % 1));


            CountDownText.text = ($"{hh}:{mm}:{ss}:{ms}");

            ClockRingFill.fillAmount = 1 - ((float)TotalTimeLeft / (float)TotalTimeFinal);

        }
    }
}
