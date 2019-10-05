using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KioskKeypad : MonoBehaviour
{
    [SerializeField] private string entry;
    [SerializeField] private int length;

    public string userEntry;

    void Awake(){

    }

    void Update(){
        if(userEntry.Length == length && checkInput()){
            GameObject.FindGameObjectWithTag("TestLamp").GetComponent<Light>().intensity = 1;
        }
    }

    public bool checkInput(){
        return true;
    }

    public void addEntry(string ch){
        userEntry += ch;
    }
}