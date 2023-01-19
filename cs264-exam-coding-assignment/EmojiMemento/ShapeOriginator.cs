using static System.Console;
using System;
using System.Collections.Generic;

public class EmojiFeatureOriginator{

    private string svgLine; //EmojiFeatures svg code to be put into html file

    private Dictionary<string, EmojiFeature> EmojiFeatureList = new Dictionary<string, EmojiFeature>();

    //Rectangle, ellipse and line all take four integer paramters, use switch case to determine which one to creat in set method
    //set method takes parameters from program, determine EmojiFeature to create, fill in paramters to svg code and information lines
    public void setAdd(string name, EmojiFeature EmojiFeature){
        EmojiFeatureList.Add(name, EmojiFeature);
        Console.WriteLine($"Added {name}, style A");
    }

    
    public void setRemove(string name){
        EmojiFeatureList.Remove(name);
        Console.WriteLine($"Removed {name}");
    }

    public void setMove(string name, string direction ,int distance){
        if(direction == "up" || direction == "down")
        {
            distance = (direction == "down") ? distance*-1 : distance;
            EmojiFeatureList[name].y += distance;
        } else {
            distance = (direction == "left") ? distance*-1 : distance;
            EmojiFeatureList[name].x += distance;
        }
        Console.WriteLine($"Moved {name} {direction} by {distance}");
    }

    public void setStyle(string name, char Style){
        string styleChoosen = null;
        switch(Style){
                case 'A':
                    styleChoosen = "fill=\"black\"";
                    break;
                case 'B':
                    styleChoosen = "fill=\"green\"";
                    break;
                case 'C':
                    styleChoosen = "fill=\"blue\"";
                    break;
                default:
                    break;
            }
        EmojiFeatureList[name].style = styleChoosen;
        Console.WriteLine($"{name} style set to {Style}");
    }


    
    //method that takes a snapshot or originator object, turns it into memento, taking paramters of svgLine and info
    public EmojiFeatureMemento storeEmojiFeatureInMemento() {
        svgLine = "";
        foreach(var item in EmojiFeatureList)
        {
            svgLine += "\n\t\t" + item.Value.getSVG() + "\n";
        }
        return new EmojiFeatureMemento(svgLine);
    }

    //method which program calls to traverse snapshot of EmojiFeatures in caretaker using undo and redo, argument m is EmojiFeature caretaker returns when undoing or redoing a EmojiFeature
    public void restoreFromMemento(EmojiFeatureMemento m){
        if(m is null){      //in case no undid all EmojiFeatures on canvas no EmojiFeatures can be returned therefore set svgLine and info to acknowledge this
            svgLine = "No Emoji Feature on Canvas";
        } else {
        svgLine = m.getSavedSvgLine();  //get new undid/redid EmojiFeatures information 
        }
    }

    //getters for EmojiFeature object parameters
    public string getSvgLine() { return this.svgLine;}
}