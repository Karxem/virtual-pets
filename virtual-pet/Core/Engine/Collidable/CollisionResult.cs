using System.Numerics;
using System.Runtime.InteropServices;

namespace virtual_pet.Core.GameEngine.Collidable
{
    public struct CollisionResult
    {
        public static readonly CollisionResult NoCollision = new CollisionResult(null, new Vector2(0, 0));

        public bool Colliding = false;
        public DateTime TimeStap = DateTime.Now;
        public Vector2 Position { get; }
        public ICollidable Collidable { get; }

        public CollisionResult(ICollidable collidable, Vector2 position)
        {
            Position = position;
            Collidable = collidable;
            Colliding = true;
        }
    }
}
