using Microsoft.Extensions.Configuration;

namespace emr_backend
{
    /// <summary>
    /// This class provides access to the variables in the appsettings.json file.
    /// </summary>
    public class AppSettingsHelper
    {

        #region Constants

        private const string SECTION__DATABASE = "Database";
        private const string FIELD__CONNECTION_STRING = "ConnectionString";

        #endregion

        #region Initialization

        /// <summary>
        /// An object which will contain appsettings.json.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor called by the service provider.
        /// Uses dependency injection to get the configuration object.
        /// </summary>
        public AppSettingsHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Database

        /// <summary>
        /// Get database connection string from configuration.
        /// </summary>
        public string DATABASE_CONNECTION_STRING
        {
            get
            {
                return Configuration.GetSection(SECTION__DATABASE).GetValue<string>(FIELD__CONNECTION_STRING);
            }
        }

        #endregion

    }
}
