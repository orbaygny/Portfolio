using....
namespace KingSlayer.Controllers
{
    public class MovementController
    {
    // Variablles
     
     /// <summary>
    /// Constructor
    /// </summary>
        public MovementController(PlayerController playerCtrl,CharacterController charCtrl, PlayeInputController inputCtrl,SkillController skillCtrl, Transform transform, float speed,Animator animator)
        {
            this.playerCtrl = playerCtrl;
            this.charCtrl = charCtrl;
            this.inputCtrl = inputCtrl;
            this.skillCtrl = skillCtrl;
            this.transform = transform;
            this.speed = speed;
            this.animator = animator;
        }
        
    /// <summary>
    /// Handles regular movement input with speed and skill modifier.
    /// </summary>
        public void Move()
        {
            if (inputCtrl.IsMoving && canDash)
                charCtrl.SimpleMove(inputCtrl.CurretMovement*(speed + skillCtrl.Swift()));
        }

    /// <summary>
    /// Performs a dash in a given direction for a certain duration.
    /// Disables dashing temporarily.
    /// </summary>
        public IEnumerator Dash(float dashTime)
        {
            if (!canDash) yield break;
            playerCtrl.DashMatChange(true);
            canDash = false;
            animator.SetBool("Dash", true);
            float startTime = Time.time;
            var lookDirection = dashDirection;
            lookDirection.y = transform.position.y;
            var _dash = dashVector * dashPower;

            while (Time.time < startTime+dashTime) 
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
                 charCtrl.SimpleMove(_dash);
                yield return null;
            }
            animator.SetBool("Dash", false);
            canDash = true;
            playerCtrl.DashMatChange(false);
        }

    /// <summary>
    /// Updates animation parameters based on movement direction and mouse position.
    /// </summary>
        public void Animate()
        {
            if (inputCtrl.CurretMovement.x != 0 || inputCtrl.CurretMovement.z != 0)
            {
                var input = Input.mousePosition;
                var mousePos = Vector3.zero;
                Ray ray = Camera.main.ScreenPointToRay(input);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mouseMask))
                    mousePos = new Vector3(hit.point.x, 0, hit.point.z);

                mousePos = mousePos.normalized;
                var angle = Vector3.Angle(transform.forward.normalized,inputCtrl.CurretMovement.normalized);
                var fixedAngle = AngleCalculate(angle);
            
                var _z = Mathf.Cos(fixedAngle);
                var  _x = Mathf.Sign(inputCtrl.CurretMovement.x) *  Mathf.Sin(fixedAngle);


                if (transform.forward.z < 0)
                    _x = -_x;
              
                z = Mathf.Lerp(z, Mathf.Round(_z), Time.deltaTime * 10f);
                x = Mathf.Lerp(x, Mathf.Round(_x), Time.deltaTime * 10f);

            }
            else
            {
                z = 0;
                x = 0;
            }
            animator.SetFloat("Float Z", z);
            animator.SetFloat("Float X", x);
        }
        
    /// <summary>
    /// Calculates the closest 8-direction angle (in radians) to snap animation directions.
    /// </summary>
        float AngleCalculate(float angle)
        {

            float fixedA = angle;
            float result = 0;
            List<float> angles = new();
            for (int i = 1; i <= 8; i++)
            {
                angles.Add(Mathf.Abs((22.5f * i) - angle));   
            }
      
            for (int i = 0; i < 8; i++)
            {
                if(fixedA> angles[i])
                {
                    fixedA = angles[i];
                    result = (i+1)*22.5f;
                }
                    
            }
            return Mathf.Deg2Rad* result;
        }

        public void ChangeSpeed(float value) => speed += value;

    }
}
