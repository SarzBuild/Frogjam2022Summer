using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "newDialogue", menuName = "Data/Dialogue/Dialogue")]
    public class DialogueData : ScriptableObject
    {
        public List<LineData> LineSequence = new List<LineData>();
    }
}
