using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] ShopItemVariable defaultPlayerSkin;

    private string dataContainerFilePath;

    [HideInInspector] public DataContainer latestData { get; private set; }

    private void Awake()
    {
        dataContainerFilePath = Application.persistentDataPath
           + "/dataContainer.dat";

        SetLatestData(LoadData());
    }

    public void SaveData(DataContainer dataContainer)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream = File.Open(dataContainerFilePath, FileMode.Create);

        bf.Serialize(stream, dataContainer);
        stream.Close();

        SetLatestData(dataContainer);
    }

    private void SetLatestData(DataContainer newDataContainer)
    {
        latestData = newDataContainer;
    }

    // Load data method is private because it's just used in the awake method.
    private DataContainer LoadData()
    {
        if (File.Exists(dataContainerFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = File.Open(dataContainerFilePath, FileMode.Open);

            DataContainer newDataContainer = (DataContainer)bf.Deserialize(stream);

            stream.Close();

            return newDataContainer;
        }

        // If file does not exist, then create a new save file.
        SaveData(GetInitialData());

        return LoadData();
    }


    private DataContainer GetInitialData()
    {
        // Create a new file.
        DataContainer dataContainer = new DataContainer();

        dataContainer.IsAudioOn = true;
        dataContainer.GemCount = 0;
        dataContainer.HighScore = 0;

        dataContainer.SelectedPlayerSkinGuid = defaultPlayerSkin.guid;

        dataContainer.BoughtShopItemsList = new List<DataContainer.ShopItemData>();
        dataContainer.BoughtShopItemsList.Add(
            new DataContainer.ShopItemData(defaultPlayerSkin.guid));

        return dataContainer;
    }
}

[System.Serializable]
public class DataContainer
{
    [System.Serializable]
    public class ShopItemData
    {
        public string Guid;

        public ShopItemData(string newGuid)
        {
            Guid = newGuid;
        }
    }

    public bool IsAudioOn;

    public int GemCount;
    public int HighScore;

    public string SelectedPlayerSkinGuid;

    public List<ShopItemData> BoughtShopItemsList = new List<ShopItemData>();
}