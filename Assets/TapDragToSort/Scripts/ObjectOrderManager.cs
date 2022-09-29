using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Simsoft.CaseStudyTabDragToSort
{
    public class ObjectOrderManager : Singleton<ObjectOrderManager>
    {
        [SerializeField]
        private List<Draggable> TopLeft,
        TopRight,
        BottomLeft,
        BottomRight;

        [SerializeField]
        private int TopLeftPlace,
        TopRightPlace,
        BottomLeftPlace,
        BottomRightPlace;

        [SerializeField] private CanvasGroup sectionText;
        [SerializeField] private CanvasGroup winText;
        [SerializeField] private int completedSectionAmount = 0;


        public void UpdateObjectCount(Place place, Place previousPlace, Draggable draggable)
        {
            UpdatePlace(place,1, draggable);
            UpdatePlace(previousPlace, -1, draggable);            
        }
        public void UpdateObjectCount(Place place, Draggable draggable)
        {
            UpdatePlace(place, 1, draggable);
        }

        private void UpdatePlace(Place place, int amount, Draggable draggable)
        {
            if(amount>0)
            switch (place)
            {
                case Place.TopLeft:
                    TopLeftPlace+= amount;
                    TopLeft.Add(draggable);
                    ControlSectionCompletionCondition(TopLeftPlace, TopLeft);
                    break;
                case Place.TopRight:
                    TopRightPlace += amount;
                    TopRight.Add(draggable);
                        ControlSectionCompletionCondition(TopRightPlace, TopRight);
                        break;
                case Place.BottomLeft:
                    BottomLeftPlace += amount;
                    BottomLeft.Add(draggable);
                        ControlSectionCompletionCondition(BottomLeftPlace, BottomLeft);
                        break;
                case Place.BottomRight:
                    BottomRightPlace += amount;
                    BottomRight.Add(draggable);
                        ControlSectionCompletionCondition(BottomRightPlace, BottomRight);
                        break;
                default:
                    break;
            }
            else
            switch (place)
            {
                case Place.TopLeft:
                    TopLeftPlace += amount;
                    TopLeft.Remove(draggable);                      
                    break;
                case Place.TopRight:
                    TopRightPlace += amount;
                    TopRight.Remove(draggable);
                    break;
                case Place.BottomLeft:
                    BottomLeftPlace += amount;
                    BottomLeft.Remove(draggable);
                    break;
                case Place.BottomRight:
                    BottomRightPlace += amount;
                    BottomRight.Remove(draggable);
                    break;
                default:
                    break;
            }
        }
        private void ControlSectionCompletionCondition(int amount, List<Draggable> slotSection)
        {
            if (amount >= 4)
            {
                int sameTypeAmount=0;
                Fruit type = slotSection[slotSection.Count-1].ObjectType;
                foreach (var item in slotSection)
                {
                    if(item.ObjectType != type)
                    {
                        return;
                    }
                    else
                    {
                        sameTypeAmount++;
                    }
                }
                if (sameTypeAmount >= 4)
                {                   
                    completedSectionAmount++;
                    CheckWinCondition();
                }
            }

        }
        private void CheckWinCondition()
        {
            if (completedSectionAmount >= 3)
            {
                UIManager.instance.FadeInPanel(winText);
                StartCoroutine(RestartLevel());
            }
            else
            {
                UIManager.instance.FadeInPanel(sectionText);
            }
        }

        private IEnumerator RestartLevel()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
