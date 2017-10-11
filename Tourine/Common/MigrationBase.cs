using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentMigrator;

namespace Tourine.Common
{
    [ExcludeFromCodeCoverage]
    public abstract class MigrationBase : FluentMigrator.Migration
    {
        public IList<string> RunningContexts { get; set; }

        protected MigrationBase()
        {
            var type = GetType();
            var migrationAttribute = type.GetCustomAttributes(false).OfType<MigrationAttribute>().FirstOrDefault();
            if (migrationAttribute == null) throw new InvalidOperationException("Migration attribute is required");
        }

        public override void Up()
        {
            if (!CheckRunningContext())
                return;

            EmbeddedScript($"{GetType().FullName}");
        }

        public sealed override void Down() { }

        private void EmbeddedScript(string resourceName)
        {
            if (!GetType().Assembly.GetManifestResourceNames().Contains(resourceName))
                return;

            Execute.EmbeddedScript(resourceName);
        }

        protected bool CheckRunningContext()
        {
            if (RunningContexts == null)
                return true;

            return RunningContexts.Contains(ApplicationContext);
        }
    }
}