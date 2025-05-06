namespace LudoAPI.Services.Utils;

public class BoardMapLibrary
{
    public static readonly string[,] StandardBoard = new string[15, 15]
    {
        { "r", "r", "r", "r", "r", "r", "R", "gD-R", "D", "g", "g", "g", "g", "g", "g" },
        { "r", "", "", "", "", "r", "D", "gU", "gSD", "g", "", "", "", "", "g" },
        { "r", "", "rH", "rH", "", "r", "U", "gD", "D", "g", "", "gH", "gH", "", "g" },
        { "r", "", "rH", "rH", "", "r", "U", "gD", "D", "g", "", "gH", "gH", "", "g" },
        { "r", "", "", "", "", "r", "U", "gD", "D", "g", "", "", "", "", "g" },
        { "r", "r", "r", "r", "r", "r", "U", "gD", "DR", "g", "g", "g", "g", "g", "g" },
        { "R", "rSR", "R", "R", "R", "RU", "", "gE", "", "R", "R", "R", "R", "R", "D" },
        { "rR-U", "rR", "rR", "rR", "rR", "rR", "rE", "", "yE", "yL", "yL", "yL", "yL", "yL", "yL-D" },
        { "U", "L", "L", "L", "L", "L", "", "bE", "", "DL", "L", "L", "L", "ySL", "L" },
        { "b", "b", "b", "b", "b", "b", "UL", "bU", "D", "y", "y", "y", "y", "y", "y" },
        { "b", "", "", "", "", "b", "U", "bU", "D", "y", "", "", "", "", "y" },
        { "b", "", "bH", "bH", "", "b", "U", "bU", "D", "y", "", "yH", "yH", "", "y" },
        { "b", "", "bH", "bH", "", "b", "U", "bU", "D", "y", "", "yH", "yH", "", "y" },
        { "b", "", "", "", "", "b", "bSU", "bU", "D", "y", "", "", "", "", "y" },
        { "b", "b", "b", "b", "b", "b", "U", "bU-L", "L", "y", "y", "y", "y", "y", "y" },
    };
}