using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YAID3
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private HashSet<Mp3FileInfo> mFiles = new HashSet<Mp3FileInfo>();

		public MainWindow()
		{
			InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			// This can't be done any earlier than this:
			GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
		}

		private void ctlFiles_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
			{
				e.Effects = DragDropEffects.Copy;
			}
			else
			{
				e.Effects = DragDropEffects.None;
			}

			e.Handled = true;
		}

		private void ctlFiles_Drop(object sender, DragEventArgs e)
		{
			// TODO: Make fool-proof and asynchronous!
			string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

			foreach (string path in paths)
			{
				if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
				{
					// TODO: Recursion...
				}
				else
				{
					if (!mFiles.Add(new Mp3FileInfo(path)))
					{
						MessageBox.Show(string.Format("The file \"{0}\" is already present.", path));
					}
				}
			}

			// TODO: Copy to list view after worker thread did its thing.
			foreach (Mp3FileInfo file in mFiles)
			{
				ctlFiles.Items.Add(file);
				dataGrid1.Items.Add(file);
			}
		}
	}
}
