//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("GitHub.Awesome.View.Pages.RepositoryListPage.xaml", "View/Pages/RepositoryListPage.xaml", typeof(global::GitHub.Awesome.View.Pages.RepositoryListPage))]

namespace GitHub.Awesome.View.Pages {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("View/Pages/RepositoryListPage.xaml")]
    public partial class RepositoryListPage : global::GitHub.Awesome.View.Base.BaseContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView lstRepositories;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(RepositoryListPage));
            lstRepositories = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "lstRepositories");
        }
    }
}
