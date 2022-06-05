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
    [SerializeField] AudioSource _countdownAudioLow;
    [SerializeField] AudioSource _countdownAudioHigh;

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
            // TODO: Trigger end of game sequence
        }
    }

    private void UpdateUI()
    {
        string previousTimeRemaining = _UItimeRemaining.text;
        _UItimeRemaining.text = Mathf.Round(_timeRemaining).ToString();
        if(_UItimeRemaining.text != previousTimeRemaining && _UItimeRemaining.text != "30")
        {
            // Clock shifted, play sound
            if(_timeRemaining > 10)
            {
                _countdownAudioLow.Play();
            }
            else
            {
                _countdownAudioHigh.Play();
            }
        }
    }

    public void ResetTimer()
    {
        _timeRemaining = _maxTime;
        UpdateUI();
        Froggerina.Crying = false;
    }

}
