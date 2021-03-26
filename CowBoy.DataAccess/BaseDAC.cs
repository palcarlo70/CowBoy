namespace CowBoy.DataAccess
    {
    public abstract class BaseDAC<T> where T : class,new()
        {
        protected string connectionString { get; set; }

       
        protected BaseDAC(string conn)
            {
            connectionString = conn;
            }

        #region "metodi astratti da implementare"

        /// <summary>
        /// Inserisce l'istanza della classe in bancadati
        /// </summary>
        /// <param name="entity">istanza della classe da inserire</param>
        public abstract void Create(T entity);

        /// <summary>
        /// Modifica il record in bancadati sulla base dell'istanza della classe
        /// </summary>
        /// <param name="entity">istanza della classe da aggiornare</param>
        public abstract void Update(T entity);

        /// <summary>
        /// Cancella il record in bancadati sulla base dell'istanza della classe
        /// </summary>
        /// <param name="entity">istanza della classe da cancellare</param>
        public abstract void Delete(T entity);

        /// <summary>
        /// Salva il record in bancadati sulla base dell'istanza della classe.
        /// <remarks>Determinare se inserire (create) o modificare (update) l'istanza in 
        /// base al risultato del metodo isNew dell'entita</remarks> 
        /// </summary>
        /// <param name="entity">istanza della classe da salvare</param>
        public abstract void Save(T entity);

        /// <summary>
        /// Restituisce il dettaglio dell'entità specificata
        /// </summary>
        /// <param name="idEntity">id dell'entità d'interesse</param>
        /// <returns></returns>
        public abstract T Load(int idEntity);

        #endregion

        }
    }
