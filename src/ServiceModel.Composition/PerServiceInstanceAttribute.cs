// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;

    /// <summary>
    /// Marks exported type to be used in per service instance composition context.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PerServiceInstanceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerServiceInstanceAttribute"/> class
        /// with enabled per service instancing for marked type.
        /// </summary>
        public PerServiceInstanceAttribute()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PerServiceInstanceAttribute"/> class
        /// with specified per service instancing for marked type.
        /// </summary>
        /// <param name="usePerServiceInstancing">If set to <c>true</c> use per service instancing.</param>
        public PerServiceInstanceAttribute(bool usePerServiceInstancing)
        {
            UsePerServiceInstancing = usePerServiceInstancing;
        }

        /// <summary>
        /// Gets a value indicating whether use of filtered composition container catalog to enable
        /// per service instancing behavior.
        /// </summary>
        /// <value>
        /// Always <c>true</c> to use per service instancing behavior.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool UsePerServiceInstancing { get; private set; }
    }
}