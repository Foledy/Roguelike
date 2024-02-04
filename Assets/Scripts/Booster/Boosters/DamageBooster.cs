public struct DamageBooster : IBooster
{
    public float Multiplier { get; set; }
    public float Duration { get; set; }
    public bool IsActive { get; set; }
}