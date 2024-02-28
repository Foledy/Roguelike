using Zenject;

public class SpeedBooster : Booster
{
    protected override void SetBoosterType()
    {
        _type = BoosterType.Speed;
    }
    
    public class Pool : MonoMemoryPool<Booster>
    {
    }
}