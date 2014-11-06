using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using RegionKillerAnalyzer;

namespace RegionKillerAnalyzer.Test
{
    [TestClass]
    public class KillRegionDiagnosticAnalyzerTests : CodeFixVerifier
    {
        
        [TestMethod]
        public void When_Region_Inside_of_Method_Will_Find_Correctly()
        {
           
            var sourceTest = @"
                public class Test
                {
                    public void MyMethod()
                    {
                        var number = 1;

                        #region My Region
                            // comment
                        #endregion
                    }
                }";

            var expected = new DiagnosticResult
            {
                Id = RegionKillerAnalyzerAnalyzer.DiagnosticId,
                Message = String.Format("Please remove Regions, they are a code smell.  Create better factored code."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                        new[] {
                            new DiagnosticResultLocation("Test0.cs", 8, 25)
                        }
            };

            VerifyCSharpDiagnostic(sourceTest, expected);

        }

        [TestMethod]
        public void When_No_Region_Inside_of_Method_Will_Not_Find_Correctly()
        {

            var sourceTest = @"
                public class Test
                {
                    public void MyMethod()
                    {
                        var number = 1;

                            // comment
                    }
                }";
            
            VerifyCSharpDiagnostic(sourceTest, new DiagnosticResult[0]);

        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new RegionKillerAnalyzerCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new RegionKillerAnalyzerAnalyzer();
        }
    }
}