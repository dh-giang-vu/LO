using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "new Decor Class" , menuName = "Item/Decoration")]
public class DecorClass : ItemClass
{


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
