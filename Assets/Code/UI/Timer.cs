using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    public float Time = 0.0f; // set the starting time counter
    public bool IsTimeRunning = false;

    public void Reset()
    {
        UpdateTimeCounter(0);
    }

    public void Stop()
    {
        IsTimeRunning = false;
    }
    private void Start()
    {
        IsTimeRunning = true;
    }

    private void Update()
    {
        if (IsTimeRunning)
        {
            UpdateTimeCounter(Time + UnityEngine.Time.deltaTime);
        }
    }

    private void UpdateTimeCounter(float time)
    {
        Time = time;
        _text.text = Math.Round(Time,2).ToString();
    }
}
