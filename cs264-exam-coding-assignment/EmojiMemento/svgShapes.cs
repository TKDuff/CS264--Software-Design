public abstract class EmojiFeature
{
    public string info;
    public int x;
    public int y;
    public int width;
    public int height;
    public int radius;
    public string style;

    public abstract string getSVG();
}

public class Mouth : EmojiFeature
{
    public Mouth()
    {
        this.x = 100;
        this.y = 350;
        this.width = 300;
        this.height = 50;
        this.style = "fill=\"black\"";
    }

    public override string getSVG(){
        return $"<rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" {style} />";
    }
}

public class LeftBrow : EmojiFeature
{
    public LeftBrow()
    {
        this.x = 50;
        this.y = 150;
        this.width = 100;
        this.height = 20;
        this.style = "fill=\"black\"";
    }

    public override string getSVG(){
        return $"<rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" {style} />";
    }
}

public class RightBrow : EmojiFeature
{
    public RightBrow()
    {
        this.x = 350;
        this.y = 150;
        this.width = 100;
        this.height = 20;
        this.style = "fill=\"black\"";
    }

    public override string getSVG(){
        return $"<rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" {style} />";
    }
}

public class LeftEye : EmojiFeature
{
    public LeftEye()
    {
        this.x = 50;
        this.y = 250;
        this.radius = 50;
        this.style = "fill=\"black\"";
    }

    public override string getSVG(){
        return $"<circle cx=\"{x}\" cy=\"{y}\" r=\"{radius}\" {style}/>";
    }
}


public class RightEye : EmojiFeature
{
    public RightEye()
    {
        this.x = 400;
        this.y = 250;
        this.radius = 50;
        this.style = "fill=\"black\"";
    }

    public override string getSVG(){
        return $"<circle cx=\"{x}\" cy=\"{y}\" r=\"{radius}\" {style}/>";
    }
}