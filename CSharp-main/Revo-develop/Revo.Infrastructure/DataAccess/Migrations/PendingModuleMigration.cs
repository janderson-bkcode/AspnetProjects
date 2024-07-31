using System.Collections.Generic;

namespace Revo.Infrastructure.DataAccess.Migrations
{
    public class PendingModuleMigration
    {
        public PendingModuleMigration(DatabaseMigrationSpecifier specifier, IReadOnlyCollection<IDatabaseMigration> migrations, IDatabaseMigrationProvider provider)
        {
            Specifier = specifier;
            Migrations = migrations;
            Provider = provider;
        }

        public DatabaseMigrationSpecifier Specifier { get; set; }
        public IReadOnlyCollection<IDatabaseMigration> Migrations { get; set; }
        public IDatabaseMigrationProvider Provider { get; set; }
    }
}