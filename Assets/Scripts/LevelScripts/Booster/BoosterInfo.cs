public struct BoosterInfo
{
    public float Duration { get; set; }
    public bool IsActive { get; set; }

    public BoosterInfo(float duration, bool isActive)
    {
        Duration = duration;
        IsActive = isActive;
    }
}