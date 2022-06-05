using System.Collections.Generic;
using Dialogues;
using UnityEngine;

namespace Generics.Dictionaries
{
    [CreateAssetMenu(fileName = "newMasterDialogueManager", menuName = "Data/Dialogue/MasterDialogueManager")]
    public class SerializedDictionaryData : ScriptableObject
    {
        [SerializeField] 
        List<int> keys = new List<int>();
        [SerializeField]
        List<DialogueData> values = new List<DialogueData>();

        public List<int> Keys { get => keys; set => keys = value; }
        public List<DialogueData> Values { get => values; set => values = value; }
    }
}