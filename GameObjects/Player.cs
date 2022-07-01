using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BaseProject.GameObjects.Tiles;
using BaseProject.GameStates;
using BaseProject.GameComponents;
using BaseProject.GameObjects.Particles;

namespace BaseProject.GameObjects
{
    public class Player : AnimatedGameObject
    {
        //variables used for the collision
        public bool isColliding; //boolean used to track if the player is colliding
        public string verticalCollidingSide; //string to set left or right for vertical collision

        //variables used for movement
        public bool isFalling; //boolean used to track if the player is falling
        public bool isJumping; //boolean used to track if the player is jumping
        public bool isGrounded; //boolean used to track if the player is touching the ground
        public bool isDashing; //boolean used to track if player is dashing
        public bool isFacingLeft; //boolean to check if the player is looking to the left
        private float dashDuration; //float used for the duration of the dash
        public float jumpSpeed; //float used to set jumping speed
        public float speed; //float used to set movement speed
        public int dashPower; //int used for the power of the dash
        private Cooldown dashCooldown; //icon for the dash cooldown timer
        public int jumpframes; //int to track the amount of frames the player has been jumping
        public Vector2 pVelocity; //player velocity

        //variables used for input
        public bool jumpKeyPressed; //boolean used to track if the jump key is pressed
        public bool keyPressed; //boolean used to track if key is pressed
        InputHandler input; //input handler

        //variables used to block players movement
        public bool blockMovement; //boolean to used to toggle blocked movement 
        public int blockedframes; //int to track the amount of blocked frames

        //variables used for lives
        public int maxLives = 3; //int to update the max amount of lives a player can have
        public int lives = 3; //int used to track the players lives
        public bool died; //boolean used to track if the player is dead
        private GameObjectList livesIcons; //icons to display lives

        private float timer; //Angelina? wat doet deze timer?

        PlayingState currentPlayingState;//current playinstate

        //variables used for the players push/stun
        public bool PushCooldown = false; //bool to see if Push is on cooldown
        public int PushCooldownTimer = 0;//int used to track cooldown of the push
        public int PushCoolDownTime = 300; //int used to set limit to the cooldown of the push
        public int PushTimer;//int used to track duration of Push
        public int PushTime = 65;//int used to set limit to the duration of the Push
        static float PushSpeed = 450f; //float to set the speed of the push
        public SpriteGameObject PushObject;//sprite game object of the push
        private Cooldown attackCooldown; //sprite object that displays the cooldown

        //variables used for the animations
        public string currentAnimation = "idle"; //string used for current animation
        public string newAnimation = "idle"; //string used to change animation

        //variables for the death animation timer
        public int DeathAnimationTimer;//int used to track death animation
        public int DeathAnimationDuration = 16; //int used to set duration of death animation
        public bool DeathAnimation = false;//boolean to activate death animation

        //variables for the attack animation timer
        public int AttackAnimationTimer;//int used to track attack animation
        public int AttackAnimationDuration = 34; //int used to set duration of attack animation
        public bool AttackAnimation; //boolean to activate attack animation

        ParticleMachine particles = new ParticleMachine(ParticleType.MovementParticle);
        Vector2 particleAccelaration = new Vector2(0, 0);
        Vector2 particleVelocity = new Vector2(0, 0);
        public int particleTimer;
        public int particleTimerInterval = 3;

        public bool finished; //boolean used to check if the player has finished
        public bool onscreen;//boolean used to check if the player is still on screen

