using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public CharacterStatHandler characterStatHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one DataPersistenceManager in the scene!");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        //Debug.LogError(Application.persistentDataPath + " / " + fileName);
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        dataHandler.Save(this.gameData);
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No Save Data Found. Initializing From Default Values.");
            NewGame();
        }

        foreach (IDataPersistence dpo in dataPersistenceObjects)
        {
            dpo.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        Debug.Log("DPM Recieved Save Call");
        characterStatHandler.healCharacters();
        foreach (IDataPersistence dpo in dataPersistenceObjects)
        {
            Debug.Log("Saving object: " + dpo);
            dpo.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
