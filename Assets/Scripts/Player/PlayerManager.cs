using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/" + nameof(PlayerManager), fileName = nameof(PlayerManager))]
public class PlayerManager : ScriptableObject
{
    [SerializeField] public float HealthPoints;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public int BagSize;

    public SubscriptionProperty<PlayerStates> States = new SubscriptionProperty<PlayerStates>();
    public SubscriptionProperty<float> HP = new SubscriptionProperty<float>();
    public SubscriptionProperty<float> Horizontal = new SubscriptionProperty<float>();
    public SubscriptionProperty<float> Vertical = new SubscriptionProperty<float>();

    public List<ItemManager> Items = new List<ItemManager>();
    [HideInInspector] public Vector2 LookDirection;
    [HideInInspector] public WeaponManager CurrentWeapon;
    [HideInInspector] public ItemManager CurrentHelmet;
    [HideInInspector] public ItemManager CurrentJacket;
    [HideInInspector] public ItemManager CurrentPants;
    [HideInInspector] public Action PutOnItem;
}