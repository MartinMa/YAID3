using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YAID3
{
	/// <summary>
	/// Provides functionality for loading ID3 tags from a list of paths.
	/// </summary>
	class Mp3FileInfoLoader
	{
		/// <summary>
		/// Event data for the <see cref="ProgressReport"/> event.
		/// </summary>
		public class ProgressReportEventArgs : EventArgs
		{
			/// <summary>
			/// Returns or sets the number of files.
			/// </summary>
			public int FileCount { get; private set; }

			/// <summary>
			/// Returns or sets the current path.
			/// </summary>
			public string CurrentPath { get; private set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="ProgressReportEventArgs"/> class.
			/// </summary>
			public ProgressReportEventArgs(int fileCount, string currentPath)
			{
				// Initialize the properties.
				FileCount = fileCount;
				CurrentPath = currentPath;
			}
		}

		/// <summary>
		/// Event handler delegate for the <see cref="ProgressReport"/> event.
		/// </summary>
		public delegate void ProgressReportEventHandler(object sender, ProgressReportEventArgs e);

		/// <summary>
		/// Event which is fired regularly to report progress.
		/// </summary>
		public event ProgressReportEventHandler ProgressReport = delegate { };

		/// <summary>
		/// Stores the loaded files.
		/// </summary>
		private Dictionary<string, Mp3FileInfo> mFiles = new Dictionary<string, Mp3FileInfo>(StringComparer.InvariantCultureIgnoreCase);

		/// <summary>
		/// Maximum allowed recursion depth.
		/// </summary>
		private int mMaxRecursionDepth;

		/// <summary>
		/// Initializes a new instance of the <see cref="Mp3FileInfoLoader"/> class.
		/// </summary>
		public Mp3FileInfoLoader(int maxRecursionDepth)
		{
			// Save the maximum recursion depth.
			mMaxRecursionDepth = maxRecursionDepth;
		}

		/// <summary>
		/// Returns the number of loaded files.
		/// </summary>
		public int Count
		{
			get
			{
				// Return the count.
				return mFiles.Count;
			}
		}

		/// <summary>
		/// Returns the loaded files.
		/// </summary>
		public IEnumerable<Mp3FileInfo> Results
		{
			get
			{
				// Return the results.
				return mFiles.Values;
			}
		}

		/// <summary>
		/// Recursively scans the specified paths.
		/// </summary>
		public void Run(IEnumerable<string> paths)
		{
			// Clear the results from the last run.
			mFiles.Clear();

			// Scan the paths.
			Run(paths, 0);
		}

		/// <summary>
		/// Internal scan worker.
		/// </summary>
		private void Run(IEnumerable<string> paths, int depth)
		{
			// Iterate through the paths.
			foreach (string path in paths)
			{
				// Check whether the current item is a directory.
				if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
				{
					// Descend if the limit hasn't been reached.
					if (depth < mMaxRecursionDepth)
					{
						Run(Directory.EnumerateFileSystemEntries(path), depth + 1);
					}
				}
				else
				{
					// Load the file if the extension is correct.
					if (Path.HasExtension(path) && Path.GetExtension(path).ToLowerInvariant().Equals(".mp3"))
					{
						if (!mFiles.ContainsKey(path))
						{
							mFiles.Add(path, new Mp3FileInfo(path));
						}
					}
				}

				// Report our progress.
				ProgressReport(this, new ProgressReportEventArgs(mFiles.Count, path));
			}
		}
	}
}
