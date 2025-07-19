using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Orbay.Gameplay.SwerveSystem
{
    public class SwerveSystem
    {
        static private float _lastFrameFingerPositionX;
        static private float _moveFactorX;
        static private float MoveFactorX => _moveFactorX;

       // [Header("Swerve Settings")]
        //public float speed = 0.5f;
       /* public float min_X;
        public float max_X;*/

        //private Vector3 x;

        ///<summary>
        ///This function takes touch input and calculate finger position.
        ///</summary>
        public static void SwerveInput(Transform _transform,float min_X,float max_X,float speed)
        {

            var x = _transform.position;
            x.x = Mathf.Clamp(x.x, min_X, max_X);
            _transform.position = x;
            
            if (Input.GetMouseButtonDown(0))
            {
                _lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
                _lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _moveFactorX = 0f;
            }

            Swerve(_transform,min_X,max_X,speed);
        }

       private static void Swerve(Transform _transform, float min_X, float max_X, float speed)
        {
            float swerveAmount = Time.fixedDeltaTime * speed * MoveFactorX;

            if (MoveFactorX < 0 && _transform.position.x > min_X)
            {
                _transform.Translate(swerveAmount, 0, 0);
            }
            if (MoveFactorX > 0 && _transform.position.x < max_X)
            {
                _transform.Translate(swerveAmount, 0, 0);
            }
        }
    }
}
