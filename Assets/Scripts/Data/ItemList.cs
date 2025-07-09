using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Item/ItemList")]
public class ItemList : ScriptableObject
{
    public List<Item> AllItemList = new List<Item>();
}

[Serializable]
public class Item 
{
    public int id;
    public string Name;
    public ItemType itemtype;
    public int maxLevel;
    public List<Property> levelPropertyList;
    public Sprite icon;
    public GameObject objectPrefab;
}

public enum ItemType
{
    Weapon,
    Consumable
}

[Serializable]
public class Property
{
    public int level;
    public int attack;
    public float interval;
}