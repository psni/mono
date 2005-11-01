//
// System.Drawing.StringFormat.cs
//
// Authors:
//   Dennis Hayes (dennish@Raytek.com)
//   Miguel de Icaza (miguel@ximian.com)
//   Jordi Mas i Hernandez (jordi@ximian.com)
//
// Copyright (C) 2002 Ximian, Inc (http://www.ximian.com)
//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Drawing.Text;

namespace System.Drawing {
	/// <summary>
	/// Summary description for StringFormat.
	/// </summary>
	public sealed class StringFormat : MarshalByRefObject, IDisposable, ICloneable {
		
		
		private CharacterRange [] _charRanges;
		private StringAlignment _alignment;
		private StringAlignment _lineAlignment;
		private HotkeyPrefix _hotkeyPrefix;
		private StringFormatFlags _flags;
		private StringDigitSubstitute _digitSubstituteMethod;
		private int _digitSubstituteLanguage;
		private StringTrimming _trimming;
		
		private float _firstTabOffset;
		private float [] _tabStops;
		
		#region Constructors

		public StringFormat() : this(0, 0) {					   
		}
		
		public StringFormat(StringFormatFlags options) : this(options,0) {
		}
		
		public StringFormat(StringFormatFlags options, int lang) {
			_alignment = StringAlignment.Near;
			_digitSubstituteLanguage = lang;
			_digitSubstituteMethod = StringDigitSubstitute.User;
			_flags = options;
			_hotkeyPrefix = HotkeyPrefix.None;
			_lineAlignment = StringAlignment.Near;
			_trimming = StringTrimming.Character;
		}
		
		public StringFormat (StringFormat source) {
			if (source == null)
				throw new ArgumentNullException("format");

			_alignment = source.LineAlignment;
			_digitSubstituteLanguage = source.DigitSubstitutionLanguage;
			_digitSubstituteMethod = source.DigitSubstitutionMethod;
			_flags = source.FormatFlags;
			_hotkeyPrefix = source.HotkeyPrefix;
			_lineAlignment = source.LineAlignment;
			_trimming = source.Trimming;
		}


		#endregion

		#region IDisposable

		public void Dispose() {	
		}

		#endregion

		#region Public properties

		public StringAlignment Alignment {
			get {
				return _alignment;
			}

			set {
				_alignment = value;
			}
		}

		public StringAlignment LineAlignment {
			get {
				return _lineAlignment;
			}
			set {
				_lineAlignment = value;
			}
		}

		public StringFormatFlags FormatFlags {
			get {				
				return _flags;
			}

			set {
				_flags = value;
			}
		}

		public HotkeyPrefix HotkeyPrefix {
			get {				
				return _hotkeyPrefix;
			}

			set {
				_hotkeyPrefix = value;
			}
		}

		public StringTrimming Trimming {
			get {
				return _trimming;
			}

			set {
				_trimming = value;
			}
		}

		public int DigitSubstitutionLanguage {
			get {
				return _digitSubstituteLanguage;
			}
		}

		public StringDigitSubstitute DigitSubstitutionMethod {
			get {
				return _digitSubstituteMethod;     
			}
		}


		#endregion

		#region static properties

		public static StringFormat GenericDefault {
			get {
				StringFormat genericDefault = new StringFormat();
				return genericDefault;
			}
		}
		
		public static StringFormat GenericTypographic {
			get {
				StringFormat genericTypographic = new StringFormat(
					StringFormatFlags.FitBlackBox |
					StringFormatFlags.LineLimit |
					StringFormatFlags.NoClip, 
					0 );
				genericTypographic.Trimming = StringTrimming.None;
				return genericTypographic;
			}
		}

		#endregion

		#region public methods

		public void SetMeasurableCharacterRanges (CharacterRange [] range) {
			_charRanges = (CharacterRange [])range.Clone();
		}

		internal CharacterRange [] GetCharRanges {
			get {
				return _charRanges;
			}
		}
	
		public object Clone() {
			StringFormat copy = (StringFormat)MemberwiseClone();
			copy._charRanges = (CharacterRange [])_charRanges.Clone();
			copy._tabStops = (float[])_tabStops.Clone();
			return copy;
		}

		public override string ToString() {
			return "[StringFormat, FormatFlags=" + this.FormatFlags.ToString() + "]";
		}
		
		public void SetTabStops(float firstTabOffset, float[] tabStops) {
			_firstTabOffset = firstTabOffset;
			_tabStops = (float[])tabStops.Clone();
		}

		public void SetDigitSubstitution(int language,  StringDigitSubstitute substitute) {
			_digitSubstituteMethod = substitute;
			_digitSubstituteLanguage = language;
		}

		public float[] GetTabStops(out float firstTabOffset) {
			firstTabOffset = _firstTabOffset;
			return (float[])_tabStops.Clone();
		}

		#endregion
	}
}
