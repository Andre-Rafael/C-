namespace Sped.Lib.Data
{
    public class MdbStringConnection
    {
        #region Singleton
        private static MdbStringConnection _instance;
        public static MdbStringConnection GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new MdbStringConnection();

                return _instance;
            }
        }
        #endregion

        private string ProvaiderMSSQL;
        private string ProvaiderMSJet;
        private string ProvaiderACEOledb;

        private MdbStringConnection()
        {
            ProvaiderMSSQL = "SQLNCLI11";
            ProvaiderMSJet = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
            ProvaiderACEOledb = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        }

        /// <summary>
        /// Generico
        /// </summary>
        /// <param name="dbProvaider"></param>
        /// <param name="dbSource"></param>
        /// <param name="dbCatalog"></param>
        /// <param name="dbUser"></param>
        /// <param name="dbPass"></param>
        /// <returns></returns>
        public string GetStringConnection(string dbProvaider, string dbSource, string dbCatalog, string dbUser, string dbPass)
        {
            var strConn = string.Empty;

            if (!string.IsNullOrWhiteSpace(dbProvaider))
                strConn = string.Format("Provider={0};", dbProvaider);

            if (!string.IsNullOrWhiteSpace(dbSource))
                strConn += string.Format("Data Source={0};", dbSource);

            if (!string.IsNullOrWhiteSpace(dbCatalog))
                strConn += string.Format("Initial Catalog={0};", dbCatalog);

            if (!string.IsNullOrWhiteSpace(dbUser))
                strConn += string.Format("User Id={0};", dbUser);

            if (!string.IsNullOrWhiteSpace(dbPass))
                strConn += string.Format("Password={0};", dbPass);

            return strConn;

        }

        /// <summary>
        /// String de Conexão com Instancia do SQL Server
        /// </summary>
        /// <param name="dbSource">Nome da Instancia de Servidor</param>
        /// <param name="dbUser">Usuario SA ou padrão da aplicação</param>
        /// <param name="dbPass">Senha do usuario</param>
        /// <param name="dbCatalog">Nome do data base anexado na instancia do Servidor</param>
        /// <param name="provaider">True: Oledb / False: SqlClient</param>
        /// <returns>String de Conexão</returns>
        public string MSSQLServerSqlCliet(string dbSource, string dbUser, string dbPass, string dbCatalog, bool provaider)
        {
            if (provaider)
                return GetStringConnection(ProvaiderMSSQL, dbSource, dbCatalog, dbUser, dbPass);
            else
                return GetStringConnection(null, dbSource, dbCatalog, dbUser, dbPass);
        }

        /// <summary>
        ///  String de Conexão com Instancia do SQL Server exclusivo para EFCore
        /// </summary>
        /// <param name="dbSource">Nome da Instancia de Servidor</param>
        /// <param name="dbUser">Usuario SA ou padrão da aplicação</param>
        /// <param name="dbPass">Senha do usuario</param>
        /// <param name="dbCatalog">Nome do data base anexado na instancia do Servidor</param>
        /// <param name="provaider">True: Oledb / False: SqlClient</param>
        /// <returns>String de Conexão</returns>
        public string MSSQLServerEFCore(string dbSource, string dbCatalog, string dbUser, string dbPass)
        {
            return GetStringConnection(null, dbSource, dbCatalog, dbUser, dbPass);
        }

        /// <summary>
        /// Obter uma String de Conexão para um SQLLocalDB, 
        /// será necessario utilizar uma intancia nomeada "MSSQLLocalDB".
        /// Instale o sqllocaldb.exe v14+(2017), e digite no prompt:
        /// </summary>
        /// <param name="provaider">True - Para utilizar com Oledb / False - Para utilizar com SqlClient</param>
        /// <param name="dbInitialCatalog">Caminho completo do arquivo de bando de dados ".mdf"</param>
        /// <returns></returns>
        public string MSSQLLocalDB(string dbInitialCatalog, bool dbProvaider)
        {
            var strConn = string.Empty;

            if (dbProvaider)
                strConn = string.Format("Provider={0};", ProvaiderMSSQL);

            strConn += "Data Source=(LocalDB)\\MSSQLLocalDB;";

            if (!string.IsNullOrEmpty(dbInitialCatalog))
                strConn += string.Format("AttachDbFilename={0};", dbInitialCatalog);

            strConn += "Integrated Security=True;";

            return strConn;
        }

        /// <summary>
        /// String de Conexão com SQLite3
        /// </summary>
        /// <param name="dbCatalog">Caminho logico do arquivo de Banco de Dados</param>
        /// <returns></returns>
        public string SQLite3(string dbCatalog)
        {
            return GetStringConnection(null, dbCatalog, null, null, null);
        }

        /// <summary>
        /// Conexao MSACCESS
        /// </summary>
        /// <param name="dbCatalog">URL do arquivo de banco de dados a qual quer acessar.</param>
        /// <returns>String de conexão com proverdor JET4 32bits</returns>
        public string MSJet(string dbCatalog)
        {
            return ProvaiderMSJet + dbCatalog;
        }

        /// <summary>
        /// Conexao MSACCESS
        /// </summary>
        /// <param name="dbCatalog">URL do arquivo de banco de dados a qual quer acessar.</param>
        /// <returns>String de conexão com proverdor ACE12 32bits e 64bits </returns>
        public string MSAce(string dbCatalog)
        {
            return ProvaiderACEOledb + dbCatalog;
        }

        /// <summary>
        /// Conexão com EXCEL
        /// </summary>
        /// <param name="dbCatalog"></param>
        /// <returns></returns>
        public string MSACEOledbEXCEL(string dbCatalog)
        {
            return string.Format("{0}{1};Extended Properties='Excel 12.0 Xml; HDR=YES; IMEX=1;'", ProvaiderACEOledb, dbCatalog);
        }

    }
}
