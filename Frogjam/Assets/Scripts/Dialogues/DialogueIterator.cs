﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GeneralJTUtils;

namespace Dialogues
{
    public class DialogueIterator : MonoBehaviour
    {
        [SerializeField] public GameObject DialogueMenuObject;
        [SerializeField] public TextMeshProUGUI TextHolder;

        public IEnumerator DialogueSequence(List<LineData> currentDialogue)
        {
            JTUtils.ToggleObjects(new List<GameObject>(){DialogueMenuObject},true);

            if (TextHolder.text != "") //Checks to see if the base object is empty, if not, sets to empty.
            {
                TextHolder.text = "";
            }
            
            for (int i = 0; i < currentDialogue.Count; i++)
            {
                LineIterator writer = new LineIterator();

                if (TextHolder.text != "")
                {
                    if (TextHolder.text[^1].ToString() != "")
                    {
                        TextHolder.text += " ";
                    }
                }

                StartCoroutine(writer.InputStringToText(TextHolder, currentDialogue[i].Input, currentDialogue[i].Delay, currentDialogue[i].DelayBeforeNextLine));
                
                yield return new WaitUntil(() => writer.LineFinished);

                if (currentDialogue[i].Input == "")
                {
                    TextHolder.text = "";
                }
            }
        
            JTUtils.ToggleObjects(new List<GameObject>(){DialogueMenuObject},false);
        }
    }
}