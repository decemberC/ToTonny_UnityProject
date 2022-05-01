using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject nextPanel;
    public GameObject lastPanelItem;
    public float distentenceOfParent;
    public float distentenceOfChild;
    RectTransform nPpos;
    RectTransform pRpos;
    RectTransform ppRos;
    RectTransform lRos;
    int isOpenSign;
    public void Start()
    {
        if (nextPanel != null)
        {
            nPpos = nextPanel.GetComponent<RectTransform>();
        }
        lRos = lastPanelItem.GetComponent<RectTransform>();
        pRpos = panel.GetComponent<RectTransform>();
        ppRos= panel.transform.parent.GetComponent<RectTransform>();
        isOpenSign =0;
    }
    public void Update()
    {
        if (nextPanel != null)
        {
            LerpPanelPos();
        }
    }
    public void OpenP()
    {
        if (panel != null)
        {
            Animator anim = panel.GetComponent<Animator>();
            if (anim != null)
            {
                bool isOpen = anim.GetBool("open");
                isOpenSign = isOpen?0:1;
                anim.SetBool("open", !isOpen);

            }
        }
    }
    private void LerpPanelPos()
    {

        float dest = ppRos.anchoredPosition.y+pRpos.anchoredPosition.y+lRos.anchoredPosition.y*isOpenSign-distentenceOfParent-distentenceOfChild*isOpenSign;
        nPpos.anchoredPosition = new Vector2(nPpos.anchoredPosition.x, dest);
    }
}
