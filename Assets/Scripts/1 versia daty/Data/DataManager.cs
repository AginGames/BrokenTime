using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;
    [Header("File Strorage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IData> dataObjects;
    private SaveSystem saveSystem; 

    public static DataManager instance { get; private set; }

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;

        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.saveSystem = new SaveSystem(Application.persistentDataPath, fileName);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataObjects = FindAllDataObjects();
        LoadGame();
    }
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = saveSystem.Load();

        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if(gameData == null)
        {
            Debug.Log("Nie znaleziono potrzebnych danych. Trzeba wystarytowaæ now¹ grê");
            return;
        }

        foreach (IData dataObj in dataObjects)
        {
            dataObj.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.LogWarning("Nie znaleziono potrzebnych danych. Trzeba wystarytowaæ now¹ grê");
            return;
        }
        foreach(IData dataObj in dataObjects)
        {
            dataObj.SaveData(ref gameData);
        }


        saveSystem.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IData> FindAllDataObjects()
    {
        IEnumerable<IData> dataObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IData>();

        return new List<IData>(dataObjects);
    }
    public bool HasGameData()
    {
        return gameData != null;
    }
}