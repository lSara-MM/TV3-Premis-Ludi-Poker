using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardsUpgradesManager : MonoBehaviour
{
    public SwitchScene cs_SwitchScene;
    public Toggle selectedUpgrade;
    public List<Toggle> toggleList = new List<Toggle>();

    public GameObject warningText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AcceptUpgrade()
    {
        if (selectedUpgrade != null)
        {
            selectedUpgrade.GetComponent<UpgradeBehaviour>().ExecuteSelectedFunction();
            cs_SwitchScene.ChangeScene("MainScene");
        }
        else
        {
            warningText.SetActive(true);
        }
    }

    public void SelectUpgrade(Toggle toggle)
    {
        if (toggle.isOn) { selectedUpgrade = toggle; }
        else { selectedUpgrade = null; }

        // Unselect all the other toggles
        foreach (Toggle item in toggleList)
        {
            if (toggle != item)
                item.isOn = false;
        }
    }
}
