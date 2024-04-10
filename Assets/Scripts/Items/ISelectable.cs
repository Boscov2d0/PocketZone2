using UnityEngine;

public interface ISelectable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Sprite IconSprite { get; set; }
    public ItemClasses Class { get; set; }
    public int Count { get; set; }
}