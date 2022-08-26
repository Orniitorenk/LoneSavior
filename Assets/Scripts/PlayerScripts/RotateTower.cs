using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTower : MonoBehaviour
{
    // drag in your player object here via the Inspector
    [SerializeField] private Transform _player;

    // If possible already drag your camera in here via the Inspector
    [SerializeField] private Camera _camera;

    private Plane plane;

    void Start()
    {
        // create a mathematical plane where the ground would be
        // e.g. laying flat in XZ axis and at Y=0
        // if your ground is placed differently you'ld have to adjust this here
        plane = new Plane(Vector3.up, Vector3.zero);

        // as a fallback use the main camera
        if (!_camera) _camera = Camera.main;
    }

    void Update()
    {
        
        //Create a ray from the Mouse position into the scene
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        // Use this ray to Raycast against the mathematical floor plane
        // "enter" will be a float holding the distance from the camera 
        // to the point where the ray hit the plane
        if (plane.Raycast(ray, out var enter))
        {
            //Get the 3D world point where the ray hit the plane
            var hitPoint = ray.GetPoint(enter);

            // project the player position onto the plane so you get the position
            // only in XZ and can directly compare it to the mouse ray hit
            // without any difference in the Y axis
            var playerPositionOnPlane = plane.ClosestPointOnPlane(_player.position);

            // now there are multiple options but you could simply rotate the player so it faces 
            // the same direction as the one from the playerPositionOnPlane -> hitPoint 
            _player.rotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);
        }
    }
}
