//Invoker class that runs commands client gives
using System;
using System.IO;
using System.Collections.Generic;

//Invoker stores and issues commands, therefore keeps track of commands using two stack, undo and redo. 
//When user undos a command, invoker must undo shape from storage and also undo shape off of FeatureList
namespace CommandBuilder
{
public class Invoker
{
    private Stack<ICommand> undo;    //store given commands so far (Shapes on the FeatureList)
    private Stack<ICommand> redo; 

    public Invoker()
    {
        undo = new Stack<ICommand>();   //stores commands for shapes which are currently on the FeatureList
        redo = new Stack<ICommand>();   //stores commands for shapes which were undone off the FeatureList
    }

    public void runCommand(ICommand command)    //This makes the invoker execute the command
    {
        undo.Push(command);     //save command to undo stack, so it can be undone
        command.execute();      //execute the command
    }



    //Removes command from both Invokers undo stack but also from the FeatureList (receiver)
    public void Undo()
    {
        if(undo.Count > 0)  //if shapes to be undone
        {
            Console.WriteLine("Undoing");
            ICommand c = undo.Pop();    //pop command off top of undo stack
            c.unexecute();              //issue command to remove popped shape from top of FeatureList
            redo.Push(c);               //push this command to top of redo stack
        } else {
            Console.WriteLine("Cannot undo, no commands to undo");
        }
    }


    //Removes command from both Invokers undo stack but also from the FeatureList (receiver)
    public void Redo()
    {
        if (redo.Count > 0)     //if shapes to be redone
        {
            Console.WriteLine("Redoing");
            ICommand c = redo.Pop();    //pop command off top of redo stack
            c.execute();                //issue command to add popped shape to top of FeatureList
            undo.Push(c);               //push this command to top of undo stack
        } else {
            Console.WriteLine("Cannot redo, nothing to re-add to FeatureList");
        }
    }
}
}