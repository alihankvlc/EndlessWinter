#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Player.Stat
{
    [CustomEditor(typeof(Stat), true)]
    public class StatEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Stat stat = (Stat)target;
            GUILayout.Space(2f);

            if (!Application.isPlaying)
            {
                if (GUILayout.Button("Default Variables", GUILayout.Width(150), GUILayout.Height(50)))
                {
                    SetDefaultVariables(stat);
                    Debug.Log($"<color=cyan>Set as Default.</color>");
                }
                else if (GUILayout.Button("Reset Value", GUILayout.Width(150), GUILayout.Height(50)))
                {
                    stat.CurrentValue = 1;
                    Debug.Log($"<color=cyan>The Notify Rate</color> " +
                              $"<color=yellow>({stat.Type})</color>" +
                              $" Has Been Reset.");
                }
            }
            else
            {
                Debug.LogWarning("<color=green>Changes can only be made in editor mode.</color>");
            }
        }

        private void SetDefaultVariables(Stat stat)
        {
            stat.Type = GetStatType(stat);
            stat.CurrentValue = 100;
            stat.MaxValue = 100;
            stat.Inform = "Null";
            stat.CustomColor = Color.white;
            stat.ThresholdColor = Color.red;
            stat.FadeInDuration = 0.35f;
            stat.FadeOutDuration = 0.35f;
            stat.Delay = 0.15f;
        }
        private StatType GetStatType(Stat stat)
        {
            if (stat is Health) return StatType.Health;
            if (stat is Stamina) return StatType.Stamina;
            if (stat is Fatigue) return StatType.Fatigue;
            if (stat is Temperature) return StatType.Temperature;
            if (stat is Hunger) return StatType.Hunger;
            if (stat is Thirst) return StatType.Thirst;

            return StatType.None;
        }
    }
}
#endif