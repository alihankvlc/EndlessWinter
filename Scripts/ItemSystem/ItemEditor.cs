#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CustomEditor(typeof(Item), true)]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            Item item = (Item)target;
            item.Type = SetItemType(item);
        }

        private ItemType SetItemType(Item item)
        {
            if (item is Food)
            {
                Food food = item as Food;
                food.ConsumableType = ConsumableType.Food;
                return ItemType.Consumable;
            }
            if (item is Drink)
            {
                Drink drink = item as Drink;
                drink.ConsumableType = ConsumableType.Drink;
                return ItemType.Consumable;
            }
            if (item is Medicine)
            {
                Medicine medicine = item as Medicine;
                medicine.ConsumableType = ConsumableType.Medicine;
                return ItemType.Consumable;
            }
            if (item is Melee)
            {
                Melee melee = item as Melee;
                melee.WeaponType = WeaponType.Melee;
                return ItemType.Weapon;
            }
            if (item is Firearms)
            {
                return ItemType.Weapon;
            }
            if (item is Ammunation)
            {
                return ItemType.Ammunation;
            }
            return ItemType.None;
        }
    }
}
#endif
