using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace EndlessWinter.Stat
{
    [System.Serializable]
    /*Yeniden düzenlenecek....*/
    public class UIStatUpdater
    {
        #region Variables
        public Stat Stat;
        public Slider Slider;
        public Image[] StatArrow;

        private int m_PreviousStatNotifyRate;
        private int m_ArrowRate;

        private bool m_ReverseArrayExecuted;
        private Sequence m_Sequence;

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

        }
        private void AnimateStatChange(float value)
        {
            if (IsValueInRange(value, -1, 0)) GetDisplayCount = -1;
            else if (IsValueInRange(value, -3, -1)) GetDisplayCount = -2;
            else if (IsValueInRange(value, float.MinValue, -3)) GetDisplayCount = -3;
            else if (IsValueInRange(value, 0.1f, 1)) GetDisplayCount = 1;
            else if (IsValueInRange(value, 1, 3)) GetDisplayCount = 2;
            else if (IsValueInRange(value, 3, float.MaxValue)) GetDisplayCount = 3;
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
        private void SetArrowColors(Color color)
        {
            for (int i = 0; i < Mathf.Abs(m_ArrowRate); i++)
                StatArrow[i].DOColor(color, 1f);
        }
        private void InitializeArrows(int rate)
        {
            for (int i = 0; i < StatArrow.Length; i++)
                SetArrowProperties(StatArrow[i], i < rate);
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
                m_Sequence.Append(StatArrow[i].DOFade(1f, Stat.FadeInDuration));

            m_Sequence.AppendInterval(Stat.Delay);

            for (int i = 0; i < Mathf.Abs(rate); i++)
                m_Sequence.Append(StatArrow[i].DOFade(0.5f, Stat.FadeOutDuration)).OnComplete(() =>
                {
                    for (int i = 0; i < Mathf.Abs(rate); i++)
                        StatArrow[i].transform.DOScale(Vector3.zero, 1).OnComplete(() =>
                          {
                              DeactivateAllArrows();
                              m_ArrowRate = 0;
                          });
                });

            m_Sequence.AppendInterval(Stat.Delay);
        }
        private void ResetArrows()
        {
            foreach (Image arrow in StatArrow)
                SetArrowProperties(arrow, false);
        }
        private void DeactivateAllArrows()
        {
            foreach (Image arrow in StatArrow)
                arrow.gameObject.SetActive(false);
        }
        private void ActivateSpecificArrows(int rate)
        {
            for (int i = 0; i < Mathf.Abs(rate); i++)
            {
                StatArrow[i].gameObject.SetActive(true);
                StatArrow[i].transform.DOScale(Vector3.one, 1.5f);
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
                System.Array.Reverse(StatArrow);
                m_ReverseArrayExecuted = true;
                SetArrowColors(Stat.DecreaseArrowColor);
            }
            else if (m_ReverseArrayExecuted && rate > 0)
            {
                System.Array.Reverse(StatArrow);
                m_ReverseArrayExecuted = false;
                SetArrowColors(Stat.IncreaseArrowColor);
            }
        }

    }
    #endregion
}