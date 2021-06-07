using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.AuditTrail.Models;
using OrchardCore.AuditTrail.ViewModels;
using YesSql;
using YesSql.Filters.Query.Services;

namespace OrchardCore.AuditTrail.Services.Models
{
    public class AuditTrailAdminListOptionBuilder
    {
        private readonly string _value;
        private bool _default;
        private Func<string, IQuery<AuditTrailEvent>, QueryExecutionContext<AuditTrailEvent>, ValueTask<IQuery<AuditTrailEvent>>> _query;
        private Func<AuditTrailAdminListOption, AuditTrailIndexOptions, SelectListItem> _selectListItem;

        public AuditTrailAdminListOptionBuilder(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Specifies the query that will be invoked when this option is selected.
        /// </summary>
        public AuditTrailAdminListOptionBuilder WithQuery(Func<string, IQuery<AuditTrailEvent>, IQuery<AuditTrailEvent>> query)
        {
            _query = (q, val, ctx) => new ValueTask<IQuery<AuditTrailEvent>>(query(q, val));

            return this;
        }

        /// <summary>
        /// Specifies the query that will be invoked when this option is selected.
        /// </summary>

        public AuditTrailAdminListOptionBuilder WithQuery(Func<string, IQuery<AuditTrailEvent>, QueryExecutionContext<AuditTrailEvent>, ValueTask<IQuery<AuditTrailEvent>>> query)
        {
            _query = query;

            return this;
        }

        /// <summary>
        /// Optionally adss a select list item to the option
        /// </summary>

        public AuditTrailAdminListOptionBuilder WithSelectListItem(Func<AuditTrailAdminListOption, AuditTrailIndexOptions, SelectListItem> selectListItem)
        {
            _selectListItem = selectListItem;

            return this;
        }

        /// <summary>
        /// Sets this query option as the default which will be applied when no option has been selected.
        /// </summary>
        public AuditTrailAdminListOptionBuilder AsDefault()
        {
            _default = true;

            return this;
        }

        internal AuditTrailAdminListOption Build()
        {
            var option = new AuditTrailAdminListOption(_value, _query, _selectListItem, _default);

            return option;
        }
    }
}