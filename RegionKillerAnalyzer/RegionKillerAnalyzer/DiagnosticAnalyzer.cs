using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RegionKillerAnalyzer
{

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RegionKillerAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        #region "start region in class"

        #endregion

        public const string DiagnosticId = "RegionKillerAnalyzer";
        internal const string Title = "Regions are a code smell.";
        internal const string MessageFormat = "Please remove Regions, they are a code smell.  Create better factored code.";
        internal const string Category = "Naming";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            //context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);

            context.RegisterSyntaxNodeAction(NodeAction, SyntaxKind.RegionDirectiveTrivia);
           

            #region "start region in method"

            #endregion
        }

        private void NodeAction(SyntaxNodeAnalysisContext context)
        {
            var myNode = context.Node as RegionDirectiveTriviaSyntax;
            var location = myNode.GetLocation();

            var diagnostic = Diagnostic.Create(Rule, location);
            var message = diagnostic.GetMessage();

            context.ReportDiagnostic(diagnostic);

        }

    }
}
