public struct WeaponBooster : IBooster
{
    public float ReducingReloadDelay { get; set; }
    public float ReducingAttackDelay { get; set; }
    public float Duration { get; set; }
    public bool IsActive { get; set; }
}