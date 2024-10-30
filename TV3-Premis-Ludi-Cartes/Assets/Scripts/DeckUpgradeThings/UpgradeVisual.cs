using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Image icon;

    public void SetVisual(Upgrade upgrade, Sprite img)
    {
        title.text = upgrade.title;
        //description.text = upgrade.description;
        level.text = $"x{upgrade.currentLvl}";
        icon.sprite = img;
    }
}
