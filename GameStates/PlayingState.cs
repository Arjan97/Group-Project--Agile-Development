using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        Player player = new Player();
        public TileList tileList = new TileList();
        Ghost ghost = new Ghost();
        bool photoMode = false;

        bool headingRight = true;

        public PlayingState()
        {
            Add(player);
            Add(ghost);
            Add(tileList);
            SpriteGameObject push = new SpriteGameObject("img/players/spr_push", 0, "push");
            push.Visible = false;
            Add(push);

            GameEnvironment.input.AssignKeys(true);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.isGrounded = false;
            tileList.CheckColission(player);
            ghost.SetGhostDistance(tileList);
            HandleCamera();
            player.CheckColission((SpriteGameObject)Find("push"));
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void LoadLevel(int level)
        {
            tileList.LoadLevel(level);
        }

        public override void Reset()
        {
            tileList.nextLevelNr = tileList.CurrentLevel;
            base.Reset();
            Find("push").Visible = false;
            
        }

        //function that moves the camera
        public void HandleCamera()
        {
            if(player.died == true)
            {
                position.X = 30;
                ghost.Position = GameEnvironment.Screen.ToVector2()/2;
                player.died = false;
            }
            //check if player turns around
            if((headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 1 / 8) || (!headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 7 / 8))
            {
                headingRight = !headingRight;
            }

            //if player moves to the right and passes 3/8 of screen, move camera, unless player is at the edge of the screen
            if (headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 3 / 8 && position.X + GameEnvironment.Screen.X < tileList.LevelSize.X)
            {
                position.X -= 5f;
            }
            //if player moves to the left and passes 5/8 of screen, move camera, unless player is at the beginning of the screen
            else if (!headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 5 / 8 && position.X <= 0)
            {
                position.X += 5f;
            }
            ghost.StayOnScreen(position);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D0))
            {
                photoMode = true;
                tileList.HideButtons();
                ghost.Visible = false;
            }
            else if(photoMode)
            {
                ghost.Visible = true;
                tileList.ShowButtons();
                photoMode = false; 
            }
            ghost.HandlePush(inputHelper.KeyPressed(GameEnvironment.input.Ghost(Buttons.R)), (SpriteGameObject)Find("push"));
            base.HandleInput(inputHelper);
        }

    }     
}
