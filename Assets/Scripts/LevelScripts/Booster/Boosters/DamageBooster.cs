﻿public struct DamageBooster : IBooster
{
    public float Duration { get; set; }
    public float Multiplier { get; set; }
    public bool IsActive { get; set; }
}