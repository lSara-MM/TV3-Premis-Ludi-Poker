using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] string sceneToChange;
    [SerializeField] List<GameObject> notDestroys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string text = "") //If no text its added it will change to the Scene on the script, but if you want to hardcode in a call the scene you can
    {
       //Dont Destroy the gameobjects from the list, like the deck, as if we update it we want it to stay updated.
        foreach (GameObject obj in notDestroys) 
        {
            DontDestroyOnLoad(obj);
        }

        //Change to the scene written on the Script
        SceneManager.LoadScene(sceneToChange);
    }
}
