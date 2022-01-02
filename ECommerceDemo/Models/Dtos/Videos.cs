using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceDemo.Models.Dtos
{
    public class Videos
    {
        public Videos()
        {
            ProductVideoMappings = new HashSet<ProductVideoMapping>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FirstImageLink { get; set; }
        public string SecondImageLink { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Brand> Influencer { get; set; }

        public virtual ICollection<ProductVideoMapping> ProductVideoMappings { get; set; }
    }
}
