public class BoosterData
{
    public BoosterInfo Speed { get; private set; } = new(0, false);
    public BoosterInfo Protection { get; private set; } = new(0, false);
    public BoosterInfo Weapon { get; private set; } = new(0, false);
    public BoosterInfo Damage { get; private set; } = new(0, false);

    public void ActivateBooster(float duration, BoosterType type)
    {
        switch (type)
        {
            case BoosterType.Speed:
                Speed = AddBoosterTime(Speed, duration);
                break;
            case BoosterType.Protection:
                Protection = AddBoosterTime(Protection, duration);
                break;
            case BoosterType.Weapon:
                Weapon = AddBoosterTime(Weapon, duration);
                break;
            case BoosterType.Damage:
                Damage = AddBoosterTime(Damage, duration);
                break;
        }
    }

    public void UpdateBoostersDuration(float deltaTime)
    {
        Speed = UpdateBooster(Speed, deltaTime);
        Protection = UpdateBooster(Protection, deltaTime);
        Weapon = UpdateBooster(Weapon, deltaTime);
        Damage = UpdateBooster(Damage, deltaTime);
    }

    private BoosterInfo UpdateBooster(BoosterInfo source, float deltaTime)
    {
        if (source.IsActive == false)
            return source;
        
        source.Duration -= deltaTime;

        if (source.Duration <= 0)
            source = DisableBooster(source);

        return source;
    }

    private BoosterInfo DisableBooster(BoosterInfo source)
    {
        source.IsActive = false;
        source.Duration = 0;

        return source;
    }

    private BoosterInfo AddBoosterTime(BoosterInfo source, float duration)
    {
        source.IsActive = true;
        source.Duration += duration;

        return source;
    }
}