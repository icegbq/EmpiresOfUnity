﻿using UnityEngine;
using System.Collections;

public class SellectorSqript : MonoBehaviour {

    
    public UnitGroup group;
    public Bounds SellectionBounds
    {
        get { return gameObject.collider.bounds; }
    }

	void Start () 
    {
        group = ScriptableObject.CreateInstance<UnitGroup>();
        group.ResetGroup();
        gameObject.collider.enabled = false;
	}

    public UnitGroup SnapSellection()
    {
        gameObject.collider.enabled = true;
        group.ResetGroup();
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Clickable"))
        {
            if (gameObject.collider.bounds.Contains(unit.transform.position)) group.BeginGroupFill(unit);
        }
        group.EndGroupFill();
        gameObject.collider.enabled = false;
        return group;
    }




    //void OnTriggerExit(Collider other)
    //{
    //    group.AddUnit(other.gameObject);
    //}

	void Update () 
    {
	
	}
}
