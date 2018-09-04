using System;
using System.Data;
using System.Configuration;

namespace DbMultiTool
{
    class ScheduleData : DataSet
    {
        private DataTable Task;
        private DataColumn dataColumn1;
        private DataColumn dataColumn2;
        private DataColumn dataColumn3;
        private DataColumn dataColumn4;
        private DataColumn dataColumn5;
        private DataColumn dataColumn6;
        private DataColumn dataColumn7;
        private DataColumn dataColumn8;
        private DataColumn dataColumn9;
        private DataTable TaskForesight;

        private void InitializeComponent()
        {
            this.Task = new System.Data.DataTable();
            this.TaskForesight = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Task)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaskForesight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Task
            // 
            this.Task.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
            this.Task.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "ID"}, false)});
            this.Task.TableName = "Task";
            // 
            // TaskForesight
            // 
            this.TaskForesight.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9});
            this.TaskForesight.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Relation1", "Task", new string[] {
                        "ID"}, new string[] {
                        "ID"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade),
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "ID"}, true)});
            this.TaskForesight.PrimaryKey = new System.Data.DataColumn[] {
        this.dataColumn4};
            this.TaskForesight.TableName = "TaskSummary";
            // 
            // dataColumn1
            // 
            this.dataColumn1.AllowDBNull = false;
            this.dataColumn1.ColumnName = "ID";
            // 
            // dataColumn2
            // 
            this.dataColumn2.AllowDBNull = false;
            this.dataColumn2.ColumnName = "TaskName";
            // 
            // dataColumn3
            // 
            this.dataColumn3.AllowDBNull = false;
            this.dataColumn3.ColumnName = "ParentTaskID";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "ID";
            // 
            // dataColumn5
            // 
            this.dataColumn5.AllowDBNull = false;
            this.dataColumn5.ColumnName = "Tantosya";
            // 
            // dataColumn6
            // 
            this.dataColumn6.AllowDBNull = false;
            this.dataColumn6.ColumnName = "FromDate";
            this.dataColumn6.DataType = typeof(System.DateTime);
            // 
            // dataColumn7
            // 
            this.dataColumn7.AllowDBNull = false;
            this.dataColumn7.ColumnName = "ToDate";
            this.dataColumn7.DataType = typeof(System.DateTime);
            // 
            // dataColumn8
            // 
            this.dataColumn8.AllowDBNull = false;
            this.dataColumn8.ColumnName = "KosuYotei";
            this.dataColumn8.DataType = typeof(decimal);
            // 
            // dataColumn9
            // 
            this.dataColumn9.AllowDBNull = false;
            this.dataColumn9.ColumnName = "KosuJisseki";
            this.dataColumn9.DataType = typeof(decimal);
            // 
            // ScheduleData
            // 
            this.DataSetName = "ScheduleData";
            this.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("Relation1", "Task", "TaskSummary", new string[] {
                        "ID"}, new string[] {
                        "ID"}, false)});
            this.Tables.AddRange(new System.Data.DataTable[] {
            this.Task,
            this.TaskForesight});
            ((System.ComponentModel.ISupportInitialize)(this.Task)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaskForesight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        public ScheduleData()
        {
            switch (ConfigurationManager.AppSettings.Get("DataMode"))
            {
                case "CSV":
                    break;
                case "DB":
                    break;
                default:
                    throw new Exception("それはだめですって～～～～");
            }
        }
    }
}
