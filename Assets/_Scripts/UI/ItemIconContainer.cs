﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconContainer : MonoBehaviour
{  
    public List<GameObject> iconArray;
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach(GameObject obj in iconArray){
            obj.gameObject.GetComponent<IconHandler>().isActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
