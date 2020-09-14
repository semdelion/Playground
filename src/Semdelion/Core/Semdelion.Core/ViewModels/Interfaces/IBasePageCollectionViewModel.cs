using System;
using System.Collections.Generic;
using System.Text;

namespace Semdelion.Core.ViewModels.Interfaces
{
    /// <summary>
	/// 	Базовая вью модель, представляющая коллекцию с возможностью загрузки данных постранично.
	/// </summary>
	/// <typeparam name="TItem">Тип модели коллекции.</typeparam>
    public interface IBasePageCollectionViewModel<TItem> : IBaseCollectionViewModel<TItem>
	{
		/// <summary>
		/// 	Количество элементов всего.
		/// </summary>
		//int? TotalCount { get; }
	}
}
