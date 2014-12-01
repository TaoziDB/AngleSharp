﻿namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-style
    /// </summary>
    sealed class CSSTextDecorationStyleProperty : CSSProperty, ICssTextDecorationStyleProperty
    {
        #region Fields

        internal static readonly TextDecorationStyle Default = TextDecorationStyle.Solid;
        internal static readonly IValueConverter<TextDecorationStyle> Converter = From(Map.TextDecorationStyles);
        TextDecorationStyle _style;

        #endregion

        #region ctor

        internal CSSTextDecorationStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TextDecorationStyle, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected decoration style.
        /// </summary>
        public TextDecorationStyle DecorationStyle
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        public void SetDecorationStyle(TextDecorationStyle style)
        {
            _style = style;
        }

        internal override void Reset()
        {
            _style = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetDecorationStyle);
        }

        #endregion
    }
}
