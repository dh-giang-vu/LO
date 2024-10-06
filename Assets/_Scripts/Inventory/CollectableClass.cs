using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Collectable Class", menuName = "Item/Collectable")]
public class CollectableClass : ItemClass
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