        public Player() : base(Game1.Depth_Player)
        {

            attackCooldown = new Cooldown("img/icon/spr_attack", parent, new Vector2(200, 80));
            dashCooldown = new Cooldown("img/icon/spr_push", parent, new Vector2(260,80));
            //loading in the animations
            LoadAnimation("img/players/spr_player_idle@8", "idle", true, 0.1f);
            LoadAnimation("img/players/spr_player_run@4", "run", true, 0.1f);
            LoadAnimation("img/players/spr_player_jump@2", "jump", true, 0.5f);
            LoadAnimation("img/players/spr_player_death@5", "death", true, 0.25f);
            LoadAnimation("img/players/spr_player_dead", "dead", false);
            LoadAnimation("img/players/spr_player_attack@2", "attack", true, 0.2f);
            PlayAnimation("idle");
            SetOriginToBottomCenter();

            particles.Parent = this;

            input = GameEnvironment.input;

            keyPressed = false;
            pVelocity = velocity;
            isFalling = true;
            isColliding = false;
            died = false;
            jumpSpeed = 100f;
            speed = 1.5f;
            jumpframes = 0;
            timer = 0;

            //player dash ability 
            isDashing = false;
            dashDuration = 0;
            dashPower = 15;
            isFacingLeft = false; //Checks if the player is facing left, used for the player dash and animation
            Reset();
            createLives();
            Respawn();


        }

        /*
         * Method used to Handle Collision
         * @params GameObject obj
         * @return void
         */


        /*
         * Method used to Reset the player
         * @return void
         */
        public override void Reset()
        {
            //resetting lives
            lives = maxLives;
            Respawn();
            base.Reset();
        }

        /*
         * Method used to get the current playing state
         * @return void
         */
        public void getCurrentPlayingState()
        {
            currentPlayingState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
            PushObject = currentPlayingState.PlayerPush;
        }



        /// <summary>
        /// Method to respawn the player  
        /// </summary>
        /// <returns>void</returns>
        public void Respawn()
        {
            position.X = 1.5f * Tile.tileSize;
            position.Y = GameEnvironment.Screen.Y / 2;
            Velocity = Vector2.Zero;
            died = false;
            DeathAnimationTimer = 0;
        }

        /*
         * Method to update player
         * @params GameTime gameTime
         * @return void
         */
        public override void Update(GameTime gameTime)
        {

            float i = 1;
            particleTimer++;

            //dying when falling of the map
            if (position.Y > GameEnvironment.Screen.Y)
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

                newAnimation = "jump";
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

            //checking if movement is blocked
            if (blockMovement)
            {
                blockedframes++;
            }

            velocity *= 70f;

            //checking if the push ability is on cooldown
            if (PushCooldown)
            {
                PushCooldownTimer++;
                if (PushCooldownTimer > PushCoolDownTime)
                {
                    PushCooldown = false;
                    PushCooldownTimer = 0;
                }
            }

            //checkinf if push object is visible/active
            if (PushObject.Visible)
            {
                PushTimer++;
                if (PushTimer > PushTime)
                {
                    PushObject.Visible = false;
                    PushTimer = 0;
                }
            }

            //checking if deathanimation is triggered
            if (DeathAnimation)
            {
                System.Diagnostics.Debug.WriteLine(DeathAnimationTimer);

                if (DeathAnimationTimer < 1)
                {
                    blockMovement = true;
                    newAnimation = "death";
                }
                else if (DeathAnimationTimer > 70 && DeathAnimationTimer < 180)
                {
                    newAnimation = "dead";
                    //PlayAnimation("dead");
                }
                else if (DeathAnimationTimer > 180)
                {
                    newAnimation = "idle";
                    DeathAnimation = false;
                    blockMovement = false;
                    death();

                }

                DeathAnimationTimer++;
            }

            AnimationHandler();
            particles.Update(gameTime);
            base.Update(gameTime);
            Velocity *= Vector2.Zero;
            playerOnScreen();
        }

