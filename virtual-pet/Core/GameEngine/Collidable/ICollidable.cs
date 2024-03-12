using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtual_pet.Core.Engine.Tilemap;

namespace virtual_pet.Core.Engine.Collidable
{
    public interface ICollidable
    {

        public TileMap Body { get; set; }
        public CollisionResult Colliding(ICollidable collidable);
        public bool CollisionEnabled() => false;

    }
}
