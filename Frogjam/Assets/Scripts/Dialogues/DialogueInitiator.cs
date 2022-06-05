using System.Collections.Generic;
using Generics.Dictionaries;
using UnityEngine;

namespace Dialogues
{
    public class DialogueInitiator : MonoBehaviour
    {
        public SerializedDictionaryData SerializedDictionaryData;

        public DialogueIterator DialogueIterator;

        public void SetInitialDialogue(int index)
        {
            var i = SerializedDictionaryData.Values[index];
            InitiateDialogue(i);
        }

        private void InitiateDialogue(DialogueData dialogueData)
        {
            StartCoroutine(DialogueIterator.DialogueSequence(dialogueData));
        }  
    }
}