﻿using UnityEngine;
using System.Collections;



public abstract class UnitComponent : MonoBehaviour
{
    virtual public bool ComponentExtendsTheOptionalstateOrder
    {
        get { return false; }
    }
    public enum OPTIONS : int
    {
        Cancel=EnumProvider.ORDERSLIST.Cancel
    }
    abstract public string IDstring
    { get; }

    public UnitScript UNIT;
    private int ID;
    
    private System.Enum[] StateExtensions;

    public UnitComponent PflongeOnUnit()
    {
        UNIT = this.gameObject.GetComponent<UnitScript>();
            if (ComponentExtendsTheOptionalstateOrder)
            {
                int i = -1;
                StateExtensions = new System.Enum[System.Enum.GetNames(typeof(OPTIONS)).Length];
                foreach (System.Enum extension in System.Enum.GetValues(typeof(OPTIONS)))
                    StateExtensions[++i] = extension;
            }
            else
            {
                StateExtensions = new System.Enum[1];
                StateExtensions[0] = EnumProvider.ORDERSLIST.Cancel;
            }
            this.ID = this.gameObject.GetComponent<UnitScript>().Options.RegisterUnitComponent(this, StateExtensions);
            SignIn();

            
            return this;
    }

    protected virtual void SignIn()
    {
        UNIT.Options.PRIMARY_STATE_CHANGE += on_UnitStateChange;
    }
    protected virtual void SignOut()
    {
        UNIT.Options.PRIMARY_STATE_CHANGE -= on_UnitStateChange;
    }



    public UnitComponent PflongeOnUnit(System.Array newextensions)
    {
        StateExtensions = new System.Enum[newextensions.Length];
        UNIT = this.gameObject.GetComponent<UnitScript>();
        newextensions.CopyTo(StateExtensions, 0);
        this.ID = this.gameObject.GetComponent<UnitScript>().Options.RegisterUnitComponent(this, StateExtensions);
        SignIn();
        return this;
    }



    public void StlontshOff()
    {
        if (!this.ComponentExtendsTheOptionalstateOrder)
        {
            StateExtensions = new System.Enum[1];
            StateExtensions[0] = EnumProvider.ORDERSLIST.Cancel;

        }
        this.gameObject.GetComponent<UnitScript>().Options.UnRegister(this.ID, StateExtensions);
        SignOut();
    }

    abstract protected EnumProvider.ORDERSLIST on_UnitStateChange(EnumProvider.ORDERSLIST stateorder);


    void OnDestroy()
    {
        if (ComponentExtendsTheOptionalstateOrder)
            StlontshOff();
    }

    abstract public void DoUpdate();


    public class UnitComponentExeption : System.Exception
    {
        public struct UnitExeptionData
        {
            public int ObjectID;
            public string UnitComponentID;

            public UnitExeptionData(int oid, string ucid)
            {
                ObjectID = oid;
                UnitComponentID = ucid;
            }
        }

        UnitExeptionData ExeptionData;

        public UnitComponentExeption(int oid, string ucid)
        {
            this.ExeptionData = new UnitExeptionData(oid, ucid);
        }
        public override string Message
        {
            get
            {
                string parentObject = "";
                foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Units"))
                    if (unit.GetInstanceID() == this.ExeptionData.ObjectID)
                        parentObject = unit.name + " - ID: " + this.ExeptionData.ObjectID.ToString();

                return "UnitComponent-Exeption in:\n " + parentObject + "\nIn UnitComponent:\n" + ExeptionData.UnitComponentID;
            }
        }
    }
}
