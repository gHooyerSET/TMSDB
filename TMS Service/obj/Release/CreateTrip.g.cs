﻿#pragma checksum "..\..\CreateTrip.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "30241E76FF77D4F5B13103AAF4A8CDFEBD21268EAFC3303F983DEDB20968EFEC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TMS_Service;


namespace TMS_Service {
    
    
    /// <summary>
    /// CreateTrip
    /// </summary>
    public partial class CreateTrip : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\CreateTrip.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCarrier;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\CreateTrip.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbStartCity;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\CreateTrip.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbEndCity;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\CreateTrip.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbType;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\CreateTrip.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateTrip;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TMS Service;component/createtrip.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CreateTrip.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cbCarrier = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\CreateTrip.xaml"
            this.cbCarrier.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbCarrier_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbStartCity = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.cbEndCity = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.cbType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.btnCreateTrip = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\CreateTrip.xaml"
            this.btnCreateTrip.Click += new System.Windows.RoutedEventHandler(this.btnCreateTrip_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

