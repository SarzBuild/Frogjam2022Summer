using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class DialogueInitiator : MonoBehaviour
    {
        public DialogueData DialogueData;
        public Dictionary<int, DialogueData> Dialogues = new Dictionary<int, DialogueData>();

        public DialogueIterator DialogueIterator;
    
        public void InitiateDialogue()
        {
            StartCoroutine(DialogueIterator.DialogueSequence(DialogueData));
        }  
    }
}