using UnityEngine;

[CreateAssetMenu(menuName = "Managers/" + nameof(WeaponManager), fileName = nameof(WeaponManager))]
public class WeaponManager : ItemManager
{
    [SerializeField] public float Damage;
    [SerializeField] public float Distance;
    [SerializeField] public float DelayTime;
    [SerializeField] public float ReloadTime;
    [SerializeField] public int MagazineSize;
    [SerializeField] public GameObject BulletPrefab;
    [HideInInspector] public int CountPatronsInMagazine;
}