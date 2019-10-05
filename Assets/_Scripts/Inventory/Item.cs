using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type{
        Keycard,
        Tape
    }

    public string id;
    public Type type;

    void removeFromWorld(){
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = new Vector3(0, -25, 0);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>().addItem(this);
            removeFromWorld();
        }
    }
}
