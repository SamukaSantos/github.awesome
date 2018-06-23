
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GitHub.Awesome.ViewTemplate.Styles
{
    /// <summary>
    /// Builder class responsible for formatting Xamarin.Forms.Span allowing to work with Two-way binding mechanism.
    /// </summary>
    public class FormattedStringBuilder
    {
        #region Fields

        private static readonly Dictionary<string, FontData> _fontDataCache = new Dictionary<string, FontData>();
        private readonly IList<Span> _spans = new List<Span>();
        private bool _withSpaces = true;

        #endregion

        #region Methods

        /// <summary>
        /// Create new Span.
        /// </summary>
        /// <param name="text">Single text.</param>
        /// <returns>FormattedStringBuilder instance.</returns>
        public FormattedStringBuilder Span(string text)
        {
            _spans.Add(new Span
            {
                Text = text
            });
            return this;
        }

        /// <summary>
        /// Create new Span.
        /// </summary>
        /// <param name="text">Text for the Span instance.</param>
        /// <param name="span">Span instance</param>
        /// <returns>FormattedStringBuilder instance.</returns>
        public FormattedStringBuilder Span(string text, Span span)
        {
            span.Text = text;
            _spans.Add(span);
            return this;
        }

        /// <summary>
        /// Create new Span.
        /// </summary>
        /// <param name="span">Span instance.</param>
        /// <returns>FormattedStringBuilder instance.</returns>
        public FormattedStringBuilder Span(Span span)
        {
            _spans.Add(span);
            return this;
        }

        /// <summary>
        /// Create new Span.
        /// </summary>
        /// <param name="text">Text for the internal Span instance.</param>
        /// <param name="styleResource">Resource identifier.</param>
        /// <returns>FormattedStringBuilder instance.</returns>
        public FormattedStringBuilder Span(string text, string styleResource)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"'Text' cannot be empty.");
            }

            FontData data;
            if (_fontDataCache.ContainsKey(styleResource))
            {
                data = _fontDataCache[styleResource];
            }
            else
            {
                data = !string.IsNullOrWhiteSpace(styleResource)
                    ? FontData.FromResource(styleResource)
                    : FontData.DefaultValues();
                _fontDataCache.Add(styleResource, data);
            }
            _spans.Add(new Span
            {
                Text = text,
                FontAttributes = data.FontAttributes,
                FontFamily = data.FontFamily,
                FontSize = data.FontSize,
                ForegroundColor = data.TextColor
            });
            return this;
        }

        /// <summary>
        /// Disable spaces.
        /// </summary>
        /// <returns>FormattedStringBuilder instance.</returns>
        public FormattedStringBuilder WithoutSpaces()
        {
            _withSpaces = false;
            return this;
        }

        /// <summary>
        /// Build operation for consolidating.
        /// </summary>
        /// <returns>FormattedString instance.</returns>
        public FormattedString Build()
        {
            var result = new FormattedString();
            var count = _spans.Count;
            for (var index = 0; index < count; index++)
            {
                var span = _spans[index];
                result.Spans.Add(span);
                if (index < count && _withSpaces)
                {
                    result.Spans.Add(new Span { Text = " " });
                }
            }
            return result;
        }

        #endregion
    }
}   
