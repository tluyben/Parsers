using System;
using System.Reflection.Emit;
using System.Diagnostics;

using PHP.Syntax;

namespace PHP.Core.AST
{
	#region ConstantUse

	/// <summary>
	/// Base class for constant uses.
	/// </summary>
	public abstract class ConstantUse : VarLikeConstructUse
    {
		public ConstantUse(Text.Span span)
			: base(span)
		{
		}
	}

	#endregion

	#region GlobalConstUse

	/// <summary>
	/// Global constant use (constants defined by <c>define</c> function).
	/// </summary>
    public sealed class GlobalConstUse : ConstantUse
	{
        public override Operations Operation { get { return Operations.GlobalConstUse; } }

		public QualifiedName Name { get { return name; } }
		private QualifiedName name;

        /// <summary>
        /// Name used when the <see cref="Name"/> is not found. Used when reading global constant in a namespace context.
        /// </summary>
        internal QualifiedName? FallbackName { get { return fallbackName; } }
        private QualifiedName? fallbackName;

		public GlobalConstUse(Text.Span span, QualifiedName name, QualifiedName? fallbackName)
			: base(span)
		{
			this.name = name;
            this.fallbackName = fallbackName;
		}

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitGlobalConstUse(this);
        }
	}

	#endregion

	#region ClassConstUse

	/// <summary>
	/// Class constant use.
	/// </summary>
    public class ClassConstUse : ConstantUse
	{
		public override Operations Operation { get { return Operations.ClassConstUse; } }

        /// <summary>
        /// Class type reference.
        /// </summary>
        public TypeRef/*!*/TargetType { get { return this.targetType; } }
        private readonly TypeRef/*!*/targetType;

        public VariableName Name => name.Name;
        private readonly VariableNameRef name;

        /// <summary>
        /// Position of <see cref="Name"/> part of the constant use.
        /// </summary>
        public Text.Span NamePosition => name.Span;

        public ClassConstUse(Text.Span span, TypeRef/*!*/typeRef, VariableNameRef/*!*/ name)
            : base(span)
        {
            Debug.Assert(typeRef != null);
            Debug.Assert(!string.IsNullOrEmpty(name.Name.Value));

            this.targetType = typeRef;
			this.name = name;
        }

		/// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitClassConstUse(this);
        }
	}

    /// <summary>
    /// Pseudo class constant use.
    /// </summary>
    public sealed class PseudoClassConstUse : ClassConstUse
    {
        /// <summary>
        /// Possible types of pseudo class constant.
        /// </summary>
        public enum Types
        {
            Class
        }

        /// <summary>Type of pseudoconstant</summary>
        public Types Type { get { return consttype; } }
        private Types consttype;

        internal PseudoClassConstUse(Text.Span span, GenericQualifiedName className, Text.Span classNamePosition, Types type, Text.Span namePosition)
            : this(span, DirectTypeRef.FromGenericQualifiedName(classNamePosition, className), type, namePosition)
		{
		}

        public PseudoClassConstUse(Text.Span span, TypeRef/*!*/typeRef, Types type, Text.Span namePosition)
            : base(span, typeRef, new VariableNameRef(namePosition, type.ToString().ToLowerInvariant()))
        {
            this.consttype = type;
        }

        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitPseudoClassConstUse(this);
        }
    }

	#endregion

    #region PseudoConstUse

    /// <summary>
	/// Pseudo-constant use (PHP keywords: __LINE__, __FILE__, __DIR__, __FUNCTION__, __METHOD__, __CLASS__, __TRAIT__, __NAMESPACE__)
	/// </summary>
    public sealed class PseudoConstUse : Expression
	{
        public override Operations Operation { get { return Operations.PseudoConstUse; } }

		public enum Types { Line, File, Class, Trait, Function, Method, Namespace, Dir }

		private Types type;
        /// <summary>Type of pseudoconstant</summary>
        public Types Type { get { return type; } }

		public PseudoConstUse(Text.Span span, Types type)
			: base(span)
		{
			this.type = type;
		}

        /// <summary>
        /// Call the right Visit* method on the given Visitor object.
        /// </summary>
        /// <param name="visitor">Visitor to be called.</param>
        public override void VisitMe(TreeVisitor visitor)
        {
            visitor.VisitPseudoConstUse(this);
        }
	}

	#endregion
}
