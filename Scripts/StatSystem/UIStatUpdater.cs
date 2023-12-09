namespace EndlessWinter.Stat
{
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;

    [System.Serializable]
    /*Yeniden düzenlenecek....*/
    public class UIStatUpdater
    {
        #region Variables
        public Stat Stat;
        public Slider Slider;
        public Image[] StatArrowImageArray;
        public Image FillAreaImage;
        private int m_PreviousStatNotifyRate;
        private int m_ArrowRate;

        private bool m_ReverseArrayExecuted;
        private Sequence m_Sequence;

        public const int FIRST_LEVEL = 1;
        public const int SECOND_LEVEL = 2;
        public const int THIRD_LEVEL = 3;
        #endregion
        #region Property
        public int GetDisplayCount
        {
            get => m_ArrowRate;
            set
            {
                if (ShouldUpdateArrowRate(value))
                {
                    if (Stat.Type == StatType.Stamina) { return; }

                    m_ArrowRate = value;
                    m_PreviousStatNotifyRate = m_ArrowRate;
                    RestartArrowAnimation(value);
                }
            }
        }
        #endregion
        #region Funcs
        public void UpdateUI(float param)
        {
            Slider.maxValue = Stat.MaxValue;
            Slider.value = Stat.CurrentValue;

            if (Stat.Type == StatType.Stamina)
                Slider.transform.DOScale(Stat.CurrentValue < Stat.MaxValue ? Vector3.one : Vector3.zero, 1);
            else
                AnimateStatChange(Stat.StatChangedAmount);


            SetFillRectColors();
            FillAreaImage.color = Stat.CurrentValue <= 0 ? Color.red : Color.white;
        }
        private void AnimateStatChange(float value)
        {
            if (IsValueInRange(value, -2, 0)) GetDisplayCount = -FIRST_LEVEL;
            else if (IsValueInRange(value, -5, -2)) GetDisplayCount = -SECOND_LEVEL;
            else if (IsValueInRange(value, float.MinValue, -5)) GetDisplayCount = -THIRD_LEVEL;
            else if (IsValueInRange(value, 0.05f, 2f)) GetDisplayCount = FIRST_LEVEL;
            else if (IsValueInRange(value, 2f, 5)) GetDisplayCount = SECOND_LEVEL;
            else if (IsValueInRange(value, 5, float.MaxValue)) GetDisplayCount = THIRD_LEVEL;
        }

        private bool IsValueInRange(float value, float minValue, float maxValue)
            => value >= minValue && value < maxValue;
        private bool ShouldUpdateArrowRate(int value)
            => m_PreviousStatNotifyRate != value || m_ArrowRate == 0;

        private void SetFillRectColors()
        {
            Color targetColor = (Slider.value / Slider.maxValue < 0.25f) ?
                Stat.ThresholdColor : Stat.CustomColor;

            Image sliderFillRectImage = Slider.fillRect.GetComponent<Image>();
            sliderFillRectImage.color = targetColor;
        }
        private void InitializeArrows(int rate)
        {
            for (int i = 0; i < StatArrowImageArray.Length; i++)
                SetArrowProperties(StatArrowImageArray[i], i < rate);
        }
        private void RestartArrowAnimation(int rate)
        {
            ReverseStatArrowsIfNeeded(rate);
            InitializeArrows(rate);
            DeactivateAllArrows();
            ActivateSpecificArrows(rate);
            KillExistingSequence();
            StartArrowAnimation(rate);
        }
        private void SetArrowProperties(Image arrow, bool isActive)
        {
            float alpha = isActive ? 0.5f : 0f;
            SetRotationAndAlpha(arrow.rectTransform, isActive);
            SetAlpha(arrow, alpha);
        }
        private void SetRotationAndAlpha(RectTransform rectTransform, bool isActive)
        {
            rectTransform.rotation = Quaternion.Euler(0, 0, isActive ? 180 : 0);
        }
        private void StartArrowAnimation(int rate)
        {
            if (m_Sequence != null && m_Sequence.IsActive())
            {
                m_Sequence.Pause();
                ResetArrows();
            }
            m_Sequence = DOTween.Sequence(rate);

            for (int i = 0; i < Mathf.Abs(rate); i++)
                m_Sequence.Append(StatArrowImageArray[i].DOFade(1f, Stat.FadeInDuration));

            m_Sequence.AppendInterval(Stat.Delay);

            for (int i = 0; i < Mathf.Abs(rate); i++)
                m_Sequence.Append(StatArrowImageArray[i].DOFade(0.5f, Stat.FadeOutDuration)).OnComplete(() =>
                {
                    for (int i = 0; i < Mathf.Abs(rate); i++)
                        StatArrowImageArray[i].transform.DOScale(Vector3.zero, 1).OnComplete(() =>
                          {
                              DeactivateAllArrows();
                              m_ArrowRate = 0;
                          });
                });

            m_Sequence.AppendInterval(Stat.Delay);
        }
        private void ResetArrows()
        {
            foreach (Image arrow in StatArrowImageArray)
                SetArrowProperties(arrow, false);
        }
        private void DeactivateAllArrows()
        {
            foreach (Image arrow in StatArrowImageArray)
                arrow.gameObject.SetActive(false);
        }
        private void ActivateSpecificArrows(int rate)
        {
            for (int i = 0; i < Mathf.Abs(rate); i++)
            {
                StatArrowImageArray[i].gameObject.SetActive(true);
                StatArrowImageArray[i].transform.DOScale(Vector3.one, 1.5f);
            }
        }
        private void KillExistingSequence()
        {
            if (m_Sequence != null)
                m_Sequence.Kill();
        }
        private void SetAlpha(Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
        private void ReverseStatArrowsIfNeeded(int rate)
        {
            if (!m_ReverseArrayExecuted && rate < 0)
            {
                System.Array.Reverse(StatArrowImageArray);
                m_ReverseArrayExecuted = true;
            }
            else if (m_ReverseArrayExecuted && rate > 0)
            {
                System.Array.Reverse(StatArrowImageArray);
                m_ReverseArrayExecuted = false;
            }
        }

    }
    #endregion
}