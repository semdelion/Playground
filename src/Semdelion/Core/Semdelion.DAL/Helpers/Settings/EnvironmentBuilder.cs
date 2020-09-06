namespace Semdelion.DAL.Helpers.Settings
{
    using Semdelion.DAL.Helpers.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using YamlDotNet.Core;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.ObjectFactories;

    public class EnvironmentBuilder
    {
        private IEnvironmentSettings settings;

        public EnvironmentBuilder SetSettings(IEnvironmentSettings clientEnvironmentSettings)
        {
            settings = clientEnvironmentSettings;
            return this;
        }

        public IEnvironment Build()
        {
            if (settings == null)
                throw new Exception("Settings from the platform are not specified.");

            var configAssembly = settings.CoreAssembly;
            var filename = GetFilenameByAssembly(configAssembly);
            using var stream = configAssembly.GetManifestResourceStream(filename);
            if (stream == null)
                throw new Exception($"Can't found file in assembly {filename}");

            using var reader = new StreamReader(stream);
            return ParseYamlSettings(reader.ReadToEnd());
        }

        private IEnvironment ParseYamlSettings(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new Exception($"{nameof(content)} for parsing is null.");

            using (var reader = new StringReader(content))
            {
                var deserializer = new DeserializerBuilder().WithTypeConverter(new EnvironmentYamlInspector()).Build();
                var environmentDocument = deserializer.Deserialize<Dictionary<string, Environment >>(new MergingParser(new Parser(reader)));
                if (environmentDocument.TryGetValue(settings.Folder.ToString(), out var currentEnvironment))
                    return currentEnvironment;
            }

            throw new Exception("Can't deserialize content of config.");
        }

        private string GetFilenameByAssembly(Assembly configAssembly)
        {
            if (string.IsNullOrEmpty(settings.Filename))
                throw new Exception($"{nameof(settings.Filename)} can't be null or empty");

            if (string.IsNullOrEmpty(settings.Folder))
                return $"{configAssembly.GetName().Name}.{settings.Filename}";

            return $"{configAssembly.GetName().Name}.{settings.Folder}.{settings.Filename}";
        }
        
        /// <summary>
        ///     Катсомный десериализатор Yaml, который ресолвит интерфейс настроек.
        /// </summary>
        private sealed class EnvironmentYamlInspector : IObjectFactory, ITypeInspector, IYamlTypeConverter
        {
            private readonly DefaultObjectFactory defaultObjectFactory;
            private readonly Dictionary<Type, Type> defaultInterfaceImplementations;

            private readonly ITypeInspector typeInspector;

            public EnvironmentYamlInspector()
            {
                defaultObjectFactory = new DefaultObjectFactory();
                defaultInterfaceImplementations = new Dictionary<Type, Type>{ };
            }

            public EnvironmentYamlInspector(ITypeInspector typeInspector) : this()
            {
                this.typeInspector = typeInspector;
            }

            public object Create(Type type)
            {
                if (type.IsInterface && defaultInterfaceImplementations.TryGetValue(type, out var implementationType))              
                    return Activator.CreateInstance(implementationType);

                return defaultObjectFactory.Create(type);
            }

            public IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
            {
                if (type != null && defaultInterfaceImplementations.TryGetValue(type, out var implementationType))
                    type = implementationType;

                return typeInspector.GetProperties(type, container);
            }
           
            public IPropertyDescriptor GetProperty(Type type, object container, string name, bool ignoreUnmatched)
            {
                if (type != null && defaultInterfaceImplementations.TryGetValue(type, out var implementationType))
                    type = implementationType;

                return typeInspector.GetProperty(type, container, name, ignoreUnmatched);
            }

            public bool Accepts(Type type)
            {
                return typeof(IPlatformSpecificValue).IsAssignableFrom(type);
            }

            public object ReadYaml(IParser parser, Type type)
            {
                return Activator.CreateInstance(type);
            }

            public void WriteYaml(IEmitter emitter, object value, Type type)
            {
            }
        }
    }
}
