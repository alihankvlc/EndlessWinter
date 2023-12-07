using UnityEngine;


#region Other
public enum ViewMode
{
    ThirdPerson,
    FirstPerson
}
public enum RarityType
{
    None,
    Common = 75,
    Uncommon = 25,
    Rare = 15,
    Epic = 10,
    Legendary = 1
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
#endregion
#region Weather
public enum WeatherType
{
    None,
    Sunny,
    Cloudy,
    Snowy,
    Stormy,
    Foggy,
    Windy,
    Freezing,
    Tornado,
}
#endregion
