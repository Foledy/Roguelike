using Zenject;

public class HealthBooster : Booster
{
    protected override void SetBoosterType()
    {
        _type = BoosterType.Health;
    }
    
    public class Pool : MonoMemoryPool<Booster>
    {
    }
}