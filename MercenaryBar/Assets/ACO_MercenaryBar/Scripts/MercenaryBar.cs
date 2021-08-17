using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class MercenaryBar : MonoBehaviour
{
    [SerializeField] private Sprite greyHelmetSprite;
    [SerializeField] private Sprite redHelmetSprite;

    private RectTransform barTemplate;
    private RectTransform helmetTemplate;

    private float fillValueMax;
    private float fillValue;
    private float fillValuePerBar;
    private List<Bar> barList;

    private void Awake()
    {
        barTemplate = transform.Find("barTemplate").GetComponent<RectTransform>();
        helmetTemplate = transform.Find("helmetTemplate").GetComponent<RectTransform>();

        barTemplate.gameObject.SetActive(false);
        helmetTemplate.gameObject.SetActive(false);


        Setup(100f,5);

        //SetFillValue(50f);
        

        float fillAmount = 0f;

        FunctionUpdater.Create(()=> {
            fillAmount += Time.deltaTime * 100f / 4f;
            SetFillValue(fillAmount);
        });
    }

    private void Setup(float fillValueMax, int barAmount)
    {
        this.fillValueMax = fillValueMax;
        fillValue = 0f;
        fillValuePerBar = fillValueMax / barAmount;

        barList = new List<Bar>();
        for (int i = 0; i < barAmount; i++)
        {
            Vector2 barSize = new Vector2(70, 18);
            float barSizeOffset = 10f;
            Vector2 barAnchoredPosition = new Vector2((barSize.x+barSizeOffset) * i, 0);
            Bar bar = CreateBar(barAnchoredPosition,barSize);
            barList.Add(bar);
        }
    }

    private Bar CreateBar(Vector2 anchoredPosition,Vector2 size) 
    {
        RectTransform barRectTransform = Instantiate(barTemplate, transform);
        barRectTransform.anchoredPosition = anchoredPosition;
        barRectTransform.gameObject.SetActive(true);
        barRectTransform.sizeDelta = new Vector2(size.x - 10f,size.y);
        barRectTransform.SetAsFirstSibling();

        RectTransform helmetRectTransform = Instantiate(helmetTemplate, transform);
        helmetRectTransform.anchoredPosition = anchoredPosition + new Vector2(size.x, 0);
        helmetRectTransform.gameObject.SetActive(true);

        Bar bar = new Bar(helmetRectTransform, barRectTransform, greyHelmetSprite, redHelmetSprite);

        return bar;
    }


    private void SetFillValue(float fillValue)
    {
        this.fillValue = fillValue;

        for (int i = 0; i < barList.Count; i++)
        {
            Bar bar = barList[i];
            float barValueMin = i * fillValuePerBar;
            float barValueMax = (i + 1) * fillValuePerBar;
            if(fillValue <= barValueMin)
            {
                bar.SetFillAmount(0f);
            }
            else
            {
                if(fillValue >= barValueMax)
                {
                    bar.SetFillAmount(1f);
                }
                else
                {
                    float barFillAmount = (fillValue - barValueMin) / (barValueMax - barValueMin);
                    bar.SetFillAmount(barFillAmount);
                }
            }
        }
    }
    //Represents a single bar logic
    public class Bar
    {
        private Sprite greyHelmetSprite;
        private Sprite redHelmetSprite;

        private RectTransform barRectTransform;
        private RectTransform helmetRectTransform;

        private Image barFilledImage;
        private Image helmetImage;


        public Bar(RectTransform helmetRectTransform, RectTransform barRectTransform,Sprite greyHelmetSprite ,Sprite redHelmetSprite)
        {
            this.barRectTransform = barRectTransform;
            this.helmetRectTransform = helmetRectTransform;
            this.greyHelmetSprite = greyHelmetSprite;
            this.redHelmetSprite = redHelmetSprite;

            barFilledImage = barRectTransform.Find("barFilled").GetComponent<Image>();
            helmetImage = helmetRectTransform.GetComponent<Image>();

            SetFillAmount(0f);
        }

        public void SetFillAmount(float fillAmount)
        {
            barFilledImage.fillAmount = fillAmount;

            if (fillAmount >= 1f)
            {
                SetHelmetRed();
            }
            else
            {
                SetHelmetGrey();
            }
        }

        private void SetHelmetGrey()
        {
            helmetImage.sprite = greyHelmetSprite;
        }
        private void SetHelmetRed()
        {
            helmetImage.sprite = redHelmetSprite;
            helmetRectTransform.GetComponent<Animator>().SetTrigger("Popup");
        }
    }
}
