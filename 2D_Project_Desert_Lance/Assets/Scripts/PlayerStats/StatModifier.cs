
public enum StatModifierType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300
}
public class StatModifier 
{
    public readonly float value;
    public readonly StatModifierType statModifierType;
    public readonly int order;
    public readonly object source;

    //Base Constructor
    public StatModifier(float _value, StatModifierType _statModifierType, int _order, object _source)
    {
        value = _value;
        statModifierType = _statModifierType;
        order = _order;
        source = _source;

    }

    //Constructors
    public StatModifier(float _value, StatModifierType _type): this(_value,_type,(int)_type, null){}

    public StatModifier(float _value, StatModifierType _type, int _order): this(_value,_type,_order, null){}

    public StatModifier(float _value, StatModifierType _type, object _source): this(_value,_type,(int)_type,_source){}

}
