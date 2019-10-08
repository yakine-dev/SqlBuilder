namespace SqlBuilder
{
    public class TableBuilder
    {
        private string sql;
        private Builder builder;
        public TableBuilder(string tableName,Builder builder)
        {
            sql = "CREATE TABLE " + tableName;
            this.builder = builder;
        }
        public Builder End()
        {
            return builder.AddSql(sql);
        }
    }
}
