//Command class
using System;
using System.IO;
using System.Collections.Generic;


public interface ICommand
{
    void execute();     //Add shape, will also be used as redo
    void unexecute();   //Remove shape, will also be used as undo   
}

// Add Shape Command - it is a ConcreteCommand Class (extends ICommand)
// This adds a Shape to the Canvas (Receiver) as the "execute" action
public class AddShapeCommand : ICommand
{
    Shape shape;
    Canvas canvas;
    
    public AddShapeCommand(Shape s, Canvas c)
    {
        shape = s;
        canvas = c;
    }
    
    // Adds a shape to the canvas class (receiver) as "execute" action, this will be also used by invoker as redo
    public void execute()
    {
        canvas.Add(shape);
    }
    
    // Removes a shape from the canvas class (receiver) as "unexecute" action, this will be also used by invoker as undo
    public void unexecute()
    {
        shape = canvas.Remove();
    }

}