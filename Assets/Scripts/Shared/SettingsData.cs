using System;

[Serializable]
public class SettingsData
{
    public VolumeData Volume;

    public SettingsData()
    {
        Volume = new VolumeData(1, 1);
    }

    public void SaveData(float musicVolume, float vfxVolume)
    {
        Volume = new VolumeData(musicVolume, vfxVolume);
    }
    
    [Serializable]
    public struct VolumeData
    {
        public float Music;
        public float Vfx;

        public VolumeData(float music, float vfx)
        {
            Music = music;
            Vfx = vfx;
        }
    }
}