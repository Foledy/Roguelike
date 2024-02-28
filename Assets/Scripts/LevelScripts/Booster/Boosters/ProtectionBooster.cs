using Zenject;

public class ProtectionBooster : Booster
{
    protected override void SetBoosterType()
    {
        _type = BoosterType.Protection;
    }
    
    public class Pool : MonoMemoryPool<Booster>
    {
    }
}