using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonUnactive : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<GameObject> gameObjects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideGO(int mode = 0) //Default activa y desactiva
    {
        
        foreach (GameObject go in gameObjects)
        {
            if(go.GetComponent<Button>() != null) 
            {
                go.GetComponent<Button>().interactable = (!go.GetComponent<Button>().interactable || (mode > 0)) && !(mode < 0);
            }
        }

    }
}
