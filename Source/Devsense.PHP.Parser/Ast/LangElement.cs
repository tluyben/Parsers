using System;
using Devsense.PHP.Text;

namespace Devsense.PHP.Syntax.Ast
{
    public interface IAstNode
    {

    }

    /// <summary>
    /// Base class for all AST nodes.
    /// </summary>
    public abstract class AstNode : IAstNode, IPropertyCollection
    {
        #region Fields & Properties

        /// <summary>
        /// Contains properties of this <see cref="AstNode"/>.
        /// </summary>
        private PropertyCollection _properties;

        /// <summary>
        /// Gets property collection associated with this node.
        /// </summary>
        public IPropertyCollection Properties { get { return (IPropertyCollection)this; } }

        #endregion

        #region IPropertyCollection

        void IPropertyCollection.SetProperty(object key, object value)
        {
            _properties.SetProperty(key, value);
        }

        object IPropertyCollection.GetProperty(object key)
        {
            return _properties.GetProperty(key);
        }

        public void SetProperty<T>(T value)
        {
            _properties.SetProperty<T>(value);
        }

        public T GetProperty<T>()
        {
            return _properties.GetProperty<T>();
        }

        bool IPropertyCollection.TryGetProperty(object key, out object value)
        {
            return _properties.TryGetProperty(key, out value);
        }

        bool IPropertyCollection.TryGetProperty<T>(out T value)
        {
            return _properties.TryGetProperty<T>(out value);
        }

        bool IPropertyCollection.RemoveProperty(object key)
        {
            return _properties.RemoveProperty(key);
        }

        bool IPropertyCollection.RemoveProperty<T>()
        {
            return _properties.RemoveProperty<T>();
        }

        void IPropertyCollection.ClearProperties()
        {
            _properties.ClearProperties();
        }

        object IPropertyCollection.this[object key]
        {
            get
            {
                return _properties.GetProperty(key);
            }
            set
            {
                _properties.SetProperty(key, value);
            }
        }

        #endregion
    }

    /// <summary>
	/// Base class for all AST nodes representing PHP language Elements - statements and expressions.
	/// </summary>
	public abstract class LangElement : AstNode
	{
        #region ContainingElement

        /// <summary>
        /// Gets the parent symbol in the AST hierarchy.
        /// </summary>
        public virtual LangElement ContainingElement { get; set; }

        /// <summary>
        /// Gets reference to containing namespace scope or <c>null</c> in case of global namespace.
        /// </summary>
        public NamespaceDecl ContainingNamespace => LookupContainingElement<NamespaceDecl>();

        /// <summary>
        /// Gets reference to containing type declaration.
        /// </summary>
        public TypeDecl ContainingType => LookupContainingElement<TypeDecl>();

        /// <summary>
        /// Gets reference to containing source unit.
        /// </summary>
        public SourceUnit ContainingSourceUnit => LookupContainingElement<GlobalCode>()?.SourceUnit;

        /// <summary>
        /// Iterates through containing elements to find closest element of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of element to look for.</typeparam>
        /// <returns>Reference to element of type <typeparamref name="T"/> or <c>null</c> is not containing.</returns>
        protected T LookupContainingElement<T>() where T : LangElement
        {
            for (var x = this.ContainingElement; x != null; x = x.ContainingElement)
            {
                if (x is T)
                {
                    return (T)x;
                }
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Position of element in source file.
        /// </summary>
        public Span Span { get; protected set; }
		
		/// <summary>
        /// Initialize the LangElement.
        /// </summary>
        /// <param name="span">The position of the LangElement in the source code.</param>
		protected LangElement(Span span)
		{
			this.Span = span;
		}

        /// <summary>
        /// In derived classes, calls Visit* on the given visitor object.
        /// </summary>
        /// <param name="visitor">Visitor.</param>
        public abstract void VisitMe(TreeVisitor/*!*/visitor);
	}

    #region Scope

    public struct Scope
    {
        public int Start { get { return start; } }
        private int start;

        public static readonly Scope Invalid = new Scope(-1);
        public static readonly Scope Global = new Scope(0);
        public static readonly Scope Ignore = new Scope(Int32.MaxValue);

        public bool IsGlobal
        {
            get
            {
                return start == 0;
            }
        }

        public bool IsValid
        {
            get
            {
                return start >= 0;
            }
        }

        public Scope(int start)
        {
            this.start = start;
        }

        public void Increment()
        {
            start++;
        }

        public void Decrement()
        {
            start--;
        }

        public override string ToString()
        {
            return start.ToString();
        }
    }

    #endregion

    #region IHasSourceUnit

    /// <summary>
    /// Annotates AST nodes having reference to containing source unit.
    /// </summary>
    public interface IHasSourceUnit
    {
        /// <summary>
        /// Gets source unit of the containing source file.
        /// </summary>
        SourceUnit SourceUnit { get; }
    }

    #endregion
}