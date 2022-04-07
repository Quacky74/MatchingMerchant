using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private bool isLocked;
    public bool IsLocked => isLocked;
    [SerializeField] private EDropableType dropableType;
    public EDropableType DropableType => dropableType;
    [SerializeField] private List<Ball> AdjustentObjects;
    [SerializeField] private SpriteRenderer lockedGraphicSpriteRenderer;

    private void OnEnable()
    {
        AdjustentObjects = new List<Ball>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            Ball Adjus = other.gameObject.GetComponent<Ball>();
            Adjus.AddAdjustentObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            Ball adjustentObject = other.gameObject.GetComponent<Ball>();
            adjustentObject.RemoveAdjustentObject(this);
        }
    }


    private void AddAdjustentObject(Ball adjustentObject)
    {
        if (adjustentObject == this)
        {
            return;
        }
        
        
        if (AdjustentObjects.Contains(adjustentObject))
        {
            return;
        }
        
        AdjustentObjects.Add(adjustentObject);
        
        
    }


    private void RemoveAdjustentObject(Ball adjustentObject)
    {
        if (adjustentObject == this)
        {
            return;
        }
        
        
        if (!AdjustentObjects.Contains(adjustentObject))
        {
            return;
        }
        
        AdjustentObjects.Remove(adjustentObject);
    }

    public void UnlockObject()
    {
        lockedGraphicSpriteRenderer.gameObject.SetActive(false);
        isLocked = false;
    }

    private void OnDisable()
    {
        AdjustentObjects.Clear();
        AdjustentObjects = null;

    }
}
