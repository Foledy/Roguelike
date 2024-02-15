using Zenject;

public class GiantEnemy : Enemy
{
    public class Pool : MonoMemoryPool<Enemy>
    {
    }
}