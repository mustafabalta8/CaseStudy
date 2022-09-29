using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Simsoft.CaseStudyTabDragToSort
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] CanvasGroup myCanvasGroup;

        [SerializeField] float fadeTime = 1;
        public void FadeInPanel(CanvasGroup canvasGroup)
        {           
            myCanvasGroup = canvasGroup;
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, fadeTime)
                .OnComplete(FadeOutPanel);

        }
        public void FadeOutPanel() 
        {
            myCanvasGroup.alpha = 1;
            myCanvasGroup.DOFade(0, fadeTime*2);

        }
    }
}
