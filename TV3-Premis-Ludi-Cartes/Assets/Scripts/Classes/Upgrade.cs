using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UPGRADE_RARITY //Eric: Maybe it would be a good idea to add a MAX value to be able to iterate a for of this enum in a generic way
{
    COMMON,
    RARE,
    LEGENDARY
}

[System.Serializable]
public class Upgrade
{
    public int id;
    public string title;
    public string description;
    public UPGRADE_RARITY rarity;
    int currentLvl;

    public Upgrade() { }

    public Upgrade(string description, UPGRADE_RARITY rarity)
    {
        this.description = description;
        this.rarity = rarity;
    }

    void Apply()
    {

    }
}
