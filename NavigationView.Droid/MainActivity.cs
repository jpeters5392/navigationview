using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

namespace NavigationViewDemo.Droid
{
	[Activity(Label = "NavigationView.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{
		private DrawerLayout drawerLayout;
		private NavigationView navigationView;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			this.drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
			this.navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

			// configure the toolbar
			this.SetSupportActionBar(toolbar);
			this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			this.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

			// add an event handler for when the user attempts to navigate
			this.navigationView.NavigationItemSelected += this.NavigateToItem;

			if (bundle == null)
			{
				SwitchFragment(new HomeFragment());	
			}
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			// An Activity's options appear in the toolbar/actionbar.  Our home button is considered part of those options,
			//  so we have to add this check here to open the nav drawer when it is clicked.
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					if (this.drawerLayout.IsDrawerOpen(GravityCompat.Start))
					{
						this.drawerLayout.CloseDrawer(GravityCompat.Start);
					}
					else
					{
						this.drawerLayout.OpenDrawer(GravityCompat.Start);
					}

					return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		public override void OnBackPressed()
		{
			// if the drawer is open when they press back, then just close the nav drawer and don't go anywhere.
			if (this.drawerLayout.IsDrawerOpen(GravityCompat.Start))
			{
				this.drawerLayout.CloseDrawers();
			}
			else
			{
				base.OnBackPressed();
			}
		}

		protected async void NavigateToItem(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			switch (e.MenuItem.ItemId)
			{
				case Resource.Id.homeFragment:
					SwitchFragment(new HomeFragment());
					return;
				case Resource.Id.secondFragment:
					SwitchFragment(new SecondFragment());
					return;
			}
		}

		private void SwitchFragment(Android.Support.V4.App.Fragment fragment)
		{
			this.SupportFragmentManager.BeginTransaction().SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen).Replace(Resource.Id.frameLayout, fragment).AddToBackStack(null).Commit();

			if (this.drawerLayout.IsDrawerOpen(GravityCompat.Start))
			{
				this.drawerLayout.CloseDrawers();
			}
		}

		#region OnDestroy
		protected override void OnDestroy()
		{
			if (this.navigationView != null)
			{
				this.navigationView.Dispose();
				this.navigationView = null;
			}

			if (this.drawerLayout != null)
			{
				this.drawerLayout.Dispose();
				this.drawerLayout = null;
			}

			base.OnDestroy();
		}
		#endregion
	}
}

