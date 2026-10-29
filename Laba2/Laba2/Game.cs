using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{

    public class Game
    {
        public int size;
        public Player cat;
        public Player mouse;

        public Game(int size)
        {
            this.size = size;

            cat = new Player("Cat");
            mouse = new Player("Mouse");
        }

        public void Run()
        {
      
        }
    }

}
