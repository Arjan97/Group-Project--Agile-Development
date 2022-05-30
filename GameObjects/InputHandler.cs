using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BaseProject.GameObjects
{
    //an enum for all the buttons on a controller
    public enum Buttons
    {
        up,
        down,
        left,
        right,
        Y,
        B,
        A,
        X,
        L,
        R,
        select,
        start
    }

    public class InputHandler
    {
        private bool isPlayer1Ghost;//bool that keeps track which player is the ghost
        Dictionary<Buttons, Keys> ghostInput = new Dictionary<Buttons, Keys>();//dictionary that keeps track which keys the ghost has to listen to
        Dictionary<Buttons, Keys> playerInput = new Dictionary<Buttons, Keys>();//dictionary that keeps track which keys the ghost has to listen to

        Dictionary<Buttons, Keys> player1Input = new Dictionary<Buttons, Keys>();//dictionary that keeps track which keys the p1Input has to listen to
        Dictionary<Buttons, Keys> player2Input = new Dictionary<Buttons, Keys>();//dictionary that keeps track which keys the p2Input has to listen to

        public InputHandler() 
        {
            //assings all the keys to the buttons of player 1
            player1Input.Add(Buttons.left,Keys.A);
            player1Input.Add(Buttons.up, Keys.W);
            player1Input.Add(Buttons.right, Keys.D);
            player1Input.Add(Buttons.down, Keys.S);
            player1Input.Add(Buttons.Y, Keys.R);
            player1Input.Add(Buttons.X, Keys.F);
            player1Input.Add(Buttons.A, Keys.E);
            player1Input.Add(Buttons.B, Keys.Q);
            player1Input.Add(Buttons.L, Keys.LeftControl);
            player1Input.Add(Buttons.R, Keys.LeftShift);
            player1Input.Add(Buttons.start, Keys.T);

            //assigns all the keys to the buttons of player 2
            player2Input.Add(Buttons.left, Keys.J);
            player2Input.Add(Buttons.up, Keys.I);
            player2Input.Add(Buttons.right, Keys.L);
            player2Input.Add(Buttons.down, Keys.K);
            player2Input.Add(Buttons.Y, Keys.NumPad4);
            player2Input.Add(Buttons.X, Keys.NumPad8);
            player2Input.Add(Buttons.A, Keys.NumPad6);
            player2Input.Add(Buttons.B, Keys.NumPad5);
            player2Input.Add(Buttons.L, Keys.NumPad7);
            player2Input.Add(Buttons.R, Keys.NumPad9);
            player2Input.Add(Buttons.start, Keys.O);

            AssignKeys(true);
        }

        
        /// <summary>
        /// function that gives the keys to the ghost and player
        /// </summary>
        /// <param name="p1Ghost">bool that checks if player1 will be ghost(true) or player(false)</param>
        public void AssignKeys(bool p1Ghost)
        {
            //checks if the controls needs to be switched
            if (isPlayer1Ghost == p1Ghost && ghostInput.Count >1)
                return;

            if (p1Ghost)
            {
                ghostInput = player1Input;
                playerInput = player2Input;
            }
            else
            {
                ghostInput = player2Input;
                playerInput = player1Input;
            }
            isPlayer1Ghost = p1Ghost;
        }

    /// <summary>
    /// functions that convert buttons back to normal keys
    /// </summary>
    /// <param name="button">the button a player can press</param>
    /// <returns>the key that is assinged to the button</returns>
        public Keys P1(Buttons button) {return player1Input[button];}
        public Keys P2(Buttons button) { return player2Input[button]; }
        public Keys Ghost(Buttons button){return ghostInput[button];}
        public Keys Player(Buttons button){return playerInput[button];}


        public bool IsPlayer1Ghost { get { return isPlayer1Ghost; } }
    }
}
