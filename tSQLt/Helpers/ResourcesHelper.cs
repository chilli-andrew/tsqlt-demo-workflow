using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace tSQLt.Helpers
{
    public class ResourcesHelper
    {
        public string GetEmbeddedResource(string ns, string res)
        {
            var fullyQualifiedResourceName = string.Format("{0}.{1}", ns, res);
            return GetEmbeddedResource(fullyQualifiedResourceName);
        }

        private string GetEmbeddedResource(string fullyQualifiedResourceName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(executingAssembly.GetManifestResourceStream(fullyQualifiedResourceName)))
            {
                return reader.ReadToEnd();
            }
        }

        public IEnumerable<string> GetAllEmbeddedResourcesInAssembly()
        {
            return this.GetType().Assembly.GetManifestResourceNames();
        }

        public IEnumerable<string> GetEmbeddedResourceScriptsFrom(IEnumerable<string> fullyQualifiedResourceNames)
        {
            var resourceScripts = new List<string>();
            if (fullyQualifiedResourceNames == null) return resourceScripts;
            foreach (var fullyQualifiedResourceName in fullyQualifiedResourceNames)
            {
                resourceScripts.Add(GetEmbeddedResource(fullyQualifiedResourceName));
            }

            return resourceScripts;
        }
    }
}