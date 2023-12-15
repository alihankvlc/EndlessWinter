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


        /* Medicine type için Hastalýklar olacak ve her medicine type için
* hastalýðý tedavi edebilecek özelliði olacak...  */

        /* Bozuk gýdalar için doðabilecek hastalýklar... */
    }
}

