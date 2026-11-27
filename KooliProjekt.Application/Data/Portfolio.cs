using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Application.Data { public class Portfolio { public int Id { get; set; } [Required, StringLength(100)] public string Name { get; set; } public int? OwnerId { get; set; } public User Owner { get; set; } public ICollection<Asset> Assets { get; set; } = new List<Asset>(); } }
