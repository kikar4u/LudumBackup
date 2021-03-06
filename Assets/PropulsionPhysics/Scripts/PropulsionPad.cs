using UnityEngine;

namespace Polycrime
{
    public class PropulsionPad : MonoBehaviour
    {
        public Transform target;
        public float reachTime = 1.5f;
        public Color trajectoryColor = Color.magenta;
        public bool showTrajectory = true;
        public GameObject toSpawn;
        public bool inputBased = false;

        protected virtual void Start()
        {
            target = GameObject.Find("Gourmet").transform;
            // Added an empty start so the script can be enabled/disabled
        }

        ////////////////////////////////////////////////////////////////////////////////
        // When an object hits me, check to see if it implements the tsg_IPropelBehavior
        // interface and if it does call its implemented method.
        protected virtual bool PropelObject(GameObject propelObject, Vector3 velocity)
        {
            var objectInterface = propelObject.GetComponent(typeof(IPropelBehavior)) as IPropelBehavior;

            if (objectInterface != null)
            {
                objectInterface.React(velocity);
                return true;
            }

            return false;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            HandleTrigger(other.gameObject, other.bounds);
        }
        private void Update()
        {
/*            if (Input.GetKeyDown("space") && inputBased)
            {
                GameObject obj = Instantiate(toSpawn, gameObject.transform.position, Quaternion.identity);
                //obj.AddComponent<Collider>();
                //obj.AddComponent<Rigidbody>();
                HandleTrigger(obj, obj.gameObject.GetComponent<SphereCollider>().bounds);
            }*/

        }
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            HandleTrigger(other.gameObject, other.bounds);
        }

        public void HandleTrigger(GameObject gameObject, Bounds bounds)
        {
            if (PropulsionPadActive())
            {
                Vector3 veloctiy = TrajectoryMath.CalculateVelocity(bounds.center, target.position, reachTime);
                PropelObject(gameObject, veloctiy);
            }
        }

        private void OnDrawGizmos()
        {
            if (showTrajectory && PropulsionPadActive())
            {
                TrajectoryLine.Render(transform.position, target.position, reachTime, trajectoryColor);
            }
        }

        private bool PropulsionPadActive()
        {
            return (enabled && target != null && reachTime > 0);
        }
    }
}