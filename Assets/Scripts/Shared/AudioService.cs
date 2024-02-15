using System;

public class AudioService : IAudioService
{
    private SettingsData _data;
    private IDataService _dataService;

    public void Init(IDataService dataService)
    {
        _dataService = dataService;
        
        _dataService.Load(data => _data = data);
    }

    public void SetMusicVolume(float value)
    {
        if (value is < 0 or > 1)
            throw new ArgumentException("[Audio Service] music volume value can`t be lower than 0!");

        _data.Volume.Music = value;
        
        _dataService.Save(_data);
    }

    public void SetVfxVolume(float value)
    {
        if (value is < 0 or > 1)
            throw new ArgumentException("[Audio Service] vfx volume value can`t be lower than 0!");

        _data.Volume.Vfx = value;
        
        _dataService.Save(_data);
    }

    public float GetMusicVolume() => _data.Volume.Music;

    public float GetVfxVolume() => _data.Volume.Vfx;
}