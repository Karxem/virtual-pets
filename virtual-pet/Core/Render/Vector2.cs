using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render {
    public struct Vector2 {
        public int X {  get; set; } 
        public int Y { get; set; }

        public Vector2(int x, int y) { this.X = x; this.Y = y; }
        public Vector2() { }

        public static Vector2 operator +(Vector2 vec1, Vector2 vec2) {
            return new(vec1.X + vec2.X, vec1.Y + vec2.Y); 
        }
        public static Vector2 operator -(Vector2 vec1, Vector2 vec2) {
            return new(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public static Vector2 operator *(Vector2 vec, float f) {
            return new((int)(f * vec.X), (int)(f * vec.Y));
        }

    }
}
