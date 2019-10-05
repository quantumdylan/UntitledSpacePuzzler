using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items;
    private int lastCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(items.Count != lastCount){
            foreach(Item i in items){
                if(i.id == "blue_card"){
                    Debug.Log("Got blue keycard");
                }
            }
        }
    }

    public void addItem(Item item){
        items.Add(item);
    }
}
