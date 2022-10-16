namespace Elapsed.Terrain.PatchStream.Component
{
    public class TransitionCellData
    {
        // Static Variables
        #region Static Variables
        public static readonly TransitionCellData[] patch = new TransitionCellData[8]
        {
            new TransitionCellData(true, false, true, false),   // Forward Left
            new TransitionCellData(false, false, true, false),  // Forward
            new TransitionCellData(false, true, true, false),   // Forward Right
            new TransitionCellData(true, false, false, false),  // Left
            new TransitionCellData(false, true, false, false),  // Right
            new TransitionCellData(true, false, false, true),   // Back Left
            new TransitionCellData(false, false, false, true),  // Back
            new TransitionCellData(false, true, false, true)    // Back Right
        };
        #endregion

        // Variables
        #region Variables
        #region Left
        private bool left;
        public bool Left
        {
            get { return left; }
            set { left = value; }
        }
        #endregion
        // Right
        #region Right
        private bool right;
        public bool Right
        {
            get { return right; }
            set { right = value; }
        }
        #endregion
        // Forward
        #region Forward
        private bool forward;
        public bool Forward
        {
            get { return forward; }
            set { forward = value; }
        }
        #endregion
        // Back
        #region Back
        private bool back;
        public bool Back
        {
            get { return back; }
            set { back = value; }
        }
        #endregion
        #endregion

        // Constructors
        #region Constructors
        // Default Constructor
        #region Default Constructor
        public TransitionCellData() { }
        #endregion
        // Standard Constructor
        #region Standard Constructor
        public TransitionCellData(bool value = false)
        {
            left = right = forward = back = value;
        }
        #endregion
        // Component Constructor
        #region Component Constructor
        public TransitionCellData(bool left, bool right, bool forward, bool back)
        {
            this.left = left;
            this.right = right;
            this.forward = forward;
            this.back = back;
        }
        #endregion
        #endregion

        // Methods
        #region Methods
        public bool GetTransitionInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.forward:
                    return forward;
                case Direction.back:
                    return back;
                case Direction.right:
                    return right;
                case Direction.left:
                    return left;
                default:
                    return false;
            }
        }
        public void SetTransitionInDirection(Direction direction, bool value)
        {
            switch (direction)
            {
                case Direction.forward:
                    forward = value;
                    break;
                case Direction.back:
                    back = value;
                    break;
                case Direction.right:
                    right = value;
                    break;
                case Direction.left:
                    left = value;
                    break;
            }
        }
        #endregion
    }
}