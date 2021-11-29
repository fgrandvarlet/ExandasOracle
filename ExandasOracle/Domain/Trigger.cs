using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class Trigger
    {
        const string ENTITY = "TRIGGER";
        public string TriggerName { get; set; }
        public string TriggerType { get; set; }
        public string TriggeringEvent { get; set; }
        public string TableOwner { get; set; }
        public string BaseObjectType { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ReferencingNames { get; set; }
        public string WhenClause { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string ActionType { get; set; }
        public string TriggerBody { get; set; }
        public string BeforeStatement { get; set; }
        public string BeforeRow { get; set; }
        public string AfterRow { get; set; }
        public string AfterStatement { get; set; }
        public string InsteadOfRow { get; set; }
        public string FireOnce { get; set; }
        public string ApplyServerOnly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(Trigger target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            if (this.TriggerType != target.TriggerType)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "TRIGGER_TYPE", this.TriggerType, target.TriggerType
                    ));
            }
            if (this.TriggeringEvent != target.TriggeringEvent)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "TRIGGERING_EVENT", this.TriggeringEvent, target.TriggeringEvent
                    ));
            }
            if (this.TableOwner != target.TableOwner && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "TABLE_OWNER", this.TableOwner, target.TableOwner
                    ));
            }
            if (this.BaseObjectType != target.BaseObjectType)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "BASE_OBJECT_TYPE", this.BaseObjectType, target.BaseObjectType
                    ));
            }
            if (this.TableName != target.TableName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "TABLE_NAME", this.TableName, target.TableName
                    ));
            }
            if (this.ColumnName != target.ColumnName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "COLUMN_NAME", this.ColumnName, target.ColumnName
                    ));
            }
            if (this.ReferencingNames != target.ReferencingNames)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "REFERENCING_NAMES", this.ReferencingNames, target.ReferencingNames
                    ));
            }
            if (this.WhenClause != target.WhenClause)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "WHEN_CLAUSE", this.WhenClause, target.WhenClause
                    ));
            }
            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.ActionType != target.ActionType)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "ACTION_TYPE", this.ActionType, target.ActionType
                    ));
            }
            if (this.TriggerBody != target.TriggerBody)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "TRIGGER_BODY", this.TriggerBody, target.TriggerBody
                    ));
            }
            if (this.BeforeStatement != target.BeforeStatement)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "BEFORE_STATEMENT", this.BeforeStatement, target.BeforeStatement
                    ));
            }
            if (this.BeforeRow != target.BeforeRow)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "BEFORE_ROW", this.BeforeRow, target.BeforeRow
                    ));
            }
            if (this.AfterRow != target.AfterRow)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "AFTER_ROW", this.AfterRow, target.AfterRow
                    ));
            }
            if (this.AfterStatement != target.AfterStatement)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "AFTER_STATEMENT", this.AfterStatement, target.AfterStatement
                    ));
            }
            if (this.InsteadOfRow != target.InsteadOfRow)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "INSTEAD_OF_ROW", this.InsteadOfRow, target.InsteadOfRow
                    ));
            }
            if (this.FireOnce != target.FireOnce)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "FIRE_ONCE", this.FireOnce, target.FireOnce
                    ));
            }
            if (this.ApplyServerOnly != target.ApplyServerOnly)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "APPLY_SERVER_ONLY", this.ApplyServerOnly, target.ApplyServerOnly
                    ));
            }
        }

    }
}
