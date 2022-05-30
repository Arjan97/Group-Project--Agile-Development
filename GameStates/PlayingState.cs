using BaseProject.GameObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        //different objects in the playingstate
        public Player player = new Player();//the runner player
        public TileList tileList = new TileList();//a list of all the tiles in the level
        public Ghost ghost = new Ghost();//the ghost player
        public SpriteGameObject PlayerPush;//the player attack projectile

        //bools to keep track on special gamestates
        bool photoMode = false;//check if photomode is active
        bool paused = false;//check if the player has paused the game

        bool headingRight = true;//bool used for the camera

        InputHandler input;//handles the input of different keys

        public int cameraStartPoint = -3700;
        private int cameraSpeed = 1;

        public PlayingState()
        {
            //adds objects to the objectlist
            Add(player);
            Add(ghost);
            Add(tileList);


            //creates text that shows up when screen pauses
            TextGameObject pause = new TextGameObject("font/Arial40",0,"pauseText");
            pause.Visible = false;
            pause.Text = "Game Paused";
            pause.Position = new Vector2(GameEnvironment.Screen.X/2-pause.Text.Length*20, GameEnvironment.Screen.Y/2);
            Add(pause);

            //creates ghostpush object
            SpriteGameObject GhostPush = new SpriteGameObject("img/players/spr_push", 0, "GhostPush");
            GhostPush.Visible = false;
            Add(GhostPush);

            //initializes playerpush object
            PlayerPush = new SpriteGameObject("img/players/spr_player_push", 0, "PlayerPush");
            PlayerPush.Visible = false;
            Add(PlayerPush);


            //initializes the inputhandler
            input = GameEnvironment.input;
            input.AssignKeys(true);

            position.X = cameraStartPoint;
            headingRight = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (paused) { return; }//skips the update when the game is paused

            base.Update(gameTime);
            player.isGrounded = false;
            tileList.CheckColission(player);//handles the player to tiles colission
            ghost.SetGhostTraps(tileList);//asigns buttons for the gost to traps
            HandleCamera();//repositions camera

            //projectiles colision check
            player.CheckColission((SpriteGameObject)Find("GhostPush"));
            ghost.CheckColission((SpriteGameObject)Find("PlayerPush"));
        }
        /// <summary>
        /// wtf doet dit??? @coen??
        /// </summary>
        /// <param name="level">the id of the next level</param>
        public void LoadLevel(int level)
        {
            player.getCurrentPlayingState();
            tileList.LoadLevel(level);
        }

        /// <summary>
        /// resets the playingstate
        /// </summary>
        public override void Reset()
        {
            position = Vector2.Zero;//repositions camera
            tileList.nextLevelNr = tileList.CurrentLevel;//reloads the tiles
            base.Reset();

            //hides objects that needs to start hidden
            Find("GhostPush").Visible = false;
            Find("pauseText").Visible = false;
        }

        /// <summary>
        /// function that moves the camera according to the player
        /// </summary>
        public void HandleCamera()
        {
            if (player.died == true)
            {
                player.Respawn();
                position.X = 30;
                ghost.Position = GameEnvironment.Screen.ToVector2()/2;
                player.died = false;
            }

            if (player.finished)
            {
                player.Respawn();
                ghost.Position = GameEnvironment.Screen.ToVector2() / 2;
                player.finished = false;
                headingRight = false;
            }


            //check if player turns around
            if(player.onscreen)
            {
                /*
                if(player.GlobalPosition.X < GameEnvironment.Screen.X && !headingRight)
                {
                    position.X += 30;
                    headingRight = true;
                }
                */

                cameraSpeed = 1;
            } else
            {
                cameraSpeed = 4;
            }

            if ((headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 1 / 8 && player.isFacingLeft) || (!headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 7 / 8))
            {
                headingRight = !headingRight;
            }

            //if player moves to the right and passes 3/8 of screen, move camera, unless player is at the edge of the screen
            if (headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 3 / 8 && position.X + GameEnvironment.Screen.X < tileList.LevelSize.X)
            {
                position.X -= 5f * cameraSpeed;
            }
            //if player moves to the left and passes 5/8 of screen, move camera, unless player is at the beginning of the screen
            else if (!headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 5 / 8 && position.X <= 0)
            {
                position.X += 5f * cameraSpeed;
            }

            ghost.StayOnScreen(position, player.onscreen);
        }

        public override void HandleInput(InputHelper inputHelper)
        {

            if (inputHelper.KeyPressed(input.P1(Buttons.start)) || inputHelper.KeyPressed(input.P2(Buttons.start))) HandlePause();

            //activates photo mode
            if (inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D0))
            {
                photoMode = true;
                tileList.HideButtons();
                ghost.Visible = false;
            }
            else if(photoMode)//deactivates photo mode
            {
                ghost.Visible = true;
                tileList.ShowButtons();
                photoMode = false; 
            }

            if(paused)
                return;

            ghost.HandlePush(inputHelper.KeyPressed(input.Ghost(Buttons.L)), (SpriteGameObject)Find("GhostPush"));
                base.HandleInput(inputHelper);
            ghost.HandlePush(inputHelper.KeyPressed(GameEnvironment.input.Ghost(Buttons.R)), (SpriteGameObject)Find("GhostPush"));
            base.HandleInput(inputHelper);
            if (!ghost.stunned)
            {
                ghost.HandlePush(inputHelper.KeyPressed(GameEnvironment.input.Ghost(Buttons.R)), (SpriteGameObject)Find("GhostPush"));
            }
                base.HandleInput(inputHelper);
        }

        /// <summary>
        /// function that makes the pause text visible
        /// </summary>
        void HandlePause()
        {
            paused = !paused;
            Find("pauseText").Visible = paused;
        }
    } 
}
