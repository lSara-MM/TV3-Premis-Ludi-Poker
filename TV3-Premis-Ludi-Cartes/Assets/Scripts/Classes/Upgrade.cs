using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UPGRADE_TYPES //Eric: Maybe it would be a good idea to add a MAX value to be able to iterate a for of this enum in a generic way
{
    COMMON,
    RARE,
    LEGENDARY
}

[System.Serializable]
public class Upgrade
{
    public string description;
    public UPGRADE_TYPES type;
    int currentLvl;

    public Upgrade() { }

    public Upgrade(string description, UPGRADE_TYPES type)
    {
        this.description = description;
        this.type = type;
    }

    void Apply()
    {

    }
}
