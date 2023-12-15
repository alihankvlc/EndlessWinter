using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EndlessWinter.Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private List<Sound> m_SoundList = new List<Sound>();

        private Dictionary<SoundType, Sound> m_SoundCache = new Dictionary<SoundType, Sound>();

        private void Start()
        {
            m_SoundCache = m_SoundList.ToDictionary(r => r.Type);
            EventManager.Instance.Subscribe<SoundType>("PlaySound", E_PlaySoundEffect);
        }
        private void E_PlaySoundEffect(SoundType soundType)
        {
            if (m_SoundCache.TryGetValue(soundType, out Sound existingSound))
                existingSound.AudioSource.Play();
        }

        private void OnDestroy()
            => EventManager.Instance.Unsubscribe<SoundType>("PlaySound", E_PlaySoundEffect);
    }
}