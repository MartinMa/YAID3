using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdSharp.Tagging.ID3v2;

namespace YAID3
{
	/// <summary>
	/// Provides functionality for reading and writing ID3 tags.
	/// </summary>
	class Mp3FileInfo : IEquatable<Mp3FileInfo>
	{
		// Name of the file.
		private string mFileName;

		// Tag.
		private ID3v2Tag mTag;

		/// <summary>
		/// Initializes a new instance of the <see cref="Mp3FileInfo"/> class.
		/// </summary>
		public Mp3FileInfo(string fileName)
		{
			// Save the name of the file.
			mFileName = fileName;

			// Parse the file using the tag library.
			mTag = new ID3v2Tag(fileName);
		}

		/// <summary>
		/// Returns the name of the file.
		/// </summary>
		public string FileName
		{
			get
			{
				// Return the name of the file.
				return mFileName;
			}
		}

		/// <summary>
		/// Returns or sets the artist.
		/// </summary>
		public string Artist
		{
			get
			{
				// Return the artist.
				return mTag.Artist;
			}

			set
			{
				// Set the artist.
				mTag.Artist = value;
			}
		}

		/// <summary>
		/// Returns or sets the album.
		/// </summary>
		public string Album
		{
			get
			{
				// Return the album.
				return mTag.Album;
			}

			set
			{
				// Set the album.
				mTag.Album = value;
			}
		}

		/// <summary>
		/// Returns or sets the disc number.
		/// </summary>
		public string DiscNumber
		{
			get
			{
				// Return the disc number.
				return mTag.DiscNumber;
			}

			set
			{
				// Set the disc number.
				mTag.DiscNumber = value;
			}
		}

		/// <summary>
		/// Returns or sets the track number.
		/// </summary>
		public string TrackNumber
		{
			get
			{
				// Return the track number.
				return mTag.TrackNumber;
			}

			set
			{
				// Set the track number.
				mTag.TrackNumber = value;
			}
		}

		/// <summary>
		/// Determines whether this instance represents the same file as the other instance.
		/// </summary>
		public bool Equals(Mp3FileInfo other)
		{
			// Compare the names of the files.
			return mFileName.Equals(other.mFileName, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
