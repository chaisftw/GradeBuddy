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
	public partial class AddAssessment : ContentPage
	{
		public AddAssessment ()
		{
			InitializeComponent ();
            DateTimePicker.Date = DateTime.Now;
            NameText.Text = "";
            WeightText.Text = "";
            MarkText.Text = "";
        }
    }
}