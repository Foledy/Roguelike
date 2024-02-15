public interface IAudioService
{
    float GetMusicVolume();
    float GetVfxVolume();
    void SetMusicVolume(float value);
    void SetVfxVolume(float value);
    void Init(IDataService dataService);
}
