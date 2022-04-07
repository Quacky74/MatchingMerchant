using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Scrpits.Line_drawing
{
    public class DrawControler : MonoBehaviour
    {
        [SerializeField] private Line _linePrefab;
        [SerializeField] private EDropableType lineType;
        [SerializeField] private List<Ball> Balls;
        [SerializeField] private float minMatchDistance = 1.0f;
        [SerializeField] private int MinMatches = 3;
        public const float RESOLUTION = .1f;
        
        private Camera _cam;
        private Line _currentLine;
        private Vector2 _startingPoint;
        private bool CanDraw;
        
        void Start()
        {
            _cam = Camera.main;   
        }


        void Update() 
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                RaycastHit2D hit = Physics2D.Raycast(GetMouseScreenPos(), Vector2.zero);

                if (hit.collider == null)
                {
                    return;
                }
                
                if (hit.collider.GetComponent<Ball>())
                {
                    Ball newBall = hit.collider.GetComponent<Ball>();
                    SetLineType(newBall.DropableType);
                    CanDraw = true;

                }
            }
            
            if (CanDraw)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    CreateLine();
                }

                if (Mouse.current.leftButton.isPressed)
                {
                    UpdateLine();
                }

                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    FinishedLineDraw();
                }

            }
            
        }
        
        
        
        void CreateLine()
        {
            Balls = new List<Ball>();
            _currentLine = Instantiate(_linePrefab, GetMouseScreenPos(), Quaternion.identity);
            _currentLine.StartLine(GetMouseScreenPos());
            _currentLine.OnHitBall += AddBall;
            
            
            _startingPoint = GetMouseScreenPos();
            
        }

        void UpdateLine()
        {
            _currentLine.AdjustPoint(GetMouseScreenPos());
        }

        void FinishedLineDraw()
        {
            if (ValidateLine())
            {
                for (int index = 0; index < Balls.Count; index++)
                {
                    Destroy(Balls[index].gameObject);//TODO:change to use object pooling
                    Balls[index] = null;
                }
            }
            
            ResetLine();
        }

        bool ValidateLine()
        {
            int counter = 0;
            int listIndex = 0;
            
            foreach (var ball in Balls)
            {
                listIndex = listIndex < Balls.Count ? listIndex + 1 : Balls.Count - 1;
                if (listIndex == Balls.Count - 1)
                {
                    break;
                }
                
                if (Vector2.Distance(ball.transform.position, Balls[listIndex].transform.position) < minMatchDistance)
                {
                    counter++;
                }
                else
                {
                    return false;
                }
            }

            return counter >= MinMatches;
        }

        void AddBall(Ball newBall)
        {
            if (newBall.DropableType != lineType)
            {
                return;
            }
            
            Balls.Add(newBall);
            _currentLine.AddPoint(newBall.transform.position);
        }

        void SetLineType(EDropableType newtype)
        {
            lineType = newtype;
        }
        
        Vector2 GetMouseScreenPos()
        {
            return _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        void ResetLine()
        {
            CanDraw = false;
            
            _currentLine.OnHitBall -= AddBall;
            Destroy(_currentLine.gameObject);
            
            Balls.Clear();
        }
    }


}