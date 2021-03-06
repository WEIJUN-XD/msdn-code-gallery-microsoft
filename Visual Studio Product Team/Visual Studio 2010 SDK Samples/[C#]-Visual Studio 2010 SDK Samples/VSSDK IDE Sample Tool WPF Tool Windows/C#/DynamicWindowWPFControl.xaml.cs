﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace Microsoft.Samples.VisualStudio.IDE.ToolWindow
{
    /// <summary>
    /// Interaction logic for DynamicWindowWPFControl.xaml
    /// </summary>
    public partial class DynamicWindowWPFControl : UserControl
    {
        public DynamicWindowWPFControl()
        {
            InitializeComponent();
        }

        private WindowStatus currentState = null;
		/// <summary>
		/// This is the object that will keep track of the state of the IVsWindowFrame
		/// that is hosting this control. The pane should set this property once
		/// the frame is created to enable us to stay up to date.
		/// </summary>
		public WindowStatus CurrentState
		{
			get { return currentState; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				currentState = value;
				// Subscribe to the change notification so we can update our UI
				currentState.StatusChange += new EventHandler<EventArgs>(this.RefreshValues);
				// Update the display now
				this.RefreshValues(this, null);
			}
		}

		/// <summary>
		/// This method is the call back for state changes events
		/// </summary>
		/// <param name="sender">Event senders</param>
		/// <param name="arguments">Event arguments</param>
		private void RefreshValues(object sender, EventArgs arguments)
		{
            this.xText.Text = currentState.X.ToString(CultureInfo.CurrentCulture);
            this.yText.Text = currentState.Y.ToString(CultureInfo.CurrentCulture);
            this.widthText.Text = currentState.Width.ToString(CultureInfo.CurrentCulture);
            this.heightText.Text = currentState.Height.ToString(CultureInfo.CurrentCulture);
			this.dockedCheckBox.IsChecked = currentState.IsDockable;
            this.InvalidateVisual();
		}

    }
}
