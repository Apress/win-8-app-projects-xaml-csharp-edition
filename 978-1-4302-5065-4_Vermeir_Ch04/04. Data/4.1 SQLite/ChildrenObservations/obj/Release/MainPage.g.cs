﻿

#pragma checksum "C:\Users\Nico\Documents\Visual Studio 2012\Projects\ChildrenObservations\ChildrenObservations\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7AC36CBFAF7568D73A28A94C8C6CAD1A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChildrenObservations
{
    partial class MainPage : global::ChildrenObservations.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 21 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Button_Click_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 22 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Button_Click_2;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 46 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.GridView_ItemClick_1;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 43 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


