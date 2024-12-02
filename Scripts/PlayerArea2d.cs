using Godot;
using System;

public partial class PlayerArea2d : Area2D
{
    #region Signals
    [Signal]
    public delegate void HitEventHandler();
    #endregion

    #region Exports
    [Export]
    public float Speed { get; set; } = 400;
    #endregion

    #region Privates
    private Vector2 _screenSize;

	private AnimatedSprite2D _animatedSprite2D;

    private CollisionShape2D _collisionShape2D;

    private Vector2 _startPosition;
    #endregion


    public override void _Ready()
	{
        Hide();
		_screenSize = GetViewportRect().Size;
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 move = GetMove();

        Position = GetNewPosition(move);
        

        if (move.LengthSquared() != 0)
            _animatedSprite2D.Play();

        if (move.Y != 0)
        {
            _animatedSprite2D.Animation = "up";
            _animatedSprite2D.FlipV = move.Y > 0;
        }
		else if (move.X != 0)
        {
            _animatedSprite2D.Animation = "walk";
            _animatedSprite2D.FlipV = false;
            _animatedSprite2D.FlipH = move.X < 0;
        }
		else
			_animatedSprite2D.Stop();

    }

    public void SetStartPosition(Vector2 positon) =>
        _startPosition = Position;

    public void Start(Vector2 positon)
    {
        Position = positon;
        Show();
        _collisionShape2D.Disabled = false;
    }


    public void OnBodyEntered(Node2D body)
    {
        Hide();
        EmitSignal(SignalName.Hit);
        _collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }


    private Vector2 GetNewPosition(Vector2 move) =>
		(Position + move).Clamp(Vector2.Zero, _screenSize);


	private Vector2 GetMove() =>
        Input.GetVector("move_left", "move_right", "move_up", "move_down").Normalized() * Speed * (float)GetProcessDeltaTime();
}
