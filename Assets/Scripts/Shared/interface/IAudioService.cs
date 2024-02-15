public interface IAudioService
{
    float GetVfxVolume();
    float GetMusicVolume();
    void SetMusicVolume(float value);
    void SetVfxVolume(float value);
    void Init(IDataService dataService);
}
