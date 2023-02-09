namespace TestTask.Exceptions
{
    public class ProviderSettingsException : Exception
    {

        private readonly string providerName;
        public ProviderSettingsException(string providerName) :
            base($"Setting data is not correct for the {providerName}")
        {

        }
    }
}
