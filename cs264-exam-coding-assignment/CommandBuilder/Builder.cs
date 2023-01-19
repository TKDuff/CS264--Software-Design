using System;
using System.Collections.Generic;
namespace CommandBuilder
{

/// The 'Builder' interface
public interface IEmojiBuilder
{
    void setName();
    void setX();
    void setY();
    void setWidth();
    void setHeight();
    void setStyle();

 Emoji GetEmoji();
}


// The Concrete Mouth
public class Mouth : IEmojiBuilder
{
    Emoji objEmoji = new Emoji();

    public void setName()
    {
        objEmoji.featureName = "Mouth";
    }
    
    public void setX()
    {
        objEmoji.X = 100;
    }
    
    public void setY()
    {
        objEmoji.Y = 350;
    }

    public void setWidth()
    {
        objEmoji.Width = 300;
    }

    public void setHeight()
    {
        objEmoji.Height = 50;
    }
    
    public void setStyle()
    {
        objEmoji.Style = "fill=\"black\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
    }
    
    public Emoji GetEmoji()
    {
        return objEmoji;
    }
}


// The Concrete Mouth
public class LeftBrow : IEmojiBuilder
{
    Emoji objEmoji = new Emoji();

    public void setName()
    {
        objEmoji.featureName = "Left Brow";
    }
    
    public void setX()
    {
        objEmoji.X = 50;
    }
    
    public void setY()
    {
        objEmoji.Y = 150;
    }

    public void setWidth()
    {
        objEmoji.Width = 100;
    }

    public void setHeight()
    {
        objEmoji.Height = 20;
    }
    
    public void setStyle()
    {
        objEmoji.Style = "fill=\"black\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
    }
    
    public Emoji GetEmoji()
    {
        return objEmoji;
    }
}

public class RightBrow : IEmojiBuilder
{
    Emoji objEmoji = new Emoji();

    public void setName()
    {
        objEmoji.featureName = "Right Brow";
    }
    
    public void setX()
    {
        objEmoji.X = 350;
    }
    
    public void setY()
    {
        objEmoji.Y = 150;
    }

    public void setWidth()
    {
        objEmoji.Width = 100;
    }

    public void setHeight()
    {
        objEmoji.Height = 20;
    }
    
    public void setStyle()
    {
        objEmoji.Style = "fill=\"black\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
    }
    
    public Emoji GetEmoji()
    {
        return objEmoji;
    }
}

// The LeftEye
public class LeftEye : IEmojiBuilder
{
    Emoji objEmoji = new Emoji();

    public void setName()
    {
        objEmoji.featureName = "Left Eye";
    }
       
    public void setX()
    {
        objEmoji.X = 50;
    }
    
    public void setY()
    {
        objEmoji.Y = 250;
    }

    public void setWidth()      //hackish way to set radius
    {
        objEmoji.Width = 50;
    }

    public void setHeight()
    {
    }
    
    public void setStyle()
    {
        objEmoji.Style = "fill=\"black\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
    }
    
    public Emoji GetEmoji()
    {
        return objEmoji;
    }
}


// The RightEye
public class RightEye : IEmojiBuilder
{
    Emoji objEmoji = new Emoji();

    public void setName()
    {
        objEmoji.featureName = "Right Eye";
    }
       
    public void setX()
    {
        objEmoji.X = 400;
    }
    
    public void setY()
    {
        objEmoji.Y = 250;
    }

    public void setWidth()      //hackish way to set radius
    {
        objEmoji.Width = 50;
    }

    public void setHeight()
    {
    }
    
    public void setStyle()
    {
        objEmoji.Style = "fill=\"black\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
    }
    
    public Emoji GetEmoji()
    {
        return objEmoji;
    }
}

/// The 'Product' class
public class Emoji
{
    public string featureName {get; set;}
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string Style {get; set;}
    
    public Emoji()
    {

    }

 public string ShowInfo()
 {
    if(featureName == "Left Eye" || featureName == "Right Eye"){
        return $"<circle cx=\"{X}\" cy=\"{Y}\" r=\"{Width}\" {Style}/>";
    } else {
        return $"<rect x=\"{X}\" y=\"{Y}\" width=\"{Width}\" height=\"{Height}\" {Style} />";
    }
 }
 
 public void MoveHorizontal(bool moveLeft, int magnitude){
    if(moveLeft) {magnitude *= -1;}
    X += magnitude;
 }


}


/// The 'Director' class
public class EmojiCreator
{
    private IEmojiBuilder objBuilder;
    public EmojiCreator()
    {

    }
        
    public void CreateVehicle(IEmojiBuilder builder)
    {
        objBuilder = builder;
        objBuilder.setName();
        objBuilder.setX();
        objBuilder.setY();
        objBuilder.setWidth();
        objBuilder.setHeight();
        objBuilder.setStyle();
    }

 public Emoji GetEmoji()
 {
 return objBuilder.GetEmoji();
 }
}

}