using Zenject;

public class WeaponBooster : Booster
{
    protected override void SetBoosterType()
    {
        _type = BoosterType.Weapon;
    }
    
    public class Pool : MonoMemoryPool<Booster>
    {
    }
}