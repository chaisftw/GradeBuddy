﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GradeBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUnit : ContentPage
	{
		public AddUnit ()
		{
			InitializeComponent ();
            SemesterPicker.SelectedIndex = 0;
            YearPicker.SelectedIndex = 0;
        }

        static void Reset()
        {
            
        }
	}
}