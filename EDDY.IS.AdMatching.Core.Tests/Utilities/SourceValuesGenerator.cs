using System;
using System.Collections.Generic;
using EDDY.IS.Common.ConstantsAndEnums;

namespace EDDY.IS.AdMatching.Core.Tests.Utilities
{
    internal class SourceValuesGenerator
    {
        private readonly Dictionary<string, string> _sourceValues = new Dictionary<string, string>();

        public Dictionary<string, string> Build()
        {
            return _sourceValues;
        }

        internal readonly Random random = new Random(DateTime.Now.Millisecond);
        internal SourceValuesGenerator AddRandomValues(int maxNumberOfKeys = 20)
        {
            for (int i = 0; i < random.Next() % maxNumberOfKeys; i++)
            {
                _sourceValues.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }

            return this;
        }

        internal SourceValuesGenerator AddIdOrFieldValues(IdOrField idOrField, object fieldValue)
        {
            _sourceValues.Add(idOrField.ToString(), fieldValue?.ToString());
            return this;
        }

        internal SourceValuesGenerator AddAgeValues(string age = "15")
        {
            AddIdOrFieldValues(IdOrField.Age, age);
            return this;
        }
    }
}