namespace Semdelion.Core.Log.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Realms;

    /// <summary>
    /// 	Провайдер локального хранилища.
    /// </summary>
    public class Repository : IRepository
    {
        protected Realm Instance { get { return Realm.GetInstance(); } }

        /// <summary>
        /// Удалить все локальные данные.
        /// </summary>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        public bool RemoveAll()
        {
            return BeginTransaction(realm => realm.RemoveAll());
        }

        /// <summary>
        /// Удалить элемент по ключу.
        /// </summary>
        /// <typeparam name="T">Тип удаляемого элемента.</typeparam>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        public bool Remove<T>(string key) where T : RealmObject
        {
            return BeginTransaction(realm => realm.Remove(realm.Find<T>(key)));
        }

        /// <summary>
        /// Удалить элемент.
        /// </summary>
        /// <typeparam name="T">Тип удаляемого элемента.</typeparam>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        public bool Remove<T>(T entity) where T : RealmObject
        {
            return BeginTransaction(realm => realm.Remove(entity));
        }

        /// <summary>
        /// Удалить все значения одного типа.
        /// </summary>
        /// <typeparam name="T">Тип удаляемых значений.</typeparam>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        public bool RemoveAll<T>() where T : RealmObject
        {
            return BeginTransaction(realm => realm.RemoveAll<T>());
        }

        /// <summary>
        /// Получить элемент по ключу.
        /// </summary>
        /// <typeparam name="T">Тип искомого элемента.</typeparam>
        /// <returns>Найденный элемент.</returns>
        public T Find<T>(string primaryKey) where T : RealmObject
        {
            return Instance.Find<T>(primaryKey);
        }

        /// <summary>
        /// Получить список элементов одного типа.
        /// </summary>
        /// <typeparam name="T">Тип искомых элементов.</typeparam>
        /// <returns>Весь список найденных элементов.</returns>
        public IEnumerable<T> All<T>() where T : RealmObject
        {
            return Instance.All<T>();
        }

        /// <summary>
        /// Получить список элементов одного типа.
        /// </summary>
        /// <typeparam name="T">Тип искомых элементов.</typeparam>
        /// <param name="predicate">Условие выбора элементов</param>
        /// <returns>Весь список найденных элементов.</returns>
        public IEnumerable<T> All<T>(Expression<Func<T, bool>> predicate) where T : RealmObject
        {
            return Instance.All<T>().Where(predicate);
        }

        /// <summary>
        /// Сохранить или обновить элемент.
        /// </summary>
        /// <typeparam name="T">Тип сохраняемого элемента.</typeparam>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        public bool Add<T>(T entity) where T : RealmObject
        {
            return BeginTransaction(realm => realm.Add(entity, true));
        }

        /// <summary>
        /// Обновление элемента в БД определенным образом.
        /// </summary>
        /// <param name="primaryKey">Ключ, по которому будет найдена сущность для обновления.</param>
        /// <param name="updateAction">Операция обновления.</param>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        public bool Update<T>(string primaryKey, Action<T> updateAction) where T : RealmObject
        {
            return BeginTransaction(realm => updateAction?.Invoke(realm.Find<T>(primaryKey)));
        }

        /// <summary>
        /// Обновление элемента в БД определенным образом.
        /// </summary>
        /// <param name="updateAction">Операция обновления.</param>
        /// <returns>
        /// <c>true</c>, если операция прошла успешно, <c>false</c> иначе.</returns>
        public bool Update(Action updateAction)
        {
            return BeginTransaction(realm => updateAction?.Invoke());
        }

        private bool BeginTransaction(Action<Realm> tune = null)
        {
            try
            {
                var realm = this.Instance;
                using (var transaction = realm.BeginWrite())
                {
                    tune?.Invoke(realm);
                    transaction.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                //App.SendException(ex);
                return false;
            }
        }
    }
}
