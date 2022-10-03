using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionPanel : MonoBehaviour
{
    private bool _isOpened = false;
    public GameObject[] content;
    private Animator _anim;
    private int _curContent = 0;
    public EventSystem _eSys;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_eSys.currentSelectedGameObject == null && _isOpened)
        {
            //StartCoroutine(ClosePanelProcess());
            ClosePanel();
        }
    }

    private IEnumerator ClosePanelProcess()
    {
        _isOpened = false;
        yield return new WaitForSeconds(.3f);
        ClosePanel();
    }

    public void OpenPanel(int content_id)
    {
        if(!_isOpened)
        {
            HideContent();
            _curContent = content_id;
            _anim.Play("Open", 0);
        }
        else
        {
            HideContent();
            _curContent = content_id;
            ShowContent();
        }
    }

    public void ClosePanel()        
    {
        HideContent();
        _anim.Play("Close", 0);
    }

    public void HideContent()
    {
        content[_curContent].SetActive(false);
    }

    public void ShowContent()
    {        
        content[_curContent].SetActive(true);
    }

    public void SetOpenedTrue()
    {
        _isOpened = true;
    }

    public void SetOpenedFalse()
    {
        _isOpened = false;
    }
}
