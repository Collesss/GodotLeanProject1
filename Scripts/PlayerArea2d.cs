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
        GetStick().Normalized() * Speed * (float)GetProcessDeltaTime();


	private Vector2 GetStick() =>
		new Vector2(GetAxisHor(), GetAxisVer());

    private float GetAxisHor() =>
		Input.GetAxis("move_left", "move_right");

    private float GetAxisVer() =>
        Input.GetAxis("move_up", "move_down");

}
