using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text currentTimeText;
    [SerializeField] private TMP_Text bestTimeText;

    private bool timerStarts;
    private float timeElapsed;
    private float bestPlayerTime = 999999999f;

    public void startTimer()
    {
        timerStarts = true;
    }
    public void resetTimer()
    {
        timeElapsed = 0;
        timerStarts = false;
    }

    public void setBestTime()
    {
        if(timeElapsed < bestPlayerTime)
        {
            string text = formatTime(timeElapsed);
            bestTimeText.text = text;
        }

        bestPlayerTime = timeElapsed;
    }

    public void setCurrentTime()
    {
        string text = formatTime(timeElapsed);
        currentTimeText.text = text;
    }

    void Update()
    {
        if (timerStarts)
        {
            timeElapsed += Time.deltaTime;
            setCurrentTime();
        }
    }

    public static string formatTime(float timeElapsed)
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        int milliseconds = Mathf.FloorToInt((timeElapsed - Mathf.Floor(timeElapsed)) * 1000f);

        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }
}
