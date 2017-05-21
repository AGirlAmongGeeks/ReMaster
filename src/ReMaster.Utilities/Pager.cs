using System;
using System.Collections.Generic;
using System.Text;

namespace ReMaster.Utilities
{
    public class Pager
    {
        #region PageIndex
        public int PageIndex { get; set;}
        #endregion

        #region PageSize
        public int PageSize { get; set; }
        #endregion

        #region SortExpression
        public string SortExpression { get; set; }
        #endregion

        #region RowCount
        public int RowCount { get; set; }
        #endregion

        #region SortDirection
        private string _sortDirection;
        public string SortDirection
        {
            get { return _sortDirection; }
            set { _sortDirection = (value.ToUpper() == "ASC") ? "ASC" : "DESC"; }
        }
        #endregion

        #region Offset
        public int Offset
        {
            get
            {
                return PageSize * (PageIndex - 1);
            }
        }
        #endregion

        #region TotalPages
        public int TotalPages
        {
            get
            {
                return (PageSize > 0) ? (int)(Math.Ceiling((double)RowCount / (double)PageSize)) : 0;
            }
        }
        #endregion

        #region Pager()
        public Pager()
        {
            PageIndex = 1;
            PageSize = 10;
            SortExpression = String.Empty;
            SortDirection = "ASC";
            RowCount = 0;
        }
        #endregion
       
    }
}
