using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class DialogueInitiator : MonoBehaviour
    {
        public List<LineData> LineSequence = new List<LineData>();
        public DialogueIterator DialogueIterator;
    
        public void InitiateDialogue()
        {
            StartCoroutine(DialogueIterator.DialogueSequence(LineSequence));
        }  
    }
}