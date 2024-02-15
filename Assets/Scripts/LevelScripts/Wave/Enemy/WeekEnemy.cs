using Zenject;

public class WeekEnemy : Enemy
{
    public class Pool : MonoMemoryPool<Enemy>
    {
        
    }
}