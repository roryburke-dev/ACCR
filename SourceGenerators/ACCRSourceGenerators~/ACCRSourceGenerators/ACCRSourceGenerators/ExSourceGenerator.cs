using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;

namespace ACCRSourceGenerators;

[Generator]
public class ExSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var compilationProvider = context.CompilationProvider;
        
        context.RegisterSourceOutput(compilationProvider, (ctx, compilation) =>
        {
            if (compilation.AssemblyName == "Assembly-CSharp")
            {
                ctx.AddSource("ExSourceGenerator.g.cs", GenerateSouce());
            }
        });
    }

    SourceText GenerateSouce()
    {
        using var sourceStream = new StringWriter();
        using var codeWriter = new IndentedTextWriter(sourceStream);
        
        codeWriter.WriteLine("using System;");
        codeWriter.WriteLine("namespace ExSourceGenerator");
        codeWriter.WriteLine("{");
        codeWriter.Indent++;
        
        codeWriter.WriteLine("public static class ExSourceGenerated");
        codeWriter.WriteLine("{");
        codeWriter.Indent++;
        
        codeWriter.WriteLine("public static string GetTestText()");
        codeWriter.WriteLine("{");
        codeWriter.Indent++;
        
        codeWriter.WriteLine("return \"Hello From Incremental Generator\";");
        
        codeWriter.Indent--;
        codeWriter.WriteLine("}");
        
        codeWriter.Indent--;
        codeWriter.WriteLine("}");
        
        codeWriter.Indent--;
        codeWriter.WriteLine("}");
        
        return SourceText.From(sourceStream.ToString(), Encoding.UTF8);
    }
}