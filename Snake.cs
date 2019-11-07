using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeModels
{
    public enum SnakeDirection
    {
        Up,
        Down,
        Left,
        Right
    };

    public class Snake
    {
        public List<Segment> BodySegments { get; private set; }
        public SnakeDirection Direction { get; private set; }
        public Queue<SnakeDirection> MovementBuffer { get; set; }

        public Snake(int x, int y, SnakeDirection Direction)
        {
            //create a snake with size 1
            BodySegments = new List<Segment>();
            BodySegments.Add(new Segment(x, y));
            this.Direction = Direction;

            //init movement buffer
            MovementBuffer = new Queue<SnakeDirection>();
            MovementBuffer.Enqueue(Direction);
        }

        //define snake head as the first segment in the bodySegments list
        public Segment GetHead()
        {
            return BodySegments.First();
        }

        //define snake tail as the last segment in the bodySegments list
        public Segment GetTail()
        {
            return BodySegments.Last();
        }

        //"move" the snake by popping its tail and placing it in front of its current head
        public void Move()
        {
            Segment tailSegment = GetTail();
            Segment headSegment = GetHead();

            //remove the current tail so we can replace it 
            BodySegments.RemoveAt(BodySegments.Count - 1);

            //if there is a direction in the buffer, retrieve it (this ensures that only one direction can be read per frame)
            if (MovementBuffer.Count > 0)
            {
                Direction = MovementBuffer.Dequeue();
            }
            
            //determine where to move the current tail
            switch (Direction)
            {
                case SnakeDirection.Up:
                    tailSegment.Move(headSegment.x, headSegment.y - 1);
                    break;

                case SnakeDirection.Down:
                    tailSegment.Move(headSegment.x, headSegment.y + 1);
                    break;

                case SnakeDirection.Left:
                    tailSegment.Move(headSegment.x - 1, headSegment.y);
                    break;

                case SnakeDirection.Right:
                    tailSegment.Move(headSegment.x + 1, headSegment.y);
                    break;
            }

            //insert the newly moved segment as the new head
            BodySegments.Insert(0, tailSegment);
        }

        public void Grow()
        {
            //insert a new segment at the current tail position
            Segment tailSegment = GetTail();
            BodySegments.Add(new Segment(tailSegment.x, tailSegment.y));
        }

    }
}
