using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    public class Player
    {
            public enum State
            {
                Winner,
                Loser,
                Playing,
                NotInGame
            }

            public string name;
            public int location;
            public State state;
            public long distanceTraveled;

            public Player(string name)
            {
                this.name = name;
                location = 0;
                state = State.NotInGame;
                distanceTraveled = 0;
            }

            public void Move(int steps, int fieldSize)
            {
                if (state == State.NotInGame)
                {
                   
                    location = steps;
                    state = State.Playing;
                    return;
                }

                
                location = (int)(((long)location - 1 + steps
                    % fieldSize + fieldSize) % fieldSize) + 1;

               
                distanceTraveled += Math.Abs((long)steps);
            }        
    }
}
