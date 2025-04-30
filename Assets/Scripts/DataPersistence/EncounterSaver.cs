using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSaver : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    public List<GameObject> removeObjects = new List<GameObject>();

    [ContextMenu("Generate GUID for encounter ID")]
    private void generateGUID() //generates a unique ID for the staticsCleared dictionary
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private GameObject encounter;

    public bool cleared = false;
    public bool started = false;

    public void clearEncounter() //sets the encounter as cleared and deactivates all requisite game objects
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

    public void resetEncounter() //resets the encounter
    {
        started = false;
    }

    public void LoadData(GameData data)
    {
        //searches for the encounter in the staticsCleared dictionary
        data.staticsCleared.TryGetValue(id, out cleared);
        if (cleared)
        {
            //disables it and all connected objects if it has been cleared
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
        //checks to make sure there isn't a duplicate ID present
        if (data.staticsCleared.ContainsKey(id))
        {
            //removes if there is
            data.staticsCleared.Remove(id);
        }

        //adds encounter to staticsCleared dictionary
        data.staticsCleared.Add(id, cleared);
    }
}
