// Copyright(c) DEVSENSE s.r.o.
// All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the License); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
// OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
// IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABILITY OR NON-INFRINGEMENT.
//
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.

using System;

namespace Devsense.PHP.Syntax.Ast
{
	#region Literal

	/// <summary>
	/// Base class for literals.
	/// </summary>
	public abstract class Literal : Expression
	{
        /// <summary>
        /// Gets internal value of literal.
        /// </summary>
        internal abstract object ValueObj { get; }

        /// <summary>
        /// All available value formats for all literal types.
        /// </summary>
        [Flags]
        public enum LiteralFormat
        {
            // default
            None = 0,
            // integer
            Decimal = 0,
            Binary = 1,
            Octal = 2,
            Hexadecimal = 4,
            // real
            FloatingPoint = 0,
            ExponentialSmall = 1,
            ExponentialBig = 2,
            // string
            SingleQuotes = 0,
            DoubleQuotes = 1,
        }

        /// <summary>
        /// Literal value format.
        /// </summary>
        public virtual LiteralFormat Format => LiteralFormat.None;

        protected Literal(Text.Span span)
			: base(span)
		{
		}
	}

	#endregion

	#region IntLiteral

	///// <summary>
	///// Integer literal.
	///// </summary>
 //   public sealed class IntLiteral : Literal
	//{
 //       public override Operations Operation { get { return Operations.IntLiteral; } }

 //       /// <summary>
 //       /// Gets internal value of literal.
 //       /// </summary>
 //       internal override object ValueObj { get { return this.value; } }

	//	/// <summary>
	//	/// Gets a value of the literal.
	//	/// </summary>
 //       public int Value { get { return value; } }
 //       private int value;

	//	/// <summary>
	//	/// Initializes a new instance of the IntLiteral class.
	//	/// </summary>
	//	public IntLiteral(Text.Span span, int value)
 //           : base(span)
	//	{
	//		this.value = value;
	//	}

	//	/// <summary>
 //       /// Call the right Visit* method on the given Visitor object.
 //       /// </summary>
 //       /// <param name="visitor">Visitor to be called.</param>
 //       public override void VisitMe(TreeVisitor visitor)
 //       {
 //           visitor.VisitIntLiteral(this);
 //       }
	//}

	#endregion

	#region LongIntLiteral

	/// <summary>
	/// Integer literal.
	/// </summary>
    public sealed class LongIntLiteral : Literal
	{
        /// <summary>
        /// Literal value format.
        /// </summary>
        public override LiteralFormat Format => _format;
        LiteralFormat _format = LiteralFormat.None;

        public override Operations Operation { get { return Operations.LongIntLiteral; } }

        /// <summary>
        /// Gets internal value of literal.
        /// </summary>
        internal override object ValueObj { get { return this.value; } }

		/// <summary>
		/// Gets a value of the literal.
		/// </summary>
        public long Value { get { return value; } }
		private long value;

		/// <summary>
		/// Initializes a new instance of the IntLiteral class.
		/// </summary>
		public LongIntLiteral(Text.Span span, long value, LiteralFormat format = LiteralFormat.Decimal)
			: base(span)
		{
			this.value = value;
            this._format = format;
        }

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitLongIntLiteral(this);
        }
	}

	#endregion

	#region DoubleLiteral

	/// <summary>
	/// Double literal.
	/// </summary>
    public sealed class DoubleLiteral : Literal
    {
        /// <summary>
        /// Literal value format.
        /// </summary>
        public override LiteralFormat Format => _format;
        LiteralFormat _format = LiteralFormat.None;

        public override Operations Operation { get { return Operations.DoubleLiteral; } }

        /// <summary>
        /// Gets internal value of literal.
        /// </summary>
        internal override object ValueObj { get { return this.value; } }

		/// <summary>
		/// Gets a value of the literal.
		/// </summary>
        public double Value { get { return value; } }
		private double value;

        /// <summary>
        /// Initializes a new instance of the DoubleLiteral class.
        /// </summary>
        /// <param name="value">A double value to be stored in node.</param>
        /// <param name="p">A position.</param>
        /// <param name="format">Value format.</param>
        public DoubleLiteral(Text.Span p, double value, LiteralFormat format = LiteralFormat.Decimal)
			: base(p)
		{
			this.value = value;
            this._format = format;
        }

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitDoubleLiteral(this);
        }
	}

	#endregion

	#region StringLiteral

	/// <summary>
	/// String literal.
	/// </summary>
    public sealed class StringLiteral : Literal
    {
        /// <summary>
        /// Literal value format.
        /// </summary>
        public override LiteralFormat Format => _format;
        LiteralFormat _format = LiteralFormat.None;

        public override Operations Operation { get { return Operations.StringLiteral; } }

        /// <summary>
        /// Gets internal value of literal.
        /// </summary>
        internal override object ValueObj { get { return this.value; } }

		/// <summary>
		/// A <see cref="string"/> value stored in node.
		/// </summary>
		private string value;

		/// <summary>
		/// A value of the literal.
		/// </summary>
        public string Value { get { return value; } }

		/// <summary>
		/// Initializes a new instance of the StringLiteral class.
		/// </summary>
		public StringLiteral(Text.Span span, string value, LiteralFormat format = LiteralFormat.Decimal)
			: base(span)
		{
			this.value = value;
            this._format = format;
        }

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitStringLiteral(this);
        }
	}

	#endregion

	#region BinaryStringLiteral

	/// <summary>
	/// String literal.
	/// </summary>
    public sealed class BinaryStringLiteral : Literal
	{
        public override Operations Operation { get { return Operations.BinaryStringLiteral; } }

        /// <summary>
        /// Gets internal value of literal.
        /// </summary>
        internal override object ValueObj { get { return this.value; } }

		/// <summary>
		/// A value of the literal.
		/// </summary>
        public byte[] Value { get { return value; } }

        /// <summary>
        /// Binary data stored in the node.
        /// </summary>
        private byte[]/*!*/ value;
        
        /// <summary>
		/// Initializes a new instance of the StringLiteral class.
		/// </summary>
		public BinaryStringLiteral(Text.Span span, byte[]/*!*/ value)
			: base(span)
		{
			this.value = value;
		}

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitBinaryStringLiteral(this);
        }
	}

	#endregion

	#region BoolLiteral

	/// <summary>
	/// Boolean literal.
	/// </summary>
    public sealed class BoolLiteral : Literal
	{
        public override Operations Operation { get { return Operations.BoolLiteral; } }

        /// <summary>
        /// Gets internal value of literal.
        /// </summary>
        internal override object ValueObj { get { return this.value; } }

		/// <summary>
		/// Gets a value of the literal.
		/// </summary>
        public bool Value { get { return value; } }
		private bool value;

		public BoolLiteral(Text.Span span, bool value)
			: base(span)
		{
			this.value = value;
		}

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitBoolLiteral(this);
        }
	}

	#endregion

	#region NullLiteral

	/// <summary>
	/// Null literal.
	/// </summary>
    public sealed class NullLiteral : Literal
	{
        public override Operations Operation { get { return Operations.NullLiteral; } }

        /// <summary>
        /// Gets internal value of literal.
        /// </summary>
        internal override object ValueObj { get { return null; } }

		public NullLiteral(Text.Span span)
			: base(span)
		{
		}

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitNullLiteral(this);
        }
	}

	#endregion
}
