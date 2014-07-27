using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceModel.Composition.Tests
{
    public static class AssertPlus
    {
        public static void AnyOfType<T>(IEnumerable collection)
        {
            bool anyOfType = collection.OfType<T>().Any();
            Assert.True(anyOfType);
        }
        
        public static void NoneOfType<T>(IEnumerable collection)
        {
            bool anyOfType = collection.OfType<T>().Any();
            Assert.False(anyOfType);
        }
    }
}
