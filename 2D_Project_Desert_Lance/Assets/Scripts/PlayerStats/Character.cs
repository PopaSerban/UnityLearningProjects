using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterUIManager _characterUiManager;
    public float FireRes;
    public float AirRes;
    public float WaterRes;
    public float EarthRes;
    [SerializeField] private Level level;
    // Start is called before the first frame update
    void Start()
    {
        level = new Level(1, OnLevelUp);
        _characterUiManager = UIManager.Instance.GetComponentInChildren<CharacterUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        FireRes = _characterUiManager.FireResistance.Value;
        AirRes = _characterUiManager.AirResistance.Value;
        WaterRes = _characterUiManager.WaterResistance.Value;
        EarthRes = _characterUiManager.EarthResistance.Value;
        //test add level call
        if( Input.GetKeyDown(KeyCode.L))
        {
            level.AddExp(50);
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("I Leveled up!");
    }
}
