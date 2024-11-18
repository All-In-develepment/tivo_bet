using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Configuration
    {
        [Key]
        public Guid ConfigurationId { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
    }
}