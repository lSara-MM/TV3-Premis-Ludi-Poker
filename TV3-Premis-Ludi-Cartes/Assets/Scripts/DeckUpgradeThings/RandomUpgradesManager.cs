using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<GameObject> UIobjects;
    [SerializeField] List<int> upgradeOptions; //Esto es un WIP, la idea seria cojer de una lista de structs con un ENUM para la funcion y rareza
    int op1 = -1, op2 = -1; //To not repeat type of upgrades

    void Start()
    {
        foreach(GameObject go in UIobjects) 
        {
            //Here would be a switch based on rarity
            go.GetComponent<Image>().color = Color.white;

            Random.Range(0, 10); //10 would be the enum size
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
