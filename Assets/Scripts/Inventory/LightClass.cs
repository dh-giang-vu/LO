using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "new Light Class" , menuName = "Item/Light")]
public class LightClass : ItemClass
{
    private double radius;
    private double duration;
    

    public override String ToString()
    {
       return this.itemName + " : " + this.quantity;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
