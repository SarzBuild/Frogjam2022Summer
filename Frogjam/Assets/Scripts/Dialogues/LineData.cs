using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "newLine", menuName = "Data/Dialogue/Line")]
    public class LineData : ScriptableObject
    {
        [Header("Text Options")]
        [TextArea] [SerializeField] public string Input;

        [Header("Time Parameters")]
        [SerializeField] public float Delay;
        [SerializeField] public float DelayBeforeNextLine;
    }
}