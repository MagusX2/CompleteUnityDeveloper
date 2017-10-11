using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    InkEngine.InkManager _InkMng;


    private bool _hasMirror = false;
    private bool _hasHairClip = false;
    private bool _isDressedUp = false;
    private bool _isFree = false;
    private bool _isTheEnd = false;

    public bool HasMirror{get { return _hasMirror;  } set  { _hasMirror = value; }  }
    public bool HasHairClip { get { return _hasHairClip; } set { _hasHairClip = value; } }
    public bool IsDressedUp { get { return _isDressedUp; } set { _isDressedUp = value; } }
    public bool IsTheEnd { get { return _isTheEnd; } set { _isTheEnd = value; } }
    public bool IsFree { get { return _isFree; } set { _isFree = value; } }

    [SerializeField]
    public Text _textInventory;

    [SerializeField]
    public Text _textStatus;

    // Use this for initialization
    void Start () {
        _InkMng = GetComponent<InkEngine.InkManager>();
	}
	
	// Update is called once per frame
	void Update () {
        ClearInvStat();
        PrintInventory();
        PrintStatus();
        if (IsTheEnd) TheEnd();
    }

    void PrintInventory()
    {
        if (HasMirror) AddInventoryItem("Mirror\n");
        if (HasHairClip) AddInventoryItem("Hair Clip\n");
    }

    void AddInventoryItem(string itemName)
    {
        _textInventory.text += itemName;
    }

    void PrintStatus()
    {
        if (IsDressedUp) AddStatus("Dressed - Cleaner\n\n");
        if (IsFree) AddStatus("Free!!! :D");
    }

    void AddStatus(string statusName)
    {
        _textStatus.text += statusName;
    }

    void TheEnd()
    {
        IsTheEnd = false;
       
        // add effects, like an ending, etc.
    }

    public void ClearInvStat()
    {
        _textInventory.text = "";
        _textStatus.text = "";
    }
}
