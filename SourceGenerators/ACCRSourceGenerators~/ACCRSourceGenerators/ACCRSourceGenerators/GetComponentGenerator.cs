using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ACCRSourceGenerators;

[Generator]
public class GetComponentGenerator : ISourceGenerator
{
    // Generated Attribute
    private const string _attributeText = """

                                          using System;

                                          [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
                                          internal class GetComponentAttribute : Attribute
                                          {
                                              public enum TargetType
                                              {
                                                  This = 0,
                                                  Parent = 1,
                                                  Child = 2,
                                              }
                                          
                                              public GetComponentAttribute(TargetType targetType = TargetType.This)
                                              {
                                              }
                                          }

                                          """;

    // ISourceGenerator Method
    // Generates GetComponent Attribute and Registers SyntaxReceiver to Syntax Notifications
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization
            (i => i.AddSource("GetComponentAttribute_g.cs", _attributeText));
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    // ISourceGenerator Method
    // Foreach GetComponent Attribute Generate InitializeComponents() method and add code to source
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxContextReceiver is not SyntaxReceiver receiver)
            return;

        // Sets the attribute syntax
        INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName("GetComponentAttribute");

        // Iterates through all attributes and processes each class containing it, generating code and adding to source
        foreach (IGrouping<INamedTypeSymbol, IFieldSymbol> group in receiver.Fields
                     .GroupBy<IFieldSymbol, INamedTypeSymbol>(f => f.ContainingType,
                         SymbolEqualityComparer.Default))
        {
            // Generates code
            var classSource = ProcessClass(group.Key, group, attributeSymbol);
            
            // Adds to source
            context.AddSource($"{group.Key.Name}_Components_g.cs", SourceText.From(classSource, Encoding.UTF8));
        }
    }

    // Adds InitializeComponents() to class with GetComponent attribute, generates code for each field 
    private string ProcessClass(INamedTypeSymbol classSymbol, IEnumerable<IFieldSymbol> fields,
        ISymbol attributeSymbol)
    {
        var source = new StringBuilder($$"""


                                         public partial class {{classSymbol.Name}} 
                                         {
                                         private void InitializeComponents()
                                         {

                                         """);

        foreach (IFieldSymbol fieldSymbol in fields)
        {
            ProcessField(source, fieldSymbol, attributeSymbol);
        }

        source.Append("}\n\n}");
        return source.ToString();
    }

    // Generates the statement for GetComponent()
    private void ProcessField(StringBuilder source, IFieldSymbol fieldSymbol, ISymbol attributeSymbol)
    {
        var fieldName = fieldSymbol.Name;
        ITypeSymbol fieldType = fieldSymbol.Type;

        AttributeData attributeData = fieldSymbol.GetAttributes().Single(ad =>
            ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));

        var methodType = ProcessAttribute(attributeData);

        source.AppendLine($@"{fieldName} = {methodType}<{fieldType}>();");
    }

    // Generates method type for GetComponent()
    private string ProcessAttribute(AttributeData attributeData)
    {
        var stringBuilder = new StringBuilder("GetComponent");
        if (attributeData.ConstructorArguments.Length > 0 &&
            int.TryParse(attributeData.ConstructorArguments[0].Value.ToString(), out var enumValue))
        {
            if (enumValue == 1) stringBuilder.Append("InParent");
            if (enumValue == 2) stringBuilder.Append("InChildren");
        }

        return stringBuilder.ToString();
    }
    
    // Navigates the syntax tree and finds all fields with [GetComponent]
    private class SyntaxReceiver : ISyntaxContextReceiver
    {
        public List<IFieldSymbol> Fields { get; } = [];

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is not FieldDeclarationSyntax { AttributeLists.Count: > 0 } fieldDeclarationSyntax) return;
            foreach (var fieldSymbol in fieldDeclarationSyntax.Declaration.Variables
                         .Select(variable => 
                             context.SemanticModel
                                 .GetDeclaredSymbol(variable) as IFieldSymbol)
                         .Where(fieldSymbol => 
                             IsDerivedFrom(fieldSymbol?.ContainingType.BaseType, "MonoBehaviour") &&
                             IsDerivedFrom(fieldSymbol?.ContainingType.BaseType, "Component") &&
                             fieldSymbol != null &&
                             fieldSymbol.GetAttributes()
                                 .Any(ad => ad.AttributeClass?.ToDisplayString() == "GetComponentAttribute")))
            {
                Fields.Add(fieldSymbol);
            }
        }

        // Util method to check if contains type
        private bool IsDerivedFrom(INamedTypeSymbol baseType, string targetType)
        {
            while (baseType != null)
            {
                if(baseType.Name == targetType)
                    return true;
                
                baseType = baseType.BaseType;
            }
            return false;
        }
    }
}


