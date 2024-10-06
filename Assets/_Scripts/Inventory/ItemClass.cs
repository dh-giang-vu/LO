using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public int quantity;
    public GameObject model;


    public  override abstract String ToString();
}
