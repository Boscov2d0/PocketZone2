using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/" + nameof(GameMyManager), fileName = nameof(GameMyManager))]
public class GameMyManager : ScriptableObject
{
    [SerializeField] public List<Item> AllItemsList = new List<Item>();
    public SubscriptionProperty<UIStates> GameStates = new SubscriptionProperty<UIStates>();
    [HideInInspector] public ISelectable SelectedItem;
}