using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconContainer : MonoBehaviour
{
    public List<Sprite> icons;
    public GameObject iconTemplate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void drawIcons(Item[] items){
        foreach(Item i in items){
            Instantiate(iconTemplate);
        }
    }
}
