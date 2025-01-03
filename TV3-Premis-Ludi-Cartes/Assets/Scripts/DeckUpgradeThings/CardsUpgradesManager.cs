using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardsUpgradesManager : MonoBehaviour
{
    public List<GameObject> upgradesPrefabs = new List<GameObject>();

    public SwitchScene cs_SwitchScene;
    public Toggle selectedUpgrade;
    public List<Toggle> toggleList = new List<Toggle>();

    public GameObject warningText;

    // Start is called before the first frame update
    void Start()
    {
        List<int> uniqueNumbers = new List<int>();

        // Generate unique random numbers
        while (uniqueNumbers.Count < 3)
        {
            int randomNumber = Random.Range(0, 4);

            // Add the number if it hasn�t been added before
            if (!uniqueNumbers.Contains(randomNumber))
            {
                uniqueNumbers.Add(randomNumber);
            }
        }

        for (int i = 0; i < upgradesPrefabs.Count; i++) 
        {
            upgradesPrefabs[i].transform.GetChild(uniqueNumbers[i]).gameObject.SetActive(true);
            toggleList.Add(upgradesPrefabs[i].transform.GetChild(uniqueNumbers[i]).gameObject.GetComponent<Toggle>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (selectedUpgrade != null)
            {
                selectedUpgrade.GetComponent<UpgradeBehaviour>().ExecuteSelectedFunction();
            }
        }
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
