using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using BaseProject.GameObjects.Tiles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    public class Player : AnimatedGameObject
    {

        public float speed, jumpSpeed;
        public bool isFalling, isColliding, keyPressed, isGrounded, isJumping, jumpKeyPressed, died, blockMovement, facingLeft;
        public Vector2 pVelocity;
        public string verticalCollidingSide;
        public int jumpframes, blockedframes, lives, maxLives =3;
        private float timer;

        public bool isDashing;
        private float dashDuration;
        public int dashPower;
        public bool isFacingLeft;
        public bool isFacingRight;
        private GameObjectList livesIcons;
        InputHandler input;

        PlayingState currentPlayingState;

        public bool PushCooldown = false; //push to see if Push is on cooldown
        public int PushCooldownTimer = 0;//int used to track cooldown of the push
        public int PushCoolDownTime = 300; //int used to set limit to the cooldown of the push
        public int PushTimer;//int used to track duration of Push
        public int PushTime = 50;//int used to set limit to the duration of the Push
        static float PushSpeed = 300f; //float to set the speed of the push
        public SpriteGameObject PushObject;

        public Player() : base(Game1.Depth_Player)
        {
            LoadAnimation("img/players/spr_player_idle@8", "idle", true, 0.1f);
            LoadAnimation("img/players/spr_player_run@4", "run", true, 0.1f);
            LoadAnimation("img/players/spr_player_jump@2", "jump", true, 0.5f);
            PlayAnimation("idle");
            SetOriginToBottomCenter();

            input = GameEnvironment.input;

            keyPressed = false;
            pVelocity = velocity;
            isFalling = true;
            isColliding = false;
            died = false;
            jumpSpeed = 100f;
            speed = 5f;
            jumpframes = 0;
            timer = 0;

            //Player dash ability 
            isDashing = false;
            dashDuration = 0;
            dashPower = 15;
            isFacingLeft = false; //Checks if the player is facing left, used for the player dash and animation
            Reset();
            createLives();
            Respawn();


        }

        public override void HandleColission(GameObject obj)
        {
            if(obj is Spike)
            {

            }
            base.HandleColission(obj);
        }

        public override void Reset()
        {
            lives = maxLives;
            Respawn();
            base.Reset();
        }

        public void getCurrentPlayingState()
        {
            currentPlayingState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
            PushObject = currentPlayingState.PlayerPush;
        }

        public void Respawn()
        {
            position.X = 1.5f * Tile.tileSize;
            position.Y = GameEnvironment.Screen.Y / 2;
            Velocity = Vector2.Zero;
            died = false;
        }

        public override void Update(GameTime gameTime)
        {
            
            float i = 1;

            //dying when falling of the map
            if(position.Y > GameEnvironment.Screen.Y)
            {
                death();
            }

            //checking different stages of the jump
            if (isJumping && jumpframes < 30 && jumpKeyPressed && (verticalCollidingSide != "up"))
            {
                if (jumpframes == 1)
                {
                    velocity.Y -= 30;
                }
                else if (jumpframes < 10)
                {
                    velocity.Y -= 17;
                }
                else
                {
                    velocity.Y -= 6;
                }
                PlayAnimation("jump");
                jumpframes++;
            }
            else
            {
                jumpframes = 1;
                isJumping = false;
            }
            
            //adding gravity
            if (!isGrounded)
            {
                velocity.Y += 4.5f * i;
            }

            if(blockMovement)
            {
                blockedframes++;
            }

            velocity *= 70f;

            
            {

            }

            if (PushCooldown)
            {
                PushCooldownTimer++;
                if (PushCooldownTimer > PushCoolDownTime)
                {
                    PushCooldown = false;
                    PushCooldownTimer = 0;
                }
            }

            if (PushObject.Visible)
            {
                PushTimer++;
                if (PushTimer > PushTime)
                {
                    PushObject.Visible = false;
                    PushTimer = 0;
                }
            }
            base.Update(gameTime);
            Velocity *= Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for(int i=0; i<lives; i++)
            {
                livesIcons.Children[i].Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            //Player push ability
            if (inputHelper.IsKeyDown(input.Player(Buttons.Y))){

                if(!PushCooldown)
                {
                    PushCooldown = true;
                    PushObject.Scale = new Vector2(2, 2);
                    PushObject.Position = position - new Vector2(0, sprite.Height / 2);
                    PushObject.Visible = true;

                    if(sprite.Mirror)
                    {
                        PushObject.Velocity = new Vector2(-PushSpeed, 0);
                    } else
                    {
                        PushObject.Velocity = new Vector2(PushSpeed, 0);
                    }
                } else
                {

                }
                
            }
            //Player Dash ability
            if (inputHelper.IsKeyDown(input.Player(Buttons.R)))
            {
                isDashing = true;
                dashDuration++;

                if (dashDuration <= 10 && !isFacingLeft)
                {
                    velocity.X += dashPower;

                }
                else if (dashDuration <= 10 && isFacingLeft)
                {
                    velocity.X += -dashPower;
                }
            }
            //Checks if the player is dashing, then a cooldown is issued
            if (isDashing)
            {
                timer++;
                if (timer >= 200)
                {
                    dashDuration = 0;
                    timer = 0;
                    isDashing = false;
                }
            }

            //code to block all movement except the dash
            if (blockMovement)
            {
                if(blockedframes == 15)
                {
                    blockMovement = false;
                    blockedframes = 0;
                }
                //possible 'stunned' animation
                PlayAnimation("idle");
                return;
            }

            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(input.Player(Buttons.left)))
            {
                velocity.X += -speed;
                PlayAnimation("run");
                facingLeft = true;
                isFacingLeft = true;
            }

            else if (inputHelper.IsKeyDown(input.Player(Buttons.right)))
            {
                velocity.X += speed;
                isFacingLeft = false;
                facingLeft = false;
                PlayAnimation("run");
                
            } else
            {
                PlayAnimation("idle");
            }

            if (!inputHelper.IsKeyDown(input.Player(Buttons.left)))
            {
                isFacingLeft = false;
            }


            if (inputHelper.IsKeyDown(input.Player(Buttons.up)) || inputHelper.IsKeyDown(input.Player(Buttons.B))  /* && isGrounded */)
            {
                isColliding = false;
                keyPressed = true;
                isJumping = true;
                jumpKeyPressed = true;
            }
            else if (!inputHelper.IsKeyDown(input.Player(Buttons.up)) && !inputHelper.IsKeyDown(input.Player(Buttons.B)))
            {
                jumpKeyPressed = false;
            }

            sprite.Mirror = facingLeft;

        }

        public void HandleColission(Tile tile)
        {
            //checking and handling collision with SpikeTile
            if (tile is SpikeTile || tile is SpikeRoofTile)
            {
               
                death();

            }
            //checking and handling collision with SwitchTile
            if(tile is SwitchTile)
            {
              SwitchObject switchTile = (SwitchObject)tile.Parent;
                if (switchTile.Armed) { 
 
                        death();

                }
            }

            //checking and handling collision with FinishTile
            if(tile is FinishTile)
            {
                nextLevel();
            }
            
            Vector2 intersection = Collision.CalculateIntersectionDepth(BoundingBox, tile.BoundingBox);

            //checking if its a vertical collision
            if (Math.Abs(intersection.X) > Math.Abs(intersection.Y))
            {
               
                //collision bottom side player and top side tile
                if (intersection.Y < 0)
                {
                    isColliding = true;
                    isGrounded = true;
                    verticalCollidingSide = "down";
                    position.Y -= Math.Abs(intersection.Y) - 1;
                    
                }
                //collision top side player and bottom side tile
                else
                {
                    isColliding = true;
                    verticalCollidingSide = "up";

                }
            }
            else
            {
                //collision right side player and left side tile
                if (intersection.X < 0)
                {
                    isColliding = true;
                    position.X -= Math.Abs(intersection.X) -1;
                }
                //collision left side player and right side tile
                else
                {
                    isColliding = true;
                    position.X += Math.Abs(intersection.X) +1;
                }

                //blocking movement when player keeps colliding with wall.
                if(intersection.X > 8 || intersection.X < -8)
                {
                    blockMovement = true;
                }
            }
        }

        //function thats moves the player, gets called when colliding with push projectile
        public void getPushed(float speed)
        {
            //checks if the push projectile is moving left or right
            if(speed > 0)
            {
                velocity.X += 30;
            }
            else
            {
                velocity.X -= 30;
            }
        }

        //function to handle the death of a player
        void death()
        {
            //TODO death annimation
            lives--;
            died = true;
            if(lives <= 0)//checks if the player can respawn
            {
                GameEnvironment.GameStateManager.SwitchTo("gameOverState");
                return;
            }
            else
            {
            PlayingState play =(PlayingState) GameEnvironment.GameStateManager.GetGameState("playingState");
            play.tileList.nextLevelNr = 0;
             Respawn();
             play.ghost.Reset();
             //System.Diagnostics.Debug.WriteLine(lives);
            }
        }
        //method to change level
        void nextLevel()
        {
            PlayingState play = (PlayingState)GameEnvironment.GameStateManager.GetGameState("playingState");
            play.tileList.nextLevelNr = play.tileList.currentLevel +1;
            
            Reset();
            play.ghost.Reset();
        }

        //function to create the lives icons
        void createLives()
        {
            livesIcons = new GameObjectList(0, "lives");
            livesIcons.Parent = Parent;
            livesIcons.Position = new Vector2(30, GameEnvironment.Screen.Y *1/10);

            //creates an Icon for each live
            for(int i = 0; i< maxLives; i++)
            {
                SpriteGameObject live = new SpriteGameObject("img/players/spr_testplayer");
                live.Scale = new Vector2(0.5f, 0.5f);
                live.Position += new Vector2(live.Sprite.Width * i,0);
                livesIcons.Add(live);
            }
        }
        void SetOriginToBottomCenter()
        {
            Origin = new Vector2(sprite.Width / 2, sprite.Height);
        }

        void HandlePush()
        {

        }
    }
}
