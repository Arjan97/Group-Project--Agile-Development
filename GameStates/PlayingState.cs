using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects.Background;
using Microsoft.Xna.Framework.Media;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        Player player = new Player();
        public TileList tileList = new TileList();
        Ghost ghost = new Ghost();
        bool photoMode = false;
        bool headingRight = true;
        private List<ScrollingBackground> _scrollingBackgrounds;

        public PlayingState()
        {
            Add(player);
            Add(tileList);
            Add(ghost);
            Add(new SpriteGameObject("img/players/spr_push", 0, "push"));
            stopMusic();
            playMusic();
            //movingBackground();
            //Add(new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_2"), player, 30f));
            //Add(new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_1"), player, 30f));
            //Add(new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_3"), player, 30f));
            Add(new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_5"), player, 60f));
           // Add(new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_4"), player, 10f));
        }

        public void stopMusic()
        {
            MediaPlayer.Stop();
        }
        public void playMusic()
        {
            GameEnvironment.AssetManager.PlayMusic("sounds/gamesong", true);
        }

        /*
        public void movingBackground()
        {
            _scrollingBackgrounds = new List<ScrollingBackground>()
      {
        new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_5"), player, 60f)
        {
          Layer = -5,
        }, 
        new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_4"), player, 60f)
        {
          Layer = -5,
        },
        new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_3"), player, 40f)
        {
          Layer = -5,
        },
        new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_2"), player, 30f)
        {
          Layer = -5,
        },
        new ScrollingBackground(GameEnvironment.AssetManager.GetSprite("img/backgrounds/background_1"), player, 25f, true)
        {
          Layer = -5,
        }
            }; System.Diagnostics.Debug.WriteLine("jaja geladen");
        }  */

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            tileList.CheckColission(player);
            ghost.SetGhostDistance(tileList);
            HandleCamera();
            player.CheckColission((SpriteGameObject)Find("push"));
            //movingBackground();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void LoadLevel(int level)
        {
            tileList.LoadLevel(level);
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
            if (inputHelper.IsKeyDown(Keys.D0))
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
            ghost.HandlePush(inputHelper.KeyPressed(Keys.P), (SpriteGameObject)Find("push"));
            base.HandleInput(inputHelper);
        }

    }     
}
