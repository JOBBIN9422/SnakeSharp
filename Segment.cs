using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeModels
{
    public class Segment
    {
        public int x { get; set; }
        public int y { get; set; }

        public Segment(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            Segment rhs = (Segment)obj;

            return x == rhs.x && y == rhs.y;
        }
    }
}