        /*
         * Method to draw the player
         * @params GameTime gameTime
         * @params SpriteBatch spriteBatch
         * @return void
         */
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < lives; i++)
            {
                livesIcons.Children[i].Draw(gameTime, spriteBatch);
            }
            dashCooldown.Draw(gameTime, spriteBatch);
            attackCooldown.Draw(gameTime, spriteBatch);
            particles.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        /*
         * Method to Handle player Input
         * @params InputHelper inputHelper
         * @return void
         */
        public override void HandleInput(InputHelper inputHelper)
        {
            //code to block all movement except the dash
            if (blockMovement || !onscreen)
            {
                if (blockedframes == 15)
                {
                    if (!DeathAnimation) blockMovement = false;
                    blockedframes = 0;
                }
                //possible 'stunned' animation
                if (!DeathAnimation) newAnimation = "idle";
                return;
            }

            //Player push ability
            if (inputHelper.IsKeyDown(input.Player(Buttons.Y)))
            {

                if (!PushCooldown)
                {
                    newAnimation = "attack";
                    AnimationHandler();
                    //PushObject.Visible = true;

                    if (sprite.Mirror)
                    {
                        PushObject.Velocity = new Vector2(-PushSpeed, 0);
                    }
                    else
                    {
                        PushObject.Velocity = new Vector2(PushSpeed, 0);
                    }
                }

            }
            //Player Dash ability
            if (inputHelper.IsKeyDown(input.Player(Buttons.R)))
            {
                isDashing = true;
                dashDuration++;

                        if (dashDuration <= 10)
                        {
                            velocity.X += dashPower;

                        }
                    }
            if (inputHelper.IsKeyDown(input.Player(Buttons.L)))
            {
                isDashing = true;
                dashDuration++;
                if (dashDuration <= 10)
                        {
                    velocity.X -= dashPower;
                }
            }
                //Checks if the player is dashing, then a cooldown is issued
                if (isDashing)
                    {
                        timer++;
                        if (timer >= 2000)
                        {
                            dashDuration = 0;
                            timer = 0;
                            isDashing = false;
                        }
                    }


                    base.HandleInput(inputHelper);

            //player moving left
            if (inputHelper.IsKeyDown(input.Player(Buttons.left)))
            {
                velocity.X += -speed;
                newAnimation = "run";
                isFacingLeft = true;
            }
            //player moving right
            else if (inputHelper.IsKeyDown(input.Player(Buttons.right)))
            {
                velocity.X += speed;
                isFacingLeft = false;
                newAnimation = "run";

            }
            else
            {
                //setting idle animation if player isnt moving
                newAnimation = "idle";
            }

            if (inputHelper.IsKeyDown(input.Player(Buttons.left)) && particleTimer > particleTimerInterval && isGrounded || inputHelper.IsKeyDown(input.Player(Buttons.right)) && particleTimer > particleTimerInterval && isGrounded)
            {
                particleTimer = 0;
                particleAccelaration.Y = GameEnvironment.Random.Next(0, 10);
                particleAccelaration.X = GameEnvironment.Random.Next(20, 30);
                particleVelocity = velocity;
                particleVelocity.Y = 0;
                particleVelocity.X = (particleVelocity.X /2) * -1;
                particleAccelaration.Y *= -1;
                if (velocity.X > 0)
                {
                    particleAccelaration.X *= -1;
                }

                for (int i = 0; i < 5; i++)
                {
                    //System.Diagnostics.Debug.WriteLine("particle acc: " + particleAccelaration.X);
                    particles.SpawnParticles(Vector2.Zero , particleVelocity, particleAccelaration, 20, "img/particle/MovementParticle");
                }
            }

                    if (!inputHelper.IsKeyDown(input.Player(Buttons.left)))
                    {
                        isFacingLeft = false;
                    }

            //player jumping
            if (inputHelper.IsKeyDown(input.Player(Buttons.up))  && isGrounded  || inputHelper.IsKeyDown(input.Player(Buttons.B))   && isGrounded)
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
            //mirroring sprite
            sprite.Mirror = isFacingLeft;
        }

