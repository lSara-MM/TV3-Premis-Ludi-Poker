using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeBehaviour : MonoBehaviour
{
    public GenerateData csGenerateData;
    public UnityEvent functionSelected;

    // Start is called before the first frame update
    void Start()
    {
        csGenerateData = GameObject.FindWithTag("Data").GetComponent<GenerateData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Llama a la función seleccionada
    public void ExecuteSelectedFunction()
    {
        functionSelected.Invoke();
    }

    public void UpgradeValidation()
    {
        Debug.Log("UpgradeValidation");
    }

    public void UpgradeSameCard()
    {
        Debug.Log("UpgradeSameCard");
    }

    public void MorePlays()
    {
        Debug.Log("MorePlays");
    }

    public void MoreDiscards()
    {
        Debug.Log("MoreDiscards");
    }
}
