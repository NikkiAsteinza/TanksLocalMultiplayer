using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    public double timeCounter = 0.0f; // set the starting time counter
    public bool timerIsRunning = false;

    public void Reset()
    {
        UpdateTimeCounter(0);
    }

    public void Stop()
    {
        timerIsRunning = false;
    }
    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            UpdateTimeCounter(timeCounter + Time.deltaTime);
        }
    }

    private void UpdateTimeCounter(double time)
    {
        timeCounter = time;
        _text.text = timeCounter.ToString();
    }
}