        /*
         * Method to Handle Collision
         * @params Tile tile
         * @return void
         */
        public void HandleColission(Tile tile)
        {
            Vector2 intersection = Collision.CalculateIntersectionDepth(BoundingBox, tile.BoundingBox);
            //checking and handling collision with SpikeTile
            if ((tile is SpikeTile && DeathAnimationTimer < 180 || tile is SpikeRoofTile && DeathAnimationTimer < 180))
            {

                DeathAnimation = true;

            }
            //checking and handling collision with SwitchTile
            if (tile is SwitchTile)
            {
                SwitchObject switchTile = (SwitchObject)tile.Parent;
                if (switchTile.Armed)
                {

                    DeathAnimation = true;
                }
            }

            //checking and handling collision with FinishTile
            if (tile is FinishTile)
            {
                finished = true;
                nextLevel();
            }



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
                if (!DeathAnimation)
                {
                    //collision right side player and left side tile
                    if (intersection.X < 0)
                    {
                        isColliding = true;
                        position.X -= Math.Abs(intersection.X) - 1;
                    }
                    //collision left side player and right side tile
                    else
                    {
                        isColliding = true;
                        position.X += Math.Abs(intersection.X) + 1;
                    }

                }

                //blocking movement when player keeps colliding with wall.
                if (intersection.X > 8 || intersection.X < -8)
                {
                    blockMovement = true;
                }
            }
        }

        /*
         * Method to move player if colliding with push object of the ghost
         * @params float speed
         * @return void
         */
        public void getPushed(float speed)
        {
            //checks if the push projectile is moving left or right
            if (speed > 0)
            {
                velocity.X += 30;
            }
            else
            {
                velocity.X -= 30;
            }
        }

        /*
         * Method to handle players Death
         * @return void
         */
        void death()
        {
            lives--;
            died = true;
            if (lives <= 0)//checks if the player can respawn
            {
                GameEnvironment.GameStateManager.SwitchTo("gameOverState");
                return;
            }
            else
            {
                PlayingState play = (PlayingState)GameEnvironment.GameStateManager.GetGameState("playingState");
                play.tileList.nextLevelNr = play.tileList.currentLevel;
                DeathAnimation = false;

                play.ghost.Reset();
            }
        }

        /*
         * Method to change level
         * @return void
         */
        void nextLevel()
        {
            PlayingState play = (PlayingState)GameEnvironment.GameStateManager.GetGameState("playingState");
            Reset();
            play.ghost.Reset();
            //died = true;
            PlayerWinState winstate = (PlayerWinState)GameEnvironment.GameStateManager.GetGameState("playerWinState");
            if (!input.IsPlayer1Ghost)
            {
                winstate.text = "player 1 wins!";
            }
            else
            {
                winstate.text = "player 2 wins!";
            }
            GameEnvironment.GameStateManager.SwitchTo("playerWinState");

        }

        /*
         * Method to create lives icons
         * @return void
         */
        void createLives()
        {
            livesIcons = new GameObjectList(0, "lives");
            livesIcons.Parent = Parent;
            livesIcons.Position = new Vector2(30, GameEnvironment.Screen.Y * 1 / 10);

            //creates an Icon for each live
            for (int i = 0; i < maxLives; i++)
            {
                SpriteGameObject live = new SpriteGameObject("img/players/spr_testplayer");
                live.Scale = new Vector2(0.5f, 0.5f);
                live.Position += new Vector2(live.Sprite.Width * i, 0);
                livesIcons.Add(live);
            }
        }

        /*
         * Method to set Origin for animations
         * @return void
         */
        void SetOriginToBottomCenter()
        {
            Origin = new Vector2(sprite.Width / 2, sprite.Height);
        }

        /*
         * Method to handle all the animations
         * @return void
         */
        public void AnimationHandler()
        {
            if (AttackAnimation)
            {
                if (AttackAnimationTimer < AttackAnimationDuration)
                {
                    AttackAnimationTimer++;
                    return;
                }
                else
                {
                    AttackAnimation = false;
                    AttackAnimationTimer = 0;
                    PushObject.Visible = true;
                    PushCooldown = true;
                    PushObject.Scale = new Vector2(2, 2);
                    PushObject.Mirror = this.Mirror;
                    PushObject.Position = position - new Vector2(0, sprite.Height / 2);
                }

            }

            if (newAnimation != currentAnimation)
            {
                currentAnimation = newAnimation;
                PlayAnimation(currentAnimation);

                if (currentAnimation == "attack")
                {
                    AttackAnimation = true;
                }
            }
        }

        /*
         * Method to check if player is on screen
         * @return void
         */
        public void playerOnScreen()
        {
            if (GlobalPosition.X < 0 || GlobalPosition.X > GameEnvironment.Screen.X)
            {
                onscreen = false;
            } else
            {
                onscreen = true;
            }
        }

    }
}


