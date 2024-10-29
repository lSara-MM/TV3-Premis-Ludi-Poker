using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCredits : MonoBehaviour
{
    [SerializeField] RectTransform picture;
    [SerializeField] Camera screenCamera;
    float dt = 0;
    public float waitMove = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if(picture == null) 
        {
            picture = this.gameObject.GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (picture.anchoredPosition.y < 2000)
        {
            dt += Time.deltaTime;

            if (dt > waitMove)
            {
                picture.anchoredPosition = new Vector2(picture.anchoredPosition.x, picture.anchoredPosition.y + 20);
                dt = 0;
            }
        }
        
    }
}
