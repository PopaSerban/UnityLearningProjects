using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[SerializeField]
public enum AbilltiyType
{
    Passive,
    Triggered,
    AbillityUnlock
}

[CreateAssetMenu(menuName = "Abillities/Abillity")]
public class Abillity : ScriptableObject
{
    [SerializeField] protected Image _spriteImage;
    [SerializeField] protected AbilltiyType _type;

    protected StringBuilder stringBuilder = new StringBuilder();


    public virtual string GetDescription()
    {
        return "";
    }
    
    public virtual string GetAbillityType()
    {
        return _type.ToString();
    }



}
