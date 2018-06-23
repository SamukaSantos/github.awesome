using System;
using System.Collections.Generic;
using System.Linq;
using GitHub.Awesome.Infra.Resources;

namespace GitHub.Awesome.Infra.Common.Paging
{
	public class PagingManager
	{
		#region Fields

		private List<PagingItem> _pagingItems;
		private int _currentPosition;

		#endregion

		#region Properties

        /// <summary>
        /// Current PageItem.
        /// </summary>
        public PagingItem Current
		{
			get { return _pagingItems[_currentPosition]; }
		}

        /// <summary>
        /// Current positon.
        /// </summary>
        public int Position
		{
			get { return _currentPosition + 1; }
		}

        /// <summary>
        /// Count of Total items.
        /// </summary>
		public int TotalItems
		{
			get { return _pagingItems.Count; }
		}

		#endregion

		#region Constructor

		public PagingManager()
		{
			_pagingItems = new List<PagingItem>();
		}

		#endregion

		#region Methods

        /// <summary>
        /// Add PageItem instance.
        /// </summary>
        /// <param name="item">PageItem.</param>
		public void Add(PagingItem item)
		{
			if (item != null)
				_pagingItems.Add(item);
		}

        /// <summary>
        /// Change current position to update the paging.
        /// </summary>
        /// <param name="position">Position.</param>
		public void SetCurrent(int position)
		{
			if (position < 1)
				throw new ArgumentException(AppResources.VALIDATION_PAGING_CURRENT_POSITION);
			
			_currentPosition = position - 1;
		}

		#endregion

	}
}
