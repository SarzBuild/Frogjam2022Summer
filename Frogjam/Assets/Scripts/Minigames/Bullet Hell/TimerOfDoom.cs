using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerOfDoom : MonoBehaviour
{
    public BulletSpawner Froggerina;
    private float _maxTime = 30;
    private float _timeRemaining;
    private TextMeshProUGUI _UItimeRemaining;

    private void Awake()
    {
        _UItimeRemaining = gameObject.GetComponent<TextMeshProUGUI>();
        ResetTimer();
    }
    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        UpdateUI();
        if(_timeRemaining < 15)
        {
            Froggerina.Crying = true;
        }    

        if(_timeRemaining < 0)
        {
            // Trigger end of game sequence
        }
    }

    private void UpdateUI()
    {
        _UItimeRemaining.text = Mathf.Round(_timeRemaining).ToString();
    }

    public void ResetTimer()
    {
        _timeRemaining = _maxTime;
        UpdateUI();
        Froggerina.Crying = false;
    }

}
