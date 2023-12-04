using UnityEngine;

#region Other
public enum ViewMode
{
    ThirdPerson,
    FirstPerson
}
#endregion
#region StatSystem
public enum StatType
{
    None,
    Thirst,
    Hunger,
    Temperature,
    Fatigue,
    Health,
    Stamina
}
#endregion
#region ItemSystem
public enum ItemType
{
    None,
    Consumable,
    Weapon,
    Clothes,
    Ammunation
}
public enum ConsumableType
{
    None,
    Medicine,
    Food,
    Drink
}
public enum WeaponType
{
    None,
    Melee,
    Rifle,
    Pistol,
    Shotgun,
    Bow
}
public enum AmmunationType
{
    Ranged,
    Firearms
}
public enum ClothesType
{
    None,
    HeadWear,
    HandWear,
    BodyWear,
    LegWear,
    FeetWear,
}
public enum RarityTpye
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
#endregion
