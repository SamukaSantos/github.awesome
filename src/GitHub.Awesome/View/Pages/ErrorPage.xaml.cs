
using System;
using GitHub.Awesome.View.Pages.Interfaces;
using Xamarin.Forms;

namespace GitHub.Awesome.View.Pages
{
	public partial class ErrorPage : ContentPage, IErrorPage
	{
		#region Constructor

		public ErrorPage()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		public string ErrorMessage
		{
			get { return lblErrorMessage.Text; }
			set { lblErrorMessage.Text = value; }
		}

		#endregion

		#region Methods

		protected override void OnAppearing()
		{
			base.OnAppearing();

			btnError.Clicked += Close;
            
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			btnError.Clicked -= Close;
		}

		private async void Close(object sender, EventArgs eventArgs)
		{
			await Navigation.PopModalAsync();
		}

		#endregion
	}
}
