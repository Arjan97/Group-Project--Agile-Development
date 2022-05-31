using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects.Tiles
{
    internal class SwitchObject : Trap
    {
        bool armed = false;//bool that checks if the trap is deadly/active

        public SwitchObject(int x, int y, string id) : base(x, y)
        {
            this.id = id;
            Add(new SwitchTile(x, y));
        }
   
        /// <summary>
        /// when ativated sends a signal to the switch trap that arms this trap and disables the other
        /// </summary>
        public override void Activate()
        {
            Switch switchtrap = (Switch)parent;
            switchtrap.Activate(id);
            activated = true;
        }
        
        /// <summary>
        /// function to update the armed bool so this trap becomes deadly
        /// </summary>
        public void Arm() { armed = true; }

        public bool Armed { get => armed; }

        public Vector2 ButtonPosition { get => buttonPosition;}
    }
}
