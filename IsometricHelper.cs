using System.Drawing;

class IsometricHelper
{
    public static Point CartesianToIsometric(Point cartPt)
    {
        int isoX = cartPt.X - cartPt.Y;
        int isoY = (cartPt.X + cartPt.Y) / 2;
        return new Point(isoX, isoY);
    }

    public static Point IsometricToCartesian(Point isoPt)
    {
        int cartX = (2 * isoPt.Y + isoPt.X) / 2;
        int cartY = (2 * isoPt.Y - isoPt.X) / 2;
        return new Point(cartX, cartY);
    }
}
