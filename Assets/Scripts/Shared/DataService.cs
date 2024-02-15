using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

public class DataService : IDataService
{
    private string _filePath = Application.persistentDataPath + "/Roguelike.settings";

    public void Load(Action<SettingsData> callback)
    {
        if (string.IsNullOrEmpty(_filePath) == true)
        {
            throw new Exception("[IDataService] File path is null or empty!");
        }
        
        SettingsData result;
        
        if (File.Exists(_filePath) == false)
        {
            result = new SettingsData
            {
                Volume = new SettingsData.VolumeData
                {
                    Music = 1,
                    Vfx = 1
                }
            };
            
            callback?.Invoke(result);
            
            return;
        }

        var formatter = new BinaryFormatter();
        using var fileStream = new FileStream(_filePath, FileMode.Open);

        result = (SettingsData)formatter.Deserialize(fileStream);
        
        callback?.Invoke(result);
    }

    public void Save(SettingsData data, Action<bool> callback = null)
    {
        if (string.IsNullOrEmpty(_filePath) == true)
        {
            callback?.Invoke(false);
            
            throw new Exception("[IDataService] File path is null or empty!");
        }
        
        var formatter = new BinaryFormatter();
        using var fileStream = new FileStream(_filePath, FileMode.Create);
        var settings = new SettingsData();
                
        settings.SaveData(data.Volume.Music, data.Volume.Vfx);
                
        formatter.Serialize(fileStream, settings);
        
        callback?.Invoke(true);
    }
}