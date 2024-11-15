using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSaver : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate GUID for encounter ID")]
    private void generateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private GameObject enemy;

    public bool defeated = false;

    public void LoadData(GameData data)
    {
        data.bossesDefeated.TryGetValue(id, out defeated);
        if (defeated)
        {
            enemy.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving Encounter: " + enemy.name);
        if (data.bossesDefeated.ContainsKey(id))
        {
            data.bossesDefeated.Remove(id);
        }
        data.bossesDefeated.Add(id, defeated);
        Debug.Log("Saved: " + id + ", " + defeated);
    }
}
