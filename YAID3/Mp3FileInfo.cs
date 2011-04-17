using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YAID3
{
	class Mp3FileInfo : IEquatable<Mp3FileInfo>
	{
		// Name of the file.
		private string mFileName;

		// Map that stores the tags.
		private Dictionary<string, Id3Tag> mTags = new Dictionary<string, Id3Tag>(StringComparer.InvariantCultureIgnoreCase);

		// Initializes a new instance.
		public Mp3FileInfo(string fileName)
		{
			// Save the name of the file.
			mFileName = fileName;

			// TODO: Parse file using favorite ID3 library...
			throw new NotImplementedException();
		}

		// Returns the name of the file.
		public string FileName
		{
			get
			{
				// Return the name of the file.
				return mFileName;
			}
		}

		// Returns or sets the specified tag.
		public Id3Tag this[string name]
		{
			get
			{
				// Return the tag.
				return mTags[name];
			}

			set
			{
				// Set the tag.
				mTags[name] = value;
			}
		}

		// Determines whether this instance represents the same file as the other instance.
		public bool Equals(Mp3FileInfo other)
		{
			// Compare the names of the files.
			return mFileName.Equals(other.mFileName, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
