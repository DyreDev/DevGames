using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private float speed = 300.0f;
    private float jumpSpeed = -500.0f;
    // Gets gravity from project settings
    private float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

    private AnimatedSprite2D animatedSprite;
    private AudioStreamPlayer soundJump;

    public override void _Ready()
    {
        // Get references to the child nodes with null checks
        animatedSprite = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        soundJump = GetNodeOrNull<AudioStreamPlayer>("SoundJump");

        // Log warnings if nodes are not found
        if (animatedSprite == null)
            GD.PrintErr("AnimatedSprite2D node not found!");

        if (soundJump == null)
            GD.PrintErr("SoundJump node not found!");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add gravity
        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;

        // Handle jump (up key)
        if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
        {
            GD.Print("Jump pressed and is on floor!");
            // Only play sound if the node exists
            if (soundJump != null)
                soundJump.Play();
            velocity.Y = jumpSpeed;
        }

        // Get keyboard input directions (left and right)
        float direction = Input.GetAxis("ui_left", "ui_right");
        velocity.X = direction * speed;

        // Only control animation if the node exists
        if (animatedSprite != null)
        {
            if (direction > 0)
            {
                animatedSprite.FlipH = false;
                animatedSprite.Play();
            }
            else if (direction < 0)
            {
                animatedSprite.FlipH = true;
                animatedSprite.Play();
            }
            else
            {
                animatedSprite.Stop();
            }
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}