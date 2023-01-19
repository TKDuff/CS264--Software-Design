using System;
using System.Collections.Generic;
using System.Linq;
//Caretaker class

class EmojiFeatureCaretaker{
    
    public static List<EmojiFeatureMemento> savedEmojiFeatures;   //list to store current EmojiFeatures on the canvas
    public static List<EmojiFeatureMemento> undidEmojiFeatures;   //list to store EmojiFeatures undid of of the canvas
    public int savedLength;
    public int undidLength;
    public string svgCode;     //string that stores svg code to write to html file

    //initialise lists
    public EmojiFeatureCaretaker(){
        savedEmojiFeatures = new List<EmojiFeatureMemento>();
        undidEmojiFeatures = new List<EmojiFeatureMemento>();
    }

    //adding EmojiFeature memento to canvas is simple 
    public void addEmojiFeature(EmojiFeatureMemento EmojiFeature){
        savedEmojiFeatures.Add(EmojiFeature);
    }

    
    //undo involves removing EmojiFeature memento from front of savedEmojiFeatures and moving it to front of undidEmojiFeatures
    //taking current EmojiFeature and moving it to storage
    public EmojiFeatureMemento Undo(){
        savedLength = savedEmojiFeatures.Count-1;
        if(savedLength == -1){      //if no EmojiFeatures on canvas to be undone
            Console.WriteLine("Cannot undo when no Emoji Features on canvas");
            return null;
        } else {
            Console.WriteLine(savedEmojiFeatures[savedLength].getSavedSvgLine() + " removed from canvas");
            undidEmojiFeatures.Add(savedEmojiFeatures[savedLength]);  //add EmojiFeature at top of savedEmojiFeatures to top of undidEmojiFeatures
            savedEmojiFeatures.RemoveAt(savedLength);          //remove EmojiFeature from top of savedEmojiFeatures
            return (savedLength > 0) ? savedEmojiFeatures[savedLength-1] : null;//check if savedEmojiFeature length is greater than 0, if not, no more EmojiFeatures on canvas therefore cannot return a EmojiFeature so return null  
        }
    }

    
    //Redo involves removing EmojiFeature memento from front of undidEmojiFeatures and moving it to front or savedEmojiFeatures
    //taking last undid EmojiFeature and putting it back on the canvas
    public EmojiFeatureMemento Redo() {
        undidLength = undidEmojiFeatures.Count-1;
        if(undidLength == -1){      //if no EmojiFeatures undone
            Console.WriteLine("Cannot redo, no Emoji Features to re-add");
            return null;
        } else {
            Console.WriteLine(undidEmojiFeatures[undidLength].getSavedSvgLine() + " added to canvas");
            savedEmojiFeatures.Add(undidEmojiFeatures[undidLength]);      //add EmojiFeature at top of undidEmojiFeatures to top of savedEmojiFeatures
            undidEmojiFeatures.RemoveAt(undidLength);              //remove EmojiFeature from top of undidEmojiFeatures
            return savedEmojiFeatures[savedEmojiFeatures.Count-1];
        }
    } 

    public string DisplayCanvas() {
        //should I call singleton object that contains styling here?
        if(savedEmojiFeatures.Count == 0){     //if nothing on canvas, cannot display anything
            return "Nothing to display";
        }

        svgCode = "";
        svgCode += "<svg id=\"emoji\" viewBox=\"0 0 500 500\" xmlns=\"http://www.w3.org/2000/svg\">\n\t<g>\n"; //boilerplate svg
        svgCode += "\t\t<circle cx=\"250\" cy=\"250\" r=\"250\" fill=\"yellow\" stroke-width=\"2\"/>"; //base svg circle
        svgCode +=  savedEmojiFeatures[savedEmojiFeatures.Count-1].getSavedSvgLine();     
        svgCode += "\n\t</g>\n</svg>"; //boilerplate svg
        return svgCode;
    }

    
}