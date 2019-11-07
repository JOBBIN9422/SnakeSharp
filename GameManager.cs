using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeModels
{
    public class GameManager
    {
        public Snake snake { get; private set; }
        private Random random;

        public Food food { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        public bool GameRunning { get; set; }

        public GameManager(int maxX, int maxY)
        {
            //define boundaries from constructor arguments
            this.MaxX = maxX;
            this.MaxY = maxY;

            //init RNG
            this.random = new Random();

            //init game elements
            this.snake = new Snake(0, 0, SnakeDirection.Right);
            this.food = new Food(random.Next(MaxX), random.Next(MaxY));

            //wait for the user to start the game
            this.GameRunning = false;
        }

        public bool CheckOutOfBounds()
        {
            //check if the head of the snake is outside the game bounds
            Segment headSegment = snake.GetHead();
            return headSegment.x >= MaxX || headSegment.x < 0
                || headSegment.y >= MaxY || headSegment.y < 0;
        }

        public bool CheckFoodCollision()
        {
            //check if the head segment occupies the same cell as the food
            Segment headSegment = snake.GetHead();
            return headSegment.x == food.x && headSegment.y == food.y;
        }
        public bool CheckSnakeCollision()
        {
            //check if the head segment has intersected any of the body segments
            Segment headSegment = snake.GetHead();

            //start at body index 1 (don't compare the head to itself)
            for (int i = 1; i < snake.BodySegments.Count; i++)
            {
                if (headSegment.Equals(snake.BodySegments[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void StepGame()
        {
            //move the snake 1 step, check for collisions, and stop the game if any occur
            //note that movement must occur before collision checking in order to prevent death when snake grows!
            snake.Move();
            if (CheckOutOfBounds() || CheckSnakeCollision())
            {
                GameRunning = false;
            }
            else
            {
                //"eat" the food, grow, and replace the food at a new location
                if (CheckFoodCollision())
                {
                    snake.Grow();
                    food.Move(random.Next(MaxX), random.Next(MaxY));
                }
            }
        }

        public bool[,] GetGameState()
        {
            bool[,] gameState = new bool[MaxY, MaxX];

            foreach (Segment segment in snake.BodySegments)
            {
                gameState[segment.y, segment.x] = true;
            }
            gameState[food.y, food.x] = true;

            return gameState;
        }

        public void SetSnakeDirection(SnakeDirection direction)
        {
            switch (direction)
            {
                case SnakeDirection.Up:
                    if (snake.Direction == SnakeDirection.Down)
                        return;
                    break;

                case SnakeDirection.Down:
                    if (snake.Direction == SnakeDirection.Up)
                        return;
                    break;

                case SnakeDirection.Left:
                    if (snake.Direction == SnakeDirection.Right)
                        return;
                    break;

                case SnakeDirection.Right:
                    if (snake.Direction == SnakeDirection.Left)
                        return;
                    break;
            }

            if (snake.MovementBuffer.Count < 3)
            {
                //push the new direction into the snake's movement buffer
                snake.MovementBuffer.Enqueue(direction);
            }
        }
    }
}
