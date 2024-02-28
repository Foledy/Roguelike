using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(WeaponSettings))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var settings = target as WeaponSettings;

        if (settings.IsShootingWeapon == true)
        {
            settings.ReloadDelay = EditorGUILayout.FloatField("Reload delay:", settings.ReloadDelay);
            settings.AmmoAmount = EditorGUILayout.IntField("Ammo amount:", settings.AmmoAmount);
        }
    }
}