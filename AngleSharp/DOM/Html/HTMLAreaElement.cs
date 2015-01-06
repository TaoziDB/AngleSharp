﻿namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the area element.
    /// </summary>
    sealed class HTMLAreaElement : HTMLElement, IHtmlAreaElement
    {
        #region Fields

        readonly ElementLocation _location;
        TokenList _relList;
        SettableTokenList _ping;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new area element.
        /// </summary>
        internal HTMLAreaElement()
            : base(Tags.Area, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _location = new ElementLocation(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the linked resource is intended to be downloaded rather than displayed.
        /// The value represent the proposed name of the file. If the name is not a valid filename of the
        /// underlying OS, the navigator will adapt it.
        /// </summary>
        public String Download
        {
            get { return GetAttribute(AttributeNames.Download); }
            set { SetAttribute(AttributeNames.Download, value); }
        }

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        public String Href
        {
            get { return _location.Href; }
            set { _location.Href = value; }
        }

        /// <summary>
        /// Gets or sets the fragment identifier, including the leading hash
        /// mark ('#'), if any, in the referenced URL.
        /// </summary>
        public String Hash
        {
            get { return _location.Hash; }
            set { _location.Hash = value; }
        }

        /// <summary>
        /// Gets or sets the hostname and port (if it's not the default port)
        /// in the referenced URL.
        /// </summary>
        public String Host
        {
            get { return _location.Host; }
            set { _location.Host = value; }
        }

        /// <summary>
        /// Gets or sets the hostname in the referenced URL.
        /// </summary>
        public String HostName
        {
            get { return _location.HostName; }
            set { _location.HostName = value; }
        }

        /// <summary>
        /// Gets or sets the path name component, if any, of the
        /// referenced URL.
        /// </summary>
        public String PathName
        {
            get { return _location.PathName; }
            set { _location.PathName = value; }
        }

        /// <summary>
        /// Gets or sets the port component, if any, of the referenced URL.
        /// </summary>
        public String Port
        {
            get { return _location.Port; }
            set { _location.Port = value; }
        }

        /// <summary>
        /// Gets or sets the protocol component, including trailing
        /// colon (':'), of the referenced URL.
        /// </summary>
        public String Protocol
        {
            get { return _location.Protocol; }
            set { _location.Protocol = value; }
        }

        /// <summary>
        /// Gets or sets the URL's username.
        /// </summary>
        public String UserName
        {
            get { return _location.UserName; }
            set { _location.UserName = value; }
        }

        /// <summary>
        /// Gets or sets the URL's password.
        /// </summary>
        public String Password
        {
            get { return _location.Password; }
            set { _location.Password = value; }
        }

        /// <summary>
        /// Gets or sets the search element, including leading question
        /// mark ('?'), if any, of the referenced URL.
        /// </summary>
        public String Search
        {
            get { return _location.Search; }
            set { _location.Search = value; }
        }

        /// <summary>
        /// Get's the URL's origin.
        /// </summary>
        public String Origin
        {
            get { return _location.Origin; }
        }

        /// <summary>
        /// Gets or sets the language of the linked resource.
        /// </summary>
        public String TargetLanguage
        {
            get { return GetAttribute(AttributeNames.HrefLang); }
            set { SetAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the target media of the linked resource.
        /// </summary>
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the value indicating relationships of the
        /// current document to the linked resource.
        /// </summary>
        public String Relation
        {
            get { return GetAttribute(AttributeNames.Rel); }
            set { SetAttribute(AttributeNames.Rel, value); }
        }

        /// <summary>
        /// Gets the value indicating relationships of the current
        /// document to the linked resource, as a list of tokens.
        /// </summary>
        public ITokenList RelationList
        {
            get 
            { 
                if (_relList == null)
                {
                    _relList = new TokenList(GetAttribute(AttributeNames.Rel));
                    _relList.Changed += (s, ev) => UpdateAttribute(AttributeNames.Rel, _relList.ToString());
                }

                return _relList; 
            }
        }

        /// <summary>
        /// Gets the ping HTML attribute, as a settable list of otkens.
        /// </summary>
        public ISettableTokenList Ping
        {
            get 
            { 
                if (_ping == null)
                {
                    _ping = new SettableTokenList(GetAttribute(AttributeNames.Ping));
                    _ping.Changed += (s, ev) => UpdateAttribute(AttributeNames.Ping, _ping.Value);
                }

                return _ping;
            }
        }

        /// <summary>
        /// Gets or sets the alternative text for the element.
        /// </summary>
        public String AlternativeText
        {
            get { return GetAttribute(AttributeNames.Alt); }
            set { SetAttribute(AttributeNames.Alt, value); }
        }

        /// <summary>
        /// Gets or sets the coordinates to define the hot-spot region.
        /// </summary>
        public String Coordinates
        {
            get { return GetAttribute(AttributeNames.Coords); }
            set { SetAttribute(AttributeNames.Coords, value); }
        }

        /// <summary>
        /// Gets or sets the shape of the hot-spot, limited to known values.
        /// The known values are: circle, default. poly, rect. The missing
        /// value is rect.
        /// </summary>
        public String Shape
        {
            get { return GetAttribute(AttributeNames.Shape); }
            set { SetAttribute(AttributeNames.Shape, value); }
        }

        /// <summary>
        /// Gets or sets the browsing context in which to open the linked resource.
        /// </summary>
        public String Target
        {
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the MIME type of the linked resource.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal Boolean IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Helpers

        internal override void Close()
        {
            base.Close();
            RegisterAttributeHandler(AttributeNames.Rel, value =>
            {
                if (_relList != null)
                    _relList.Update(value);
            });
        }

        #endregion
    }
}
