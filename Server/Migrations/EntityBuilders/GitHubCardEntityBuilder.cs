using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace RyanJagdfeld.Module.GitHubCard.Migrations.EntityBuilders
{
    public class GitHubCardEntityBuilder : AuditableBaseEntityBuilder<GitHubCardEntityBuilder>
    {
        private const string _entityTableName = "RyanJagdfeldGitHubCard";
        private readonly PrimaryKey<GitHubCardEntityBuilder> _primaryKey = new("PK_RyanJagdfeldGitHubCard", x => x.GitHubCardId);
        private readonly ForeignKey<GitHubCardEntityBuilder> _moduleForeignKey = new("FK_RyanJagdfeldGitHubCard_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public GitHubCardEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override GitHubCardEntityBuilder BuildTable(ColumnsBuilder table)
        {
            GitHubCardId = AddAutoIncrementColumn(table,"GitHubCardId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Username = AddStringColumn(table, "Username", 255);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> GitHubCardId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Username { get; set; }
    }
}
