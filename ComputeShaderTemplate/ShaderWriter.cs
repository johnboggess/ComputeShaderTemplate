using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ComputeShaderTemplate
{
    public class ShaderWriter
    {
        private string _source;

        public ShaderWriter() { }

        public void Load(string filePath)
        {
            _source = File.ReadAllText(filePath);
        }

        public void Write(string name, object value)
        {
            if (!_source.Contains($"${name}$"))
                throw new Exception($"Cannot find variable {name}");

            _source = _source.Replace($"${name}$", value.ToString());
        }

        public void Save(string filePath)
        {
            File.WriteAllText(filePath, _source);
        }
    }
}
