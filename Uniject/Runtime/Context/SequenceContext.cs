using System;
using System.Collections.Generic;

namespace Uniject
{
    public class SequenceContext : IResolvable
    {
        protected Dictionary<Type, SequenceValue> m_sequence = new Dictionary<Type, SequenceValue>();

        public object Resolve(Type type)
        {
            if (m_sequence.TryGetValue(type, out SequenceValue value))
            {
                return value.GetNextValue();
            }

            return null;
        }

        public void Add(object value)
        {
            Type type = value.GetType();

            if (!m_sequence.ContainsKey(type))
            {
                m_sequence.Add(type, new SequenceValue());
            }

            m_sequence[type].Add(value);
        }

        public void Reset()
        {
            foreach (KeyValuePair<Type, SequenceValue> keyPair in m_sequence)
            {
                keyPair.Value.Reset();
            }
        }

        public void Clear()
        {
            m_sequence.Clear();
        }

        protected class SequenceValue
        {
            private List<object> m_values;
            private int m_index;

            public SequenceValue()
            {
                m_values = new List<object>();

                m_index = 0;
            }

            public object GetNextValue()
            {
                if (m_values.Count <= m_index)
                {
                    return null;
                }

                object value = m_values[m_index];
                m_index++;

                return value;
            }

            public void Reset()
            {
                m_index = 0;
            }

            public void Add(object value)
            {
                m_values.Add(value);
            }
        }
    }
}
