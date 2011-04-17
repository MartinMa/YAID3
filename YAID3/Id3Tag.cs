using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YAID3
{
	/// <summary>
	/// Represents an abstract base class for ID3 tags.
	/// </summary>
	abstract class Id3Tag
	{
		// Name of the tag.
		private string mName;

		/// <summary>
		/// Initializes a new instance of the <see cref="Id3Tag"/> class.
		/// </summary>
		public Id3Tag(string name)
		{
			// Save the name of the tag.
			mName = name;
		}

		/// <summary>
		/// Returns the name of the tag.
		/// </summary>
		public string Name
		{
			get
			{
				// Return the name of the tag.
				return mName;
			}
		}
	}
}
