using System;

public interface IDataService
{
    void Load(Action<SettingsData> callback);
    void Save(SettingsData data, Action<bool> callback = null);
}