#pragma checksum "C:\Users\SelfMade\source\repos\E-Shopper\Views\Shared\_AdminSideBar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2ade2d6836db603201811c27a3cfd1cc176415c9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__AdminSideBar), @"mvc.1.0.view", @"/Views/Shared/_AdminSideBar.cshtml")]
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
#line 1 "C:\Users\SelfMade\source\repos\E-Shopper\Views\_ViewImports.cshtml"
using E_Shopper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\SelfMade\source\repos\E-Shopper\Views\_ViewImports.cshtml"
using E_Shopper.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\SelfMade\source\repos\E-Shopper\Views\_ViewImports.cshtml"
using E_Shopper.Models.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\SelfMade\source\repos\E-Shopper\Views\_ViewImports.cshtml"
using E_Shopper.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\SelfMade\source\repos\E-Shopper\Views\_ViewImports.cshtml"
using E_Shopper.Models.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\SelfMade\source\repos\E-Shopper\Views\_ViewImports.cshtml"
using E_Shopper.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ade2d6836db603201811c27a3cfd1cc176415c9", @"/Views/Shared/_AdminSideBar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"979716eb68f2dda1b4cc506a727881851837b0b1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__AdminSideBar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- ========== Left Sidebar Start ========== -->
<div class=""vertical-menu"">

    <!-- LOGO -->
    <div class=""navbar-brand-box"">
        <a href=""index-2.html"" class=""logo logo-dark"">
            <span class=""logo-sm"">
                <img src=""assets/images/logo-sm.png""");
            BeginWriteAttribute("alt", " alt=\"", 281, "\"", 287, 0);
            EndWriteAttribute();
            WriteLiteral(" height=\"22\">\r\n            </span>\r\n            <span class=\"logo-lg\">\r\n                <img src=\"assets/images/logo-dark.png\"");
            BeginWriteAttribute("alt", " alt=\"", 414, "\"", 420, 0);
            EndWriteAttribute();
            WriteLiteral(" height=\"20\">\r\n            </span>\r\n        </a>\r\n\r\n        <a href=\"index-2.html\" class=\"logo logo-light\">\r\n            <span class=\"logo-sm\">\r\n                <img src=\"assets/images/logo-sm.png\"");
            BeginWriteAttribute("alt", " alt=\"", 618, "\"", 624, 0);
            EndWriteAttribute();
            WriteLiteral(" height=\"22\">\r\n            </span>\r\n            <span class=\"logo-lg\">\r\n                <img src=\"assets/images/logo-light.png\"");
            BeginWriteAttribute("alt", " alt=\"", 752, "\"", 758, 0);
            EndWriteAttribute();
            WriteLiteral(@" height=""20"">
            </span>
        </a>
    </div>

    <button type=""button"" class=""btn btn-sm px-3 font-size-16 header-item waves-effect vertical-menu-btn"">
        <i class=""fa fa-fw fa-bars""></i>
    </button>

    <div data-simplebar class=""sidebar-menu-scroll"">

        <!--- Sidemenu -->
        <div id=""sidebar-menu"">
            <!-- Left Menu Start -->
            <ul class=""metismenu list-unstyled"" id=""side-menu"">
                <li class=""menu-title"">Menu</li>

                <li>
                    <a href=""index-2.html"">
                        <i class=""uil-home-alt""></i><span class=""badge rounded-pill bg-primary float-end"">01</span>
                        <span>Dashboard</span>
                    </a>
                </li>
                <li>
                    <a href=""index-2.html"">
                        <i class=""uil-home-alt""></i><span class=""badge rounded-pill bg-primary float-end"">01</span>
                        <span>Dashboard</span>
        ");
            WriteLiteral("            </a>\r\n                </li>\r\n\r\n            </ul>\r\n        </div>\r\n        <!-- Sidebar -->\r\n    </div>\r\n</div>\r\n<!-- Left Sidebar End -->");
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
