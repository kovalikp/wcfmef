using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
    /// <summary>
    /// Marks exported type to be used in per service instance composition context.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PerServiceInstanceAttribute : Attribute
    {
        /// <summary>
        /// Gets value indicating use of filtered composition container catalog to enable 
        /// per service instancing behavior.
        /// </summary>
        /// <value>
        /// Always <c>true</c> to use per service instancing behavior.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool UsePerServiceInstancing { get { return true; } }
    }
}
