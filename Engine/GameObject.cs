using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseProject.GameObjects;
using BaseProject.GameObjects.Tiles;

public abstract class GameObject : IGameLoopObject
{
    protected GameObject parent;
    protected Vector2 position, velocity;
    protected int layer;
    protected string id;
    protected bool visible;

    public GameObject(int layer = 0, string id = "")
    {
        this.layer = layer;
        this.id = id;
        position = Vector2.Zero;
        velocity = Vector2.Zero; 
        visible = true;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(this is Player)
        {
           // System.Diagnostics.Debug.WriteLine(true);
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Reset()
    {
        visible = true;
    }

    public virtual void HandleColission(GameObject obj) {  }

    public void CheckColission(GameObjectList list)
    {
        foreach (GameObject obj2 in list.Children)
        {
            if (obj2 is SpriteGameObject)
            {
                this.CheckColission((SpriteGameObject)obj2);
            }
        }
    }

    public virtual void CheckColission(SpriteGameObject obj)
    {
        if (this is GameObjectList)
        {
            GameObjectList list = (GameObjectList)this;
            foreach (GameObject sub in list.Children)
            {
                sub.CheckColission(obj);
            }
        }
        else
        {
            SpriteGameObject oneSprite = (SpriteGameObject)this;
            if (oneSprite.CollidesWith(obj))
            {
                SortColission(oneSprite, obj);
            }
        }
    }

    //function who changes objects back to their types
        void SortColission(GameObject one, GameObject other)
        {

            //player vs tile colission
            if (one is Tile && other is Player)
            {
                Player player = (Player)other;
                Tile tile = (Tile)one;
                player.HandleColission(tile);
                tile.HandleColission(player);
                return;
            }
        //checks if player collides with push projectile
        if(one is Player && ((SpriteGameObject)other).id == "push")
        {
            ((Player)one).getPushed(((SpriteGameObject)other).velocity.X);
            return;
        }
            //rest colission
            other.HandleColission(one);
            one.HandleColission(other);
        }

        

    public virtual Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public virtual Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public virtual Vector2 GlobalPosition
    {
        get
        {
            if (parent != null)
            {
                return parent.GlobalPosition + Position;
            }
            else
            {
                return Position;
            }
        }
    }

    public GameObject Root
    {
        get
        {
            if (parent != null)
            {
                return parent.Root;
            }
            else
            {
                return this;
            }
        }
    }

    public GameObjectList GameWorld
    {
        get
        {
            return Root as GameObjectList;
        }
    }

    public virtual int Layer
    {
        get { return layer; }
        set { layer = value; }
    }

    public virtual GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public string Id
    {
        get { return id; }
    }

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }

    }


   