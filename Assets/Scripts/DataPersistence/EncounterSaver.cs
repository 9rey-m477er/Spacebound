using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSaver : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    public List<GameObject> removeObjects = new List<GameObject>();

    [ContextMenu("Generate GUID for encounter ID")]
    private void generateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private GameObject encounter;

    public bool cleared = false;
    public bool started = false;

    public void clearEncounter()
    {
        cleared = true;
        encounter.SetActive(false);
        if (removeObjects.Count > 0 )
        {
            foreach (GameObject go in removeObjects)
            {
                go.SetActive(false);
            }
        }    
    }

    public void resetEncounter()
    {
        started = false;
    }

    public void LoadData(GameData data)
    {
        data.staticsCleared.TryGetValue(id, out cleared);
        if (cleared)
        {
            encounter.SetActive(false);
            if (removeObjects.Count > 0)
            {
                foreach (GameObject go in removeObjects)
                {
                    go.SetActive(false);
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.staticsCleared.ContainsKey(id))
        {
            data.staticsCleared.Remove(id);
        }
        data.staticsCleared.Add(id, cleared);
    }
}
