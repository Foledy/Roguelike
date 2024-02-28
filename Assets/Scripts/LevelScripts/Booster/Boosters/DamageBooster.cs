using Zenject;

public class DamageBooster : Booster
{
    protected override void SetBoosterType()
    {
        _type = BoosterType.Damage;
    }
    
    public class Pool : MonoMemoryPool<Booster>
    {
    }
}