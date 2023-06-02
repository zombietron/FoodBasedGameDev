using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateWaveInfo : MonoBehaviour
{
    [SerializeField] WaveManager wave;
    [SerializeField] TextMeshProUGUI waveTitleText;
    // Start is called before the first frame update

    private void OnEnable()
    {
        waveTitleText.text = "Wave " + wave.wave.waveNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
