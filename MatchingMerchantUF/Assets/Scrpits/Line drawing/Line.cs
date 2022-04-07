using System;
using System.Collections.Generic;
using UnityEngine;

using Scrpits;

namespace Scrpits.Line_drawing
{
    
    public class Line : MonoBehaviour
    {
        [SerializeField] private int LineIndex;
        [SerializeField] private List<Vector2> linePoints = new List<Vector2>();
        [SerializeField] private LineRenderer _renderer;
        [SerializeField] private EdgeCollider2D _collider;
        
        public event Action<Ball> OnHitBall;


        void Start() 
        {
            _collider.transform.position -= transform.position;
        }
        
        public void StartLine(Vector2 pos) 
        {
            if(!CanAppend(pos)) return;
            

            linePoints.Add(pos);
            linePoints.Add(pos);

            _renderer.positionCount += 2; //We had two points, one for the start of the line and one for the "End of the line"
            _renderer.SetPosition(0,pos);
            _renderer.SetPosition(_renderer.positionCount - 1,pos);
            

            _collider.points = linePoints.ToArray();
        }

        public void AdjustPoint(Vector2 pos)
        {
            LineIndex = _renderer.positionCount - 1;
            _renderer.SetPosition(LineIndex,pos);
            linePoints.Add(pos);
            _collider.points = linePoints.ToArray();

        }

        public void AddPoint(Vector2 pos)
        {
            
            _renderer.SetPosition(_renderer.positionCount - 1,pos);
            _renderer.positionCount++;
        }
        
        private bool CanAppend(Vector2 pos) {
            if (_renderer.positionCount == 0) return true;

            return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawControler.RESOLUTION;
        }

        public void ClearLine()
        {
            _renderer.positionCount = 0;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Ball>())
            {
                Ball newBall = col.GetComponent<Ball>();
                HitBall(newBall);
            }
        }

        void HitBall(Ball hitball)
        {
            OnHitBall?.Invoke(hitball);
        }

    }
}