#pragma checksum "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78cdfbdb6896da45cbf5c63ac7d6b9d03e82fea9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Participant_ViewDataPoints), @"mvc.1.0.view", @"/Views/Participant/ViewDataPoints.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Dima\source\project\MedicalWeb3\Views\_ViewImports.cshtml"
using MedicalWeb3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Dima\source\project\MedicalWeb3\Views\_ViewImports.cshtml"
using MedicalWeb3.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"78cdfbdb6896da45cbf5c63ac7d6b9d03e82fea9", @"/Views/Participant/ViewDataPoints.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7a53095612e0496ec99755b066470f12ba72a90", @"/Views/_ViewImports.cshtml")]
    public class Views_Participant_ViewDataPoints : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
  
    ViewData["Title"] = "DataPoints";
    var model = Model;// as Tuple<List<MedicalClient.DataPoint>, string>;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">Data Points</h1>
    <table style=""width:100%; border:1px solid black;"">
        <tr>
            <td>Date</td>
            <td>Heartbeat</td>
            <td>Temperature</td>
            <td>Distal Pressure</td>
            <td>Sistal Pressure</td>
            <td>Cough</td>
            <td>Headache</td>
            <td>Dizzines</td>
            <td>Nausea</td>
            <td>Rheum</td>
            <td>Weakness</td>
            <td>Self Feeling</td>
        </tr>
");
#nullable restore
#line 23 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
         foreach (var point in model.Item1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 26 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Time.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 27 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.HeartBeat);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 28 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Temperature);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 29 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.DistalPressure);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 30 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.SistalPressure);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 31 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Cough);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 32 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Headache);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 33 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Dizziness);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 34 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Nausea);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 35 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Rheum);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 36 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.Weakness);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 37 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
               Write(point.SelfFeeling);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 39 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 1309, "\"", 1356, 2);
            WriteAttributeValue("", 1316, "/Participant/DownloadById/", 1316, 26, true);
#nullable restore
#line 41 "C:\Users\Dima\source\project\MedicalWeb3\Views\Participant\ViewDataPoints.cshtml"
WriteAttributeValue("", 1342, model.Item2, 1342, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Download CSV</a>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591