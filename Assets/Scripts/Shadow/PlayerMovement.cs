using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shadow
{

    [RequireComponent(typeof(SurfaceTouch), typeof(HoleDetection))]
    public class PlayerMovement : MonoBehaviour
    {
        private Controls input;
        private SurfaceTouch player;
        private Rigidbody rb;
        private HoleDetection holeDetection;

        [Header("To Link")]
        public Transform graphic;

        [Header("Properties")]
        public float moveSpeed;

        //[Header("States")]
        public Vector2 move;
        private float rotationAngle;

        private void Awake()
        {
            input = new Controls();
            rb = GetComponent<Rigidbody>();
            player = GetComponent<SurfaceTouch>();
            holeDetection = GetComponent<HoleDetection>();
        }

        private void OnEnable()
        {
            Health.OnDie += Disable;
            Finish.OnVictory += Disable;

            input.Player.Enable();
        }

        private void Disable()
        {
            enabled = false;
        }

        private void Update()
        {
            move = input.Player.Movement.ReadValue<Vector2>();
            Move(move);
        }

        private void _FixedUpdate()
        {
            //float x = transform.position.x + velocity.x * Time.fixedDeltaTime;
            //float y = transform.position.y + velocity.y * Time.fixedDeltaTime;
            //float z = transform.position.z + velocity.z * Time.fixedDeltaTime;

            //transform.position = new Vector3(x, y, z);
        }

        void Move(Vector2 direction)
        {
            rb.velocity = Vector3.zero;
            if (player.IsOnWall && holeDetection.DetectHoleInDirection(direction))
                return;

            if (player.IsOnFloor && holeDetection.DetectHoleInDirection(direction))
                return;

            Vector3 vertical;
            if (direction.y > 0 && player.wallTouching > SurfaceTouch.TOUCH_DISTANCE)
                vertical = Vector3.up * direction.y;
            else if (direction.y < 0 && player.floorTouching > SurfaceTouch.TOUCH_DISTANCE)
                vertical = Vector3.forward * direction.y;
            else
                vertical = (player.floorTouching > SurfaceTouch.TOUCH_DISTANCE ? Vector3.forward : Vector3.up) * direction.y;

          Vector3 velocity = direction.x * moveSpeed * Vector3.right + moveSpeed * vertical;
            rb.velocity = velocity;
        }

    }
}