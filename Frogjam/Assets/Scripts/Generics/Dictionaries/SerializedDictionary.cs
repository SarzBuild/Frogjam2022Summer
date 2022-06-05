using System.Collections.Generic;
using Dialogues;
using UnityEngine;

namespace Generics.Dictionaries
{
    public class SerializedDictionary : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        private SerializedDictionaryData dictionaryData;

        [SerializeField]
        private List<int> keys = new List<int>();
        [SerializeField]
        private List<DialogueData> values = new List<DialogueData>();

        private Dictionary<int, DialogueData> myDictionary = new Dictionary<int, DialogueData>();

        public bool modifyValues;

        private void Awake()
        {
            for (int i = 0; i < Mathf.Min(dictionaryData.Keys.Count, dictionaryData.Values.Count); i++)
            {
                myDictionary.Add(dictionaryData.Keys[i], dictionaryData.Values[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            if (modifyValues == false)
            {
                keys.Clear();
                values.Clear();
                for (int i = 0; i < Mathf.Min(dictionaryData.Keys.Count, dictionaryData.Values.Count); i++)
                {
                    keys.Add(dictionaryData.Keys[i]);
                    values.Add(dictionaryData.Values[i]);
                }
            }
        }

        public void OnAfterDeserialize()
        {
        
        }

        public void DeserializeDictionary()
        {
            Debug.Log("DESERIALIZATION");
            myDictionary = new Dictionary<int, DialogueData>();
            dictionaryData.Keys.Clear();
            dictionaryData.Values.Clear();
            for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
            {
                dictionaryData.Keys.Add(keys[i]);
                dictionaryData.Values.Add(values[i]);
                myDictionary.Add(keys[i], values[i]);
            }
            modifyValues = false;
        }

        public void PrintDictionary()
        {
            foreach (var pair in myDictionary)
            {
                Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
            }
        }
    }
}