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
        //checks for duplicate DPMs
        if (instance != null)
        {
            Debug.LogError("Found more than one DataPersistenceManager in the scene!");
        }
        //sets the used DPM to this one
        instance = this;
    }

    private void Start()
    {
        //creates a new data handler and collects all of the Data Persistence objects
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        //Debug.LogError(Application.persistentDataPath + " / " + fileName);
        LoadGame();
    }

    public void NewGame() //starts a new game
    {
        this.gameData = new GameData();
        dataHandler.Save(this.gameData);
    }

    public void LoadGame() //loads a saved game
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null) //starts a new game if there isn't a saved game
        {
            Debug.Log("No Save Data Found. Initializing From Default Values.");
            NewGame();
        }


        //runs the load function in all saveable/loadable objects
        foreach (IDataPersistence dpo in dataPersistenceObjects)
        {
            dpo.LoadData(gameData);
        }

    }

    public void SaveGame() //saves the game
    {
        Debug.Log("DPM Recieved Save Call");

        //heals the player characters
        characterStatHandler.healCharacters();

        //runs the save function in all saveable/loadable objects
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

    private List<IDataPersistence> FindAllDataPersistenceObjects() //finds every object that inherits from IDataPersistence
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
