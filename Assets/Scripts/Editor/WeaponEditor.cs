using UnityEditor;
using Weapon;

[CanEditMultipleObjects]
[CustomEditor(typeof(WeaponSettings))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var settings = target as WeaponSettings;

        if (settings.IsShootableWeapon == true)
        {
            settings.AmmoAmount = EditorGUILayout.IntField("Ammo amount:", settings.AmmoAmount);
            settings.ReloadDelay = EditorGUILayout.FloatField("Reload delay:", settings.ReloadDelay);
        }
    }
}