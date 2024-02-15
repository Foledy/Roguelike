using System;

public interface IDataService
{
    void Save(SettingsData data, Action<bool> callback = null);
    void Load(Action<SettingsData> callback);
}