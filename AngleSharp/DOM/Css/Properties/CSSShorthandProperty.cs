﻿namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for all shorthand properties
    /// </summary>
    abstract class CSSShorthandProperty : CSSProperty
    {
        #region Fields

        readonly IEnumerable<CSSProperty> _properties;

        #endregion

        #region ctor

        public CSSShorthandProperty(String name, CSSStyleDeclaration rule, PropertyFlags flags = PropertyFlags.None)
            : base(name, rule, flags | PropertyFlags.Shorthand)
        {
            _properties = CssPropertyFactory.CreateLonghandsFor(name, rule);
            Reset();
        }

        #endregion

        #region Properties

        public IEnumerable<CSSProperty> Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Methods

        protected TProperty Get<TProperty>()
        {
            return _properties.OfType<TProperty>().FirstOrDefault();
        }

        protected Boolean IsComplete(IEnumerable<CSSProperty> properties)
        {
            foreach (var property in _properties)
            {
                if (!properties.Contains(property))
                    return false;
            }

            return true;
        }

        protected Boolean ValidatePeriodic(CSSValue v, CSSProperty t, CSSProperty r, CSSProperty b, CSSProperty l)
        {
            var values = v as CSSValueList ?? new CSSValueList(v);
            CSSValue top = null;
            CSSValue right = null;
            CSSValue bottom = null;
            CSSValue left = null;

            if (values.Length > 4)
                return false;

            foreach (var value in values)
            {
                if (!t.CanStore(value, ref top) && !r.CanStore(value, ref right) && !b.CanStore(value, ref bottom) && !l.CanStore(value, ref left))
                    return false;
            }

            right = right ?? top;
            bottom = bottom ?? top;
            left = left ?? right;
            return t.TrySetValue(top) && r.TrySetValue(right) && b.TrySetValue(bottom) && l.TrySetValue(left);
        }

        protected String SerializePeriodic(CSSProperty t, CSSProperty r, CSSProperty b, CSSProperty l)
        {
            var top = t.SerializeValue();
            var right = r.SerializeValue();
            var bottom = b.SerializeValue();
            var left = l.SerializeValue();

            if (left != right)
                return String.Format("{0} {1} {2} {3}", top, right, bottom, left);
            else if (bottom != top)
                return String.Format("{0} {1} {2}", top, right, bottom);
            else if (right != top)
                return String.Format("{0} {1}", top, right);

            return top;
        }

        internal sealed override void Reset()
        {
            foreach (var property in _properties)
                property.Reset();
        }

        internal override sealed String SerializeValue()
        {
            return SerializeValue(_properties);
        }

        /// <summary>
        /// Serializes the shorthand with only the given properties.
        /// </summary>
        /// <param name="properties">The properties to use.</param>
        /// <returns>The serialized value or an empty string, if serialization is not possible.</returns>
        internal abstract String SerializeValue(IEnumerable<CSSProperty> properties);

        #endregion
    }
}
