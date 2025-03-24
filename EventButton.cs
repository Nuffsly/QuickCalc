using System;
using Godot;

namespace QuickCalc;

public partial class EventButton : Button
{
    [Signal]
    public delegate void ValueButtonsInputEventHandler(int value);
    
    [Signal]
    public delegate void MouseEnteredValueButtonEventHandler(int value);
    
    [Signal]
    public delegate void MouseExitedValueButtonEventHandler(int value);
    
    [Export]
    public string ButtonValue { get; set; }

    public override void _Ready()
    {
        Pressed += OnButtonPressed;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    private void OnButtonPressed()
    {
        int.TryParse(ButtonValue, out var value);
        EmitSignal(SignalName.ValueButtonsInput, value);
    }

    private void OnMouseEntered()
    {
        int.TryParse(ButtonValue, out var value);
        EmitSignal(SignalName.MouseEnteredValueButton, value);
    }

    private void OnMouseExited()
    {
        int.TryParse(ButtonValue, out var value);
        EmitSignal(SignalName.MouseExitedValueButton, value);
    }
}