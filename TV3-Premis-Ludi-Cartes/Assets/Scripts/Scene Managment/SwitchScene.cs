using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] List<GameObject> notDestroys;

    // Start is called before the first frame update
    void Start()
    {
        notDestroys.Add(GameObject.FindWithTag("Data"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string text)
    {
        //Dont Destroy the gameobjects from the list, like the deck, as if we update it we want it to stay updated.
        for (int i = 0; i < notDestroys.Count; i++)
        {
            try 
            {
                DontDestroyOnLoad(notDestroys[i]); //IMPORTANT: Don't destroy on load only work for root game object
            }
            catch { }
            
        }

        //Change to the scene written on the Script
        SceneManager.LoadScene(text);
    }
}
