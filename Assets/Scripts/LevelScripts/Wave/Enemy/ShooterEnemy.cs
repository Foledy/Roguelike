using Zenject;

public class ShooterEnemy : Enemy
{
    public class Pool : MonoMemoryPool<Enemy>
    {
        
    }
}