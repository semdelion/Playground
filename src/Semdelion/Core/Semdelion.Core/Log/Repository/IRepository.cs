namespace Semdelion.Core.Log.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Realms;

    /// <summary>
    /// 	Провайдер локального хранилища.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        ///     Сохраняет элемент в БД, если он уже там есть – обновляет его.
        /// </summary>
        /// <typeparam name="T">Тип сохраняемого элемента.</typeparam>
        /// <returns><c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        bool Add<T>(T entity) where T : RealmObject;

        /// <summary>
        ///     Обновление элемента в БД определенным образом.
        /// </summary>
        /// <param name="primaryKey">Ключ, по которому будет найдена сущность для обновления.</param>
        /// <param name="updateAction">Операция обновления.</param>
        /// <returns><c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        bool Update<T>(string primaryKey, Action<T> updateAction) where T : RealmObject;

        /// <summary>
        /// Обновление элемента в БД определенным образом.
        /// </summary>
        /// <param name="updateAction">Операция обновления.</param>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        bool Update(Action updateAction);

        /// <summary>
        /// 	Получить элемент по ключу.
        /// </summary>
        /// <typeparam name="T">Тип искомого элемента.</typeparam>
        /// <returns>Найденный элемент.</returns>
        T Find<T>(string primaryKey) where T : RealmObject;

        /// <summary>
        /// 	Получить список элементов одного типа.
        /// </summary>
        /// <typeparam name="T">Тип искомых элементов.</typeparam>
        /// <returns>Весь список найденных элементов.</returns>
        IEnumerable<T> All<T>() where T : RealmObject;

        /// <summary>
        /// Получить список элементов одного типа.
        /// </summary>
        /// <typeparam name="T">Тип искомых элементов.</typeparam>
        /// <param name="predicate">Условие выбора элементов</param>
        /// <returns>Весь список найденных элементов.</returns>
        IEnumerable<T> All<T>(Expression<Func<T, bool>> predicate) where T : RealmObject;

        /// <summary>
        ///     Удалить элемент по ключу.
        /// </summary>
        /// <typeparam name="T">Тип удаляемого элемента.</typeparam>
        /// <returns><c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        bool Remove<T>(string key) where T : RealmObject;

        /// <summary>
        ///     Удалить элемент.
        /// </summary>
        /// <typeparam name="T">Тип удаляемого элемента.</typeparam>
        /// <returns><c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        bool Remove<T>(T entity) where T : RealmObject;

        /// <summary>
        ///     Удалить все значения одного типа.
        /// </summary>
        /// <typeparam name="T">Тип удаляемых значений.</typeparam>
        /// <returns><c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        bool RemoveAll<T>() where T : RealmObject;

        /// <summary>
        ///     Удалить все локальные данные.
        /// </summary>
        /// <returns><c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        bool RemoveAll();
    }
}
