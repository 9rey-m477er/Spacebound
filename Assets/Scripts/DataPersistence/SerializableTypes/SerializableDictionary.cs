using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> kvp in this)
        {
            keys.Add(kvp.Key);
            Debug.Log("Added Key: " + kvp.Key);
            values.Add(kvp.Value);
            Debug.Log("Added Value: " + kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a Serializable Dictionary, but the keys (" + keys.Count + ") did not match the values (" + values.Count + "). Something has gone wrong.");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
            Debug.Log(this);
        }
    }
}
