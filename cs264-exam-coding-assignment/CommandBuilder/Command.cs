//Command class
using System;
using System.IO;
using System.Collections.Generic;

namespace CommandBuilder
{
    public interface ICommand
    {
        void execute();     //Add shape, will also be used as redo
        void unexecute();   //Remove shape, will also be used as undo   
    }

public class AddFeatureCommand : ICommand
{
    string feature;
    EmojiCreator createCommand = new EmojiCreator();
    Receiver receiver;

    public AddFeatureCommand(string feature, Receiver receiver) {
        this.feature = feature;
        this.receiver = receiver;
    }

    //Adds emoji to base FeatureList
    public void execute() {
        switch(feature)
        {
            case "mouth":
                createCommand.CreateVehicle((new Mouth()));
                break;
            case "left-eye":
                createCommand.CreateVehicle((new LeftEye()));
                break;
            case "right-eye":
                createCommand.CreateVehicle((new RightEye()));
                break;
            case "left-brow":
                createCommand.CreateVehicle((new LeftBrow()));
                break;
            case "right-brow":
                createCommand.CreateVehicle((new RightBrow()));
                break;
            default:
                break;
        }
        receiver.add(feature, createCommand.GetEmoji());
    }

    public void unexecute(){
        receiver.removeFeature(feature);
    }
}

public class RemoveFeatureCommand : ICommand
{
    string feature;
    EmojiCreator createCommand = new EmojiCreator();
    Receiver receiver;

    public RemoveFeatureCommand(string feature, Receiver receiver) {
        this.feature = feature;
        this.receiver = receiver;
    }

    //Adds emoji to base FeatureList
    public void execute() {
        receiver.removeFeature(feature);
    }

    public void unexecute(){
        switch(feature)
        {
            case "mouth":
                createCommand.CreateVehicle((new Mouth()));
                break;
            case "left-eye":
                createCommand.CreateVehicle((new LeftEye()));
                break;
            case "right-eye":
                createCommand.CreateVehicle((new RightEye()));
                break;
            case "left-brow":
                createCommand.CreateVehicle((new LeftBrow()));
                break;
            case "right-brow":
                createCommand.CreateVehicle((new RightBrow()));
                break;
            default:
                break;
        }
        receiver.add(feature, createCommand.GetEmoji());
    }
}

public class MoveFeatureHorizontalCommand : ICommand
{
    string feature;
    bool MoveLeft;
    int magnitude;
    Receiver receiver;

    public MoveFeatureHorizontalCommand(string feature, string direction, int magnitude, Receiver receiver)
    {
        this.feature = feature;
        MoveLeft = (direction == "left") ? true : false;
        this.magnitude = magnitude;
        this.receiver = receiver;
    }

    public void execute()
    {
        receiver.MoveEmojiFeatureHorizontal(feature,MoveLeft,magnitude);
    }

    public void unexecute()
    {
        receiver.MoveEmojiFeatureHorizontal(feature,MoveLeft,magnitude*-1);
    }
}


public class MoveFeatureVerticalCommand : ICommand
{
    string feature;
    bool MoveDown;
    int magnitude;
    Receiver receiver;

    public MoveFeatureVerticalCommand(string feature, string direction, int magnitude, Receiver receiver)
    {
        this.feature = feature;
        MoveDown = (direction == "down") ? true : false;
        this.magnitude = magnitude;
        this.receiver = receiver;
    }

    public void execute()
    {
        receiver.MoveEmojiFeatureVertical(feature,MoveDown,magnitude);
    }

    public void unexecute()
    {
        receiver.MoveEmojiFeatureVertical(feature,MoveDown,magnitude*-1);
    }
}


public class ChangeFeatureStyleCommand : ICommand
{
    string feature;
    char Style;
    Receiver receiver;

    public ChangeFeatureStyleCommand(string feature, char Style, Receiver receiver)
    {
        this.feature = feature;
        this.Style = Style;
        this.receiver = receiver;
    }

    public void execute()
    {
        receiver.ChangeFeatureStyle(feature,Style);
    }

    public void unexecute()
    {
        receiver.UnChangeFeatureStyle(feature);
    }
}
}