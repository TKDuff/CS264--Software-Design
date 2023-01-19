using static System.Console;
using System;
using System.Collections.Generic;

public class EmojiFeatureFactory {

    public EmojiFeatureFactory() {
    }
    public EmojiFeature generateEmojiFeature(String typeOfEmojiFeature){
        switch(typeOfEmojiFeature){
            case "mouth":
                return new Mouth();
            case "left-eye":
                return new LeftEye();
            case "right-eye":
                return new RightEye();
            case "left-brow":
                return new LeftBrow();
            case "right-brow":
                return new RightBrow();
            default:
                return null;
        }
    }
        
}