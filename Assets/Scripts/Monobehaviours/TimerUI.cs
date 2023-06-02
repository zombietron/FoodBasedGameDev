using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    [SerializeField] int timerLength;

    [SerializeField] WaveManager wMgr;

    // Start is called before the first frame update
    
    public void SetTimerText(int val)
    {
        timerText.text = val.ToString();
    }

    public void StartTimer()
    {
        StartCoroutine(RunTimer(timerLength));
    }

    public IEnumerator RunTimer(int time)
    {
        var currentTime = time;
        
        while (currentTime >= 0)
        {
            SetTimerText(currentTime);
            currentTime--;
            yield return new WaitForSeconds(1);
        }

        wMgr.UpdateWaveState(WaveManager.WaveState.running);
        yield break;
    }
    

}
