using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : Joystick
{
    public bool showBackground = true;
    private CanvasGroup _backgroundCanvasGroup;
    public float MoveThreshold
    {
        get { return moveThreshold; }
        set { moveThreshold = Mathf.Abs(value); }
    }

    [SerializeField] private float moveThreshold = 1;


    private void Awake()
    {
        _backgroundCanvasGroup = background.GetComponent<CanvasGroup>();
    }

    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        // background.gameObject.SetActive(showBackground);
    }
    
    public void SetBackgroundTransparency(float alpha)
    {
        _backgroundCanvasGroup.alpha = alpha;
    }


    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }

        base.HandleInput(magnitude, normalised, radius, cam);
    }


    // public override void OnPointerDown(PointerEventData eventData)
    // {
    //     background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
    //     background.gameObject.SetActive(true);
    //     base.OnPointerDown(eventData);
    // }
    //
    // public override void OnPointerUp(PointerEventData eventData)
    // {
    //     background.gameObject.SetActive(false);
    //     base.OnPointerUp(eventData);
    // }

    // protected override void Start()
    // {
    //     base.Start();
    //     background.gameObject.SetActive(false);
    //
    //     // if (GameController.Instance.CpiTricks)
    //     // {
    //     // background.GetComponent<Image>().enabled = false;
    //     // handle.GetComponent<Image>().enabled = false;
    //     //   }
    // }
    //
    protected override void OnInputDown()
    {
        if (!EventSystem.current.currentSelectedGameObject)
        {
    
            background.anchoredPosition = ScreenPointToAnchoredPosition(Input.mousePosition);
            background.gameObject.SetActive(true);
            base.OnInputDown();
        }
    }
    
    //
    public override void Update()
    {
        background.gameObject.SetActive(isPressed);
        if (!EventSystem.current.currentSelectedGameObject)
        {
            base.Update();
            background.gameObject.SetActive(Input.GetMouseButton(0));
        }
    }

    //
    // protected override void OnInputUp()
    // {
    //     Debug.Log("ON_UP");
    //     // if (!EventSystem.current.currentSelectedGameObject)
    //     // {
    //     background.gameObject.SetActive(false);
    //     base.OnInputUp();
    //     // }
    // }
}