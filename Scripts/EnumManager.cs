namespace EndlessWinter
{
    using UnityEngine;

    #region Other||Player
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
    public enum MovementType
    {
        Walk,
        Run,
        Crouch,
        Jump
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
    #region WeatherSystem
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
    #region DiseaseSystem
    public enum InjuryType
    {
        Bruise,                     // Yara izi // Hayvan saldırıları vs // Psikolojik sağlık düşüşü /// Tedavisi => Bandaj
        Laceration,                 // Yırtık, derin kesik // Hayvan saldırıları vs // Tedavisi Bandaj
        Fracture,                   // Kırık // Level 3 seviyeden atlama // Tedavisi alter kullanımı
        Sprain,                     // Burkulma //Level 1 seviyeden atlama // Hareket kısıtlılığı Level 2 Fatigue // Tedavisi zaman
        Burn,                       // Yanık // Ateşe kafa atmak // Psikolejik sağlık + Health azalması // tedavisi zaman
        Concussion,                 // Sarsıntı // Açlıktan doğabilir // Psikolojik Sağlık + hareket kısıtlaması // tedavisi zaman
        Dislocation,                // Çıkık // Level 2 seviyeden atlama // Koşarsan ebeninkini görrüsn => Kırık // tedavisi zaman
        Poisoning,                  // Zehirlenme // Otu boku yemek // Level 2 // Belirli bir süre içerisinde azalan health // tedavisi antibiytoik
        Frostbite,                  // Donma // Anladın  // tedavisi Temperature stat yükseltmek
        Abrasion,                   // Sıyrık // Hayvanların öpücük alması // Psikolejik sağlık + level1 // herhangi bir tedavi gerektirmiyor
        Hemorrhage,                 // Kanama // Hayvan saldırsıı // Health azalması + Psikolojik sağlık level 2 // Tedavisi bandaj
        RadiationExposure,          // Radyasyon maruziyeti // çıkarabilirim görecezz
        PsychologicalTrauma,    // Psikolojik travma // Açlık + Susuzluk // psikolojik sağlığı noksanlığı // Aptal saptan eventler tüm statlarda düşüş
        Dysentery,                   // Güvenli olmayan suyu içmek // psikolojik sağlık + artı zamanla azalan su kaybı ve Fatigue stat artışı // antibiyotik  + zaman
        Hypothermia,                   // Frostbite'den sonra gerçekleşecek Temperature belli bir süre 0 kalırsa 
        Parasitic,                      // Otu boku yemek // Level 2 sağlık + açlık +susuzluk azalması.  // tedavisi susuzluğun ve açlığın 0 olmaması 

        //Düzenlenecek.
    }
    #endregion
}
