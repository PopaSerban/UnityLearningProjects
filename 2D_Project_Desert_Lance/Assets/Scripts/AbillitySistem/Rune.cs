using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum RuneType
{
    RightArmRune,
    LeftArmRune,
    RightLegRune,
    LeftLegRune,
    ChestRune,
    HeadRune
}

public class Rune : ScriptableObject
{
    [SerializeField] protected Image _image;
    [SerializeField] protected List<Abillity> _abillities = new List<Abillity>();
    [SerializeField] protected RuneType _runeType;

    public RuneType RuneType{
        get{ return _runeType;}
        set{ _runeType = value;}
    }

    StringBuilder stringBuilder = new StringBuilder();

    public virtual string GetRuneType()
    {
        return _runeType.ToString();
    }

     public virtual string GetDescription()
    {
        stringBuilder.Length = 0;

        foreach (Abillity effect in _abillities)
        {
            stringBuilder.AppendLine(effect.GetDescription());
        }
        return stringBuilder.ToString();
    }

    

}
