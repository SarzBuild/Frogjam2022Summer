using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogressBar : MonoBehaviour
{

    private float _startingProgress = 0; // Position of the bar before it starts moving
    private float _currentProgress = 0; // How much of the bar is currently filled in percentage
    private float _targetProgress = 0; // Where we want the bar to end up

    private float _timer;
    private float _speed;

    public float[] _movementPercentages;
    private int _movesMade = 0;

    private bool _calledFirstDialogue = false;

    public GameObject Froggerina;

    private void Update()
    {
        if(_currentProgress != _targetProgress)
        {
            _timer += Time.deltaTime * _speed;
            _currentProgress = Mathf.Lerp(_startingProgress, _targetProgress, _timer);
            GetComponent<SpriteRenderer>().size = new Vector2(11 * _currentProgress / 100, 2);
            transform.localPosition = new Vector3(11 * _currentProgress / 200 - 5.5f, 0, 0);
            Froggerina.transform.position = transform.position + new Vector3(5.5f * _currentProgress / 100 - 1, 0.0625f, 0);
        }
        else
        {
            _startingProgress = _targetProgress;
            _timer = 0;
            if(_startingProgress == _movementPercentages[0] && !_calledFirstDialogue)
            {
                // dialogue event, don't spawn froggerina just yet
                _calledFirstDialogue = true;
                SpawnFroggerina();
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateBar(5);
        }
    }

    public void UpdateBar(float timeRequired)
    {
        if(_timer != 0)
        {
            Debug.Log("Trying to update the frogress bar too early! Cancelling operation.");
            return;
        }
        _targetProgress = _startingProgress + _movementPercentages[_movesMade++];
        _speed = 1 / timeRequired;
    }

    public void SpawnFroggerina()
    {
        if (!Froggerina.activeInHierarchy)
        {
            Froggerina.SetActive(true);
            Froggerina.GetComponent<Animator>().SetBool("Spawning", true);
        }
    }


}
