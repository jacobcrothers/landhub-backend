namespace Domains.DBModels
{
    public class PropertyhatchConfiguration : BaseEntity
    {
        public string ConfigKey { get; set; }
        public object ConfigValue { get; set; }
    }
}
