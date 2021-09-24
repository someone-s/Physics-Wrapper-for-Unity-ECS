using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;

namespace Eden.Physics
{
    public class PhysicsRaycast
    {
        private static PhysicsRaycast singleton;
        public static PhysicsRaycast active
        {
            get
            {
                if (singleton == null)
                    singleton = new PhysicsRaycast();
                return singleton;
            }
        }

        public bool Cast(float3 origin, float3 displacement, uint layer, out RaycastHit hit)
        {
            var physicsWorldSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            var collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;

            RaycastInput input = new RaycastInput
            {
                Start = origin,
                End = origin + displacement,
                Filter = new CollisionFilter
                {
                    BelongsTo = layer,
                    CollidesWith = layer
                }
            };

            return collisionWorld.CastRay(input: input, closestHit: out hit);
        }

        private PhysicsRaycast() { }
    }
}
