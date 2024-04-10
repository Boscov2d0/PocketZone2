using UnityEngine;

[CreateAssetMenu(menuName = "Managers/" + nameof(NPCManager), fileName = nameof(NPCManager))]
public class NPCManager : ScriptableObject
{
    [SerializeField] public int HP;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public float AttackRange;
    [SerializeField] public float PatrolDistance;
    [SerializeField] public float PatrolSpeed;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public float AttackDistance;
    [SerializeField] public float Damage;
    [SerializeField] public float ReloadTime;

    public SubscriptionProperty<NPCStates> States = new SubscriptionProperty<NPCStates>();
    [HideInInspector] public Vector2 LookDirection;
}