namespace SqlBuilder
{
    public class Builder
    {
        private string sql;
        public Builder()
        {
            sql = "";
        }

        public Builder UseDatabase(string databaseName)
        {
            sql += $"USE DATABASE  {databaseName};";
            return this;
        }
        public Builder CreateDatabase(string databaseName)
        {
            sql += $"CREATE DATABASE  {databaseName};";
            return this;
        }
        public Builder DropDatabase(string databaseName)
        {
            sql += $"DROP DATABASE  {databaseName};";
            return this;
        }
        public Builder BackupDatabase(string databaseName,string filePath,bool differential = false)
        {
            var differentialSql = differential ? "WITH DIFFERENTIAL" : "";
            sql += $"BACKUP DATABASE  {databaseName} TO DISK = '{filePath}' {differentialSql};";
            return this;
        }
        public TableBuilder CreateTable(string tableName)
        {
            return new TableBuilder(tableName,this);
        }
        public Builder AddPrimaryKey(string tableName, string constraintName ,params string[] columnNames)
        {
            constraintName = string.IsNullOrWhiteSpace(constraintName) ? "PK_" + tableName : constraintName;
            sql += $"ALTER TABLE {tableName} ADD CONSTRAINT {constraintName} PRIMARY KEY ({string.Join(",",columnNames)});";
            return this;
        }
        public Builder AddIndexConstraint(string tableName, string constraintName, params string[] columnNames)
        {
            constraintName = string.IsNullOrWhiteSpace(constraintName) ? "IDX_" + tableName + "_" + string.Join("_", columnNames) : constraintName;
            sql += $"CREATE INDEX {constraintName} ON  {tableName} ({string.Join(",", columnNames)});";
            return this;
        }
        public Builder AddUniqueIndexConstraint(string tableName, string constraintName, params string[] columnNames)
        {
            constraintName = string.IsNullOrWhiteSpace(constraintName) ? "IDX_" + tableName + "_" + string.Join("_", columnNames) : constraintName;
            sql += $"CREATE UNIQUE  INDEX {constraintName} ON  {tableName} ({string.Join(",", columnNames)});";
            return this;
        }
        public Builder AddUniqueConstraint(string tableName, string constraintName, params string[] columnNames)
        {
            constraintName = string.IsNullOrWhiteSpace(constraintName) ? "UC_" + tableName : constraintName;
            sql += $"ALTER TABLE {tableName} ADD CONSTRAINT {constraintName} UNIQUE ({string.Join(",", columnNames)});";
            return this;
        }
        public Builder AddDefaultConstraint(string tableName, string constraintName, string columnName, string constraint)
        {
            constraintName = string.IsNullOrWhiteSpace(constraintName) ? "DF_" + tableName : constraintName;
            sql += $"ALTER TABLE {tableName} ADD CONSTRAINT {constraintName} DEFAULT '{constraint}'  FOR  {columnName};";
            return this;
        }
        public Builder AddCheckConstraint(string tableName, string constraintName, params string[] checks)
        {
            constraintName = string.IsNullOrWhiteSpace(constraintName) ? "CHK_" + tableName : constraintName;
            sql += $"ALTER TABLE {tableName} ADD CONSTRAINT {constraintName} CHECK ({string.Join(" AND ", checks)});";
            return this;
        }
        public Builder AddForeignKey(string tableName, string constraintName,string columnName,string refTableName,string refColumnName)
        {
            constraintName = string.IsNullOrWhiteSpace(constraintName) ? "FK_" + tableName : constraintName;
            sql += $"ALTER TABLE {tableName} ADD CONSTRAINT {constraintName} FOREIGN KEY ({columnName}) REFERENCES {refTableName}({refColumnName});";
            return this;
        }
        public Builder AddSql(string sql)
        {
            this.sql += sql;
            return this;
        }
        public string Build()
        {
            return sql;
        }

    }
}
