using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineIterator
{
    public bool LineFinished { get; private set; }
    private bool _keyDown;
    private int _indexCount;
    
    public IEnumerator InputStringToText(TextMeshProUGUI holder, string input, float delay, float nextLineDelay)
    {
        yield return null; //Make sure that the last input is not on the same frame

        if (input.Length != 0)
        {
            while (!_keyDown && input.Length != _indexCount + 1)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    Debug.Log(input.Length.ToString() + _indexCount);
                    holder.text += input[i];
                    _indexCount = i;

                    if (_keyDown) { break; } //If key is pressed, stop the loop

                    yield return WaitForKeyPressOrSeconds(KeyCode.Mouse0,delay); 
                }
            }
            if (_keyDown) //Set the text to input if the player skips the dialogue
            {
                for (int i = _indexCount + 1; i < input.Length; i++)
                {
                    holder.text += input[i];
                }
            }
        }

        yield return null; //Make sure that the next input is not on the same frame

        float timer = 0;
        while (!Input.GetKeyDown(KeyCode.Mouse0) && timer < nextLineDelay) //Wait until one of the two trigger happens to continue
        {
            timer += Time.fixedDeltaTime; 
            yield return null;
        }
        LineFinished = true;
    }
    
    
    private IEnumerator WaitForKeyPressOrSeconds(KeyCode key, float delay)
    {
        bool done = false;
        float timer = 0;
        while(!done)
        {
            if (Input.GetKeyDown(key)) //If key pressed, change variable to true and finish the loop
            {
                _keyDown = true;
                done = true;
            }
            
            if(timer > delay) //If time reaches the delay, finish the loop
            {
                done = true; 
            }

            timer += Time.fixedDeltaTime;

            yield return null;
        }
    }
}