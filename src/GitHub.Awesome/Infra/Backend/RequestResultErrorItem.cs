
namespace GitHub.Awesome.Infra.Backend
{
    /// <summary>
    /// Transport class for custom errors.
    /// </summary>
    public class RequestResultErrorItem
    {
        #region Properties

        public string Key { get; set; }
        public string Message { get; set; }

        #endregion
    }
}
