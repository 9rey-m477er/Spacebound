using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    DataPersistenceManager dm;
    sceneLoader sl;
    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>();
        sl = GameObject.Find("Canvas").GetComponent<sceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        dm.NewGame();
        StartCoroutine(sl.LoadGame());
    }

    public void LoadGame()
    {
        dm.LoadGame();
        StartCoroutine(sl.LoadGame());
    }

}
