using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
//using System.Windows.Shapes; // Path conflicts with System.IO.Path... why do we need Shapes anyway?
using System.ComponentModel;

namespace YAID3
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Background worker used to load MP3 files.
		private BackgroundWorker mBackgroundLoader = new BackgroundWorker();
		private Mp3FileInfoLoader mFileLoader = new Mp3FileInfoLoader(25);

		// Collection which stores the loaded files.
		private ObservableCollection<Mp3FileInfo> mFiles = new ObservableCollection<Mp3FileInfo>();

		public MainWindow()
		{
			InitializeComponent();

			mBackgroundLoader.DoWork += new DoWorkEventHandler(mBackgroundLoader_DoWork);
			mBackgroundLoader.ProgressChanged += new ProgressChangedEventHandler(mBackgroundLoader_ProgressChanged);
			mBackgroundLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mBackgroundLoader_RunWorkerCompleted);
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
			mBackgroundLoader.RunWorkerAsync(e.Data.GetData(DataFormats.FileDrop));
		}

		private void mBackgroundLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			// TODO: Forget the background worker! Move proper threading code to Mp3FileInfoLoader class.
			try
			{
				//mFileLoader.ProgressReport += new Mp3FileInfoLoader.ProgressReportEventHandler(
				//	delegate(object sender2, Mp3FileInfoLoader.ProgressReportEventArgs e2) { mBackgroundLoader.ReportProgress(0, e2); });

				mFileLoader.Run((IEnumerable<string>)e.Argument);

				e.Result = true;
			}
			catch
			{
				e.Result = false;
			}
		}

		private void mBackgroundLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			// TODO: Show progress here...
		}

		private void mBackgroundLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!(bool)e.Result)
			{
				MessageBox.Show("Error!!!!!!!!!!!!!11111");
				return;
			}

			// Copy the results to the observable collection.
			foreach (Mp3FileInfo info in mFileLoader.Results)
			{
				if (!mFiles.Contains(info))
				{
					mFiles.Add(info);
				}
			}
			
			// Give some feedback.
			MessageBox.Show(string.Format("Done! {0} files imported!", mFileLoader.Count));
		}
	}
}
