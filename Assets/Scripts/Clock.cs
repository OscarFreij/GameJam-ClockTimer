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
    public double OldTotalTimeLeft = 0;

    public Image ClockRingFill;
    public Text CountDownText;

    public GameObject ControlPanel;

    public bool ClockIsRunning = false;
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
        ControlPanel = transform.Find("ControlPanel").gameObject;

        ControlPanel.transform.Find("Start_Pause").transform.Find("Text").GetComponent<Text>().text = "Start";
        ControlPanel.transform.Find("Stop_Reset").transform.Find("Text").GetComponent<Text>().text = "Reset";

        CountDownText.text = ($"00:00:00:00");
    }

    // Update is called once per frame
    void Update()
    {
        if ((float)TotalTimeLeft / (float)TotalTimeFinal > 0 && ClockIsRunning)
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

            if (OldTotalTimeLeft < 0)
            {
                OldTotalTimeLeft = 0;
            }
            
            if (TotalTimeLeft < 0)
            {
                TotalTimeLeft = 0;
                CountDownText.text = ($"00:00:00:00");
                ClockIsRunning = false;
                ControlPanel.transform.Find("Start_Pause").transform.Find("Text").GetComponent<Text>().text = "Start";
                ControlPanel.transform.Find("Stop_Reset").transform.Find("Text").GetComponent<Text>().text = "Reset";
            }
        }
    }

    public void StartPause()
    {
        if (!ClockIsRunning)
        {

            if (OldTotalTimeLeft == 0)
            {
                ControlPanel.transform.Find("Start_Pause").transform.Find("Text").GetComponent<Text>().text = "Pause";
                ControlPanel.transform.Find("Stop_Reset").transform.Find("Text").GetComponent<Text>().text = "Stop";

                try
                {
                    Hours = Int32.Parse(ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldHH").transform.Find("Text").GetComponent<Text>().text);
                }
                catch
                {
                    Hours = 0;
                }
            
                try
                {
                    Minutes = Int32.Parse(ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldMM").transform.Find("Text").GetComponent<Text>().text);
                }
                catch
                {
                    Minutes = 0;
                }

                try
                {
                    Seconds = Int32.Parse(ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldSS").transform.Find("Text").GetComponent<Text>().text);
                }
                catch
                {
                    Seconds = 0;
                }

                try
                {
                    Milliseconds = Int32.Parse(ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldMS").transform.Find("Text").GetComponent<Text>().text);
                }
                catch
                {
                    Milliseconds = 0;
                }

                TotalTimeFinal = 0;
                TotalTimeLeft += Hours;
                TotalTimeLeft += Minutes / 60;
                TotalTimeLeft += Seconds / (60 * 60);
                TotalTimeLeft += Milliseconds / 3600000;
                TotalTimeFinal = TotalTimeLeft;
            }
            else
            {
                TotalTimeLeft = OldTotalTimeLeft;
            }


            ClockIsRunning = true;
        }
        else
        {
            OldTotalTimeLeft = TotalTimeLeft;
            ControlPanel.transform.Find("Start_Pause").transform.Find("Text").GetComponent<Text>().text = "Start";
            ControlPanel.transform.Find("Stop_Reset").transform.Find("Text").GetComponent<Text>().text = "Reset";
            ClockIsRunning = false;
        }
    }

    public void StopReset()
    {
        if(ControlPanel.transform.Find("Stop_Reset").transform.Find("Text").GetComponent<Text>().text == "Stop")
        {
            ControlPanel.transform.Find("Start_Pause").transform.Find("Text").GetComponent<Text>().text = "Start";
            ControlPanel.transform.Find("Stop_Reset").transform.Find("Text").GetComponent<Text>().text = "Reset";
            OldTotalTimeLeft = 0;
            TotalTimeLeft = 0;
            ClockIsRunning = false;
        }
        else if (ControlPanel.transform.Find("Stop_Reset").transform.Find("Text").GetComponent<Text>().text == "Reset")
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            Milliseconds = 0;

            OldTotalTimeLeft = 0;
            TotalTimeLeft = 0;

            ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldHH").transform.Find("Text").GetComponent<Text>().text = Hours.ToString();
            ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldMM").transform.Find("Text").GetComponent<Text>().text = Minutes.ToString();
            ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldSS").transform.Find("Text").GetComponent<Text>().text = Seconds.ToString();
            ControlPanel.transform.Find("TimePanel").transform.Find("InputFieldMS").transform.Find("Text").GetComponent<Text>().text = Milliseconds.ToString();

        }
    }
}
