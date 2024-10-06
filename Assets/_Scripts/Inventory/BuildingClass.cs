using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "new Building Class" , menuName = "Item/Building")]
public class BuildingClass : ItemClass
{
    private double radius;
    private double duration;


    public override string ToString()
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
