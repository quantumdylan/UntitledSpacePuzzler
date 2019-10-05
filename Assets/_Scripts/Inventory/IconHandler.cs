using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image img;
    public bool isActive = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
