using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace EndlessWinter.Item
{
    public abstract class Item : ScriptableObject
    {
        #region Variables
        [Header("General Settings")]
        [SerializeField] internal ItemType Type;
        [SerializeField] internal RarityType Rarity;
        [SerializeField] internal Sprite Icon;
        [SerializeField] internal GameObject Prefab;
        [SerializeField] internal int Id;
        [SerializeField] internal string DisplayName;
        [SerializeField] internal float Weight;
        [SerializeField] internal float Durability;
        [SerializeField] internal bool IsStackable;
        [SerializeField, Multiline] internal string Description; 
        #endregion


        /* Medicine type i�in Hastal�klar olacak ve her medicine type i�in
* hastal��� tedavi edebilecek �zelli�i olacak...  */

        /* Bozuk g�dalar i�in do�abilecek hastal�klar... */
    }
}

