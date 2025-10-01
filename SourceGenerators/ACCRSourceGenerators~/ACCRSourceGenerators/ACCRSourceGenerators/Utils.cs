using System.Text;

namespace ACCRSourceGenerators;

public static class Utils
{
    public static StringBuilder Indent(StringBuilder builder, int level)
    {
        for (var i = 0; i < level; i++)
        {
            builder.Append("    ");
        }
        return builder;
    }
}