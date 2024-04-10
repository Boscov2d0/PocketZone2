using UnityEngine;

[CreateAssetMenu(menuName = "Managers/" + nameof(ItemManager), fileName = nameof(ItemManager))]
public class ItemManager : ScriptableObject, ISelectable
{
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public int MaxCount;
    [SerializeField] public Sprite IconSprite;
    [SerializeField] public ItemClasses Class;
    [SerializeField] public GameObject Prefab;
    [HideInInspector] public int Count;

    string ISelectable.Name { get => Name; set => throw new System.NotImplementedException(); }
    string ISelectable.Description { get => Description; set => throw new System.NotImplementedException(); }
    Sprite ISelectable.IconSprite { get => IconSprite; set => throw new System.NotImplementedException(); }
    ItemClasses ISelectable.Class { get => Class; set => throw new System.NotImplementedException(); }
    int ISelectable.Count { get => Count; set => throw new System.NotImplementedException(); }
}