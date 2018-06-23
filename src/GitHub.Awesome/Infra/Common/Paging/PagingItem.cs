using System;
namespace GitHub.Awesome.Infra.Common.Paging
{
	/// <summary>
    /// Represents a paging configuration.
    /// </summary>
	public class PagingItem
	{

		#region Properties

        /// <summary>
        /// Page
        /// </summary>
        public int Page { get; set; }

		/// <summary>
        /// Total per page.
        /// </summary>
		public int TotalPerPage { get; set; }

        /// <summary>
        /// Helps the manager dectect if this item is actual.
        /// </summary>
		public bool IsCurrent { get; set; }

		#endregion
	}
}
